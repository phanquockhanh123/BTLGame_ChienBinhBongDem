using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{

    public Stats _Stats;
    [SerializeField] public int damage;
    [SerializeField] protected int gems;
    [SerializeField] protected float speed;

    [SerializeField] protected Transform wayPointA, wayPointB;
    [SerializeField] protected Transform target;
    [SerializeField] protected AnimationClip idleClip;
    [SerializeField] bool isIdling;

    [SerializeField] protected Animator animator;
    [SerializeField] protected State state;
    [SerializeField] protected GameObject player;
                    // protected bool isDizzy=false;
    [SerializeField] protected PlayerController _player;
    [SerializeField] protected bool hasAttaked;
    [SerializeField] protected GameObject chestPrefab;
    [SerializeField] protected Rigidbody2D rg;

    public int Damage
    {
        get { return damage; }
    }

    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    protected void InitVariables()
    {
        target = wayPointA;
        animator = GetComponent<Animator>();
        state = State.Roaming;
        animator.SetBool("IsIdling", false);
        player = GameObject.FindGameObjectWithTag("Player"); 
        animator.SetFloat("Speed", 1);
    }
    
    protected virtual void MoveToWaypoint()
    {
        if ((transform.position - target.position).magnitude < 0.1f)
        {
            isIdling = true;
            animator.SetFloat("Speed",0);
            //StartCoroutine(RandomIdling());
            if (target == wayPointA)
            {
                animator.SetFloat("Facing", 1);
                target = wayPointB;
            }
            else
            {
                animator.SetFloat("Facing", 0);                
                target = wayPointA;
            }
        }
        else
        {
            if (!isIdling&& animator.GetBool("InCombat")==false)
            {
                //fix them de eneny sau khi thoat combat neu gan diem nao se di ve phia diem do
                if(target==wayPointA)
                {
                    animator.SetFloat("Facing", 0);
                }
                else
                {
                    animator.SetFloat("Facing", 1);
                }
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }                
            else
            {
                StartCoroutine(PlayIdle());
            }
        }
    }

    IEnumerator PlayIdle()
    {
        // Play animation
        animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(idleClip.length);
        isIdling = false;
        animator.SetFloat("Speed", 1);
    }

    //IEnumerator RandomIdling()
    //{
    //    float timer = Random.Range(1f, 2f);
    //    while (timer > 0f)
    //    {
    //        yield return timer -= Time.deltaTime;
    //    }
    //    isIdling = true;
    //}

    protected void RoamingMode()
    {
        MoveToWaypoint();
    }

    protected virtual void AttackMode()
    {
        
        if (Vector2.Distance(transform.position, player.transform.position) > 3f)
        {
            animator.SetBool("InCombat", false);
            // state = State.Roaming;
           
            StopAction();
            return;
        }

        animator.SetFloat("Facing", player.transform.position.x - transform.position.x);
        if (Vector2.Distance(transform.position, player.transform.position) > 1.5f )
        {
            animator.SetFloat("Speed", 1);

            transform.position = Vector2.MoveTowards
                (transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);
            
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

    protected IEnumerator AttackCoroutine()
    {
        animator.SetBool("InCombat", true);

        yield return new WaitForSeconds(3f);
        
    }

    protected IEnumerator Recover()
    {
        yield return new WaitForSeconds(1f);
        state = State.Combat;
    }

    public void StopAction()
    {
        state = State.Roaming;
        animator.SetFloat("Speed", 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Stop moving when hit player while roaming
        if (collision.gameObject.CompareTag("Player"))
        {
            if (state == State.Roaming)
            {
                state = State.Idle;
                animator.SetFloat("Speed", 0);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Continue moving after player left while roaming
        if (collision.gameObject.CompareTag("Player"))
        {
            if (state == State.Idle)
            {
                state = State.Roaming;
                animator.SetFloat("Speed", 1);
            }
        }
    }

    void Test()
    {
        TextAsset a = Resources.Load("text.txt") as TextAsset;
        string[] arrayText = a.text.Split('\n');
        for (int i = 0; i< arrayText.Length; i++)
        {
            print(arrayText[i]);
        }
    }
    

}

public enum State
{
    Roaming,
    Combat,
    Dizzy,
    Die,
    Idle
}
