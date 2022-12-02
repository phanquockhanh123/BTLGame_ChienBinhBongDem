using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject DiamodPrefab;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform poinA, poinB;
    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected bool IsHit = false;
    protected bool IsDead = false;
    protected Player player;
    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();    
    }
    private void Start()
    {
        Init();
    }
    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("IsCombat")== false)
        {
            return;
        }
        if (IsDead == false)
        {
            Movement();
        }
    }
    public virtual void Movement()
    {
        if (currentTarget == poinA.position)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        if (transform.position == poinA.position)
        {
            //_swite = false;
            currentTarget = poinB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == poinB.position)
        {
            //_swite = true;
            currentTarget = poinA.position;
            anim.SetTrigger("Idle");
        }
        if (IsHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if(distance > 2.0f)
        {
            IsHit = false;
            anim.SetBool("IsCombat", false);
        }
        Vector3 diretion = player.transform.localPosition - transform.localPosition;
        if (diretion.x > 0 && anim.GetBool("IsCombat") == true)
        {
            sprite.flipX = false;
        }
        else if (diretion.x < 0 && anim.GetBool("IsCombat") == true)
        {
            sprite.flipX = true;
        }
    }

}
