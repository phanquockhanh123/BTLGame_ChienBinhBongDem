using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Skeleton : EnemyBase, IDamagable
{
    public int health { get; set; }
    public GameObject playerCharater;
    private AudioSource audioSource;
    public AudioClip _foot;
    public AudioClip _attack;
    public AudioClip _hit;
    public AudioClip _death;
    public float dir;
    public BoxCollider2D colli;
    // Start is called before the first frame update

    void Start()
    {
        LoadEnemyData();
        audioSource = gameObject.GetComponent<AudioSource>();
        InitVariables();
        health = _Stats.sts_Heath;
        gems = _Stats.sts_gem;
        damage = _Stats.sts_Attack;
        speed = _Stats.sts_Speed;
        colli = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //check player trong tam phat hien;
        bool isPlayerDetected = CheckPlayer();
        if (isPlayerDetected && state != State.Combat)
        {
                state = State.Combat;
                colli.isTrigger = false;
        }
        if (!isPlayerDetected && state != State.Roaming)
        {
            animator.SetBool("InCombat", false);
            state = State.Roaming;
            //animator.SetFloat("Speed", 1);
            GetComponent<Collider2D>().isTrigger = true;
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")|| animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }
        switch (state)
        {
            case State.Idle:
                break;
            case State.Roaming:
                RoamingMode();
                break;
            case State.Combat:
                if (Vector2.Distance(transform.position, player.transform.position) <= 0.65f)
                {
                    animator.SetBool("InCombat", true);
                    animator.SetFloat("Speed", 0);
                }
                else
                {
                    animator.SetBool("InCombat", false);

                }
                if ((player.transform.position.x <= wayPointA.transform.position.x )|| (player.transform.position.x >= wayPointB.transform.position.x))
                {
                    if (transform.position.x <= wayPointA.transform.position.x || transform.position.x >= wayPointB.transform.position.x)
                    {
                        animator.SetFloat("Speed", 0);
                        return;
                    }

                }
                AttackMode();
                break;
            case State.Dizzy:
                isDizzy();
                break;
            case State.Die:
                break;
        }
        
        dir = transform.position.x-player.transform.position.x;
      
    }

    public bool CheckPlayer()
    {
        return (Vector2.Distance(transform.position, player.transform.position) <= 3.5f && ((dir > 0 && animator.GetFloat("Facing") <= 0) || (dir < 0 && animator.GetFloat("Facing") > 0)));
    }
    public void TakeDamage(int amount)
    {
        _Stats.sts_Heath -= amount;

        if (_Stats.sts_Heath <= 0)
        {
            StartCoroutine(Die());
            return;
        }

        print("Take damage: Cur HP = " + _Stats.sts_Heath);
        animator.Play("Hit");
        AttackMode();
        state = State.Dizzy;
        StartCoroutine(Recover());
    }

    protected override void AttackMode()
    {
        
        animator.SetFloat("Facing", player.transform.position.x - transform.position.x);
        if (Vector2.Distance(transform.position, player.transform.position) > 0.65f)
        {
            animator.SetBool("InCombat", false);
            animator.SetFloat("Speed", 1);

            transform.position = Vector2.MoveTowards
                (transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);
            
        }
        else
        {
            animator.SetBool("InCombat", true);
            animator.SetFloat("Speed", 0);
            StartCoroutine(AttackCoroutine());

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.gameObject.CompareTag("Player")&& state==State.Combat)
            {
                player.GetComponent<PlayerController>().TakeDamage(damage);
            }
        
    }
   
    public IEnumerator Die()
    {
        state = State.Die;
        animator.Play("Die");
        GameManager.instance.dieEnemy++;
        yield return new WaitForSeconds(1f);
        GameManager.instance.DropGems(gems, transform.position);
        gameObject.SetActive(false);
    }
    public IEnumerator isDizzy()
    {
        state = State.Dizzy;
        rg.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1f);

    }
     public void LoadEnemyData() 
     {
            TextAsset _json = Resources.Load<TextAsset>("Skeleton");
            _Stats = (Stats) JsonUtility.FromJson<Stats>(_json.text);
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
    
}


