using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
    
public class MossGiant : EnemyBase, IDamagable
{
    public int health { get; set; }
    //public GameObject playerCharater;
    public AudioClip _foot;
    public AudioClip _attack;
    public AudioClip _hit;
    public AudioClip _death;
    private AudioSource audioSource;
    
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        InitVariables();
        MossData load = JsonUtility.FromJson<MossData>(LoadMossData());
        //health = load.health;
        health = 5;
        speed = load.speed;
        damage = load.Damange;
    }

    //Nhin thay player trong tam nhin
    public bool CheckPlayer()
    {
        if (((animator.GetFloat("Facing")>=0.5f) && (player.transform.position.x - transform.position.x > 0f)&&(player.transform.position.x - transform.position.x <10f))||((animator.GetFloat("Facing")<=0.5f) && (transform.position.x -  player.transform.position.x >0f)&&(transform.position.x -  player.transform.position.x <10f)))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }



    //Xac dinh trong diem AB
     protected bool EnemyInRange()
    {
        if(transform.position.x <= wayPointB.position.x && transform.position.x >=wayPointA.position.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Kiem tra player con nam trong AB khong
    protected bool PlayerInRange()
    {
        if(player.transform.position.x <= wayPointB.position.x && player.transform.position.x >=wayPointA.position.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
    // Update is called once per frame
    void Update()
    {

        switch(state)
        {    
            case State.Idle:
                break;
            case State.Dizzy:
                break;
            case State.Die:
                StartCoroutine(Die());
                break;
            case State.Roaming:
                //Neu chi nhin thay 
                if(CheckPlayer())
                {
                    animator.SetFloat("Speed",0);
                    animator.SetBool("donothing",true);
                    return;
                }
                //Neu khong nhin thay nua
                    animator.SetBool("donothing",false);
                    animator.SetFloat("Speed",1);
                    RoamingMode();
                    break;
            case State.Combat:
                    animator.SetBool("donothing",false);
                    if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        return;
                    }
                    AttackMode();
                break;
        }  
    }

        

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            state = State.Die;
            return;
        }
        print("Take damage: Cur HP = " + health);
        animator.Play("Hit");
        animator.SetBool("InCombat", true);
        GetComponent<Collider2D>().isTrigger = false; 
        state = State.Dizzy;
        StartCoroutine(Recover());
    }

    protected override void AttackMode()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > 10f && animator.GetBool("InCombat"))
        {
            animator.SetBool("InCombat", false);
            GetComponent<Collider2D>().isTrigger = true;
            StopAction();
            return;
        }

        if(player.transform.position.x - transform.position.x>0)
        {
            animator.SetFloat("Facing", 1);
        }
        else
        {
            animator.SetFloat("Facing", 0  );
        }
        
        if (Vector2.Distance(transform.position, player.transform.position) > 1.5f )
        {
            if((transform.position.x <= (wayPointB.position.x )) && transform.position.x >=(wayPointA.position.x))
            {
                if(!PlayerInRange())
                {
                    if(animator.GetFloat("Facing")<0.5f)
                    {
                        if(transform.position.x!=wayPointA.position.x)
                        {
                            animator.SetFloat("Speed", 1);
                            target = wayPointA;
                            transform.position = Vector2.MoveTowards
                            (transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
                        }
                        else
                        {
                             animator.SetFloat("Speed",0);
                            animator.SetBool("donothing",true);
                            return;
                        }
                    }
                    else
                    {
                        if(transform.position.x!=wayPointB.position.x)
                        {
                            animator.SetFloat("Speed", 1);
                            target = wayPointB;
                            transform.position = Vector2.MoveTowards
                            (transform.position, new Vector2(target.position.x, transform.position.y), speed * Time.deltaTime);
                        }
                        else
                        {
                            animator.SetFloat("Speed",0);
                            animator.SetBool("donothing",true);
                            return;
                        }
                    }
                }
                else
                {
                    animator.SetFloat("Speed", 1);
                    transform.position = Vector2.MoveTowards
                    (transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);
                }
            }
            else
            {
                animator.SetFloat("Speed",0);
                animator.SetBool("donothing",true);
                return;
            }
            
        }
        else
        {
            animator.SetFloat("Speed", 0);
            if (!hasAttaked)
            {
                StartCoroutine(AttackCoroutine());
            }
            else
            {
                return;
            }
            
        }
    }


    
    public IEnumerator Die()
    {
        animator.Play("Die");
        GameManager.instance.dieEnemy++; //SO ENEMY BI TIEU DIET TANG 
        yield return new WaitForSeconds(1f);
        GameObject chest = (GameObject)Instantiate(chestPrefab, transform.position+new Vector3(0,-0.5f,0), Quaternion.identity);
        chest.GetComponent<Chest>().gems = base.gems;
        gameObject.SetActive(false);
    }
    public void PlayerSound()
    {
        audioSource.PlayOneShot(_foot);
    }
    public void PlayerHitSound()
    {
        audioSource.PlayOneShot(_hit);
    }
    public void PlayerAttackSound()
    {
        audioSource.PlayOneShot(_attack);
    }
    public void PlayerDeathSound()
    {
        audioSource.PlayOneShot(_death);
    }
    public class MossData
    {
        public int health;
        public float speed;
        public int Damange;
    }
    string LoadMossData()
    {
        TextAsset asset = Resources.Load("MossData") as TextAsset;
        string jsonString= asset.text;
        return jsonString;
    }
}
