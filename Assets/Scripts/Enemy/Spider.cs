using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Spider : EnemyBase, IDamagable
{
    public int health { get; set; }
    [SerializeField] GameObject acid;

    public GameObject playerCharater;
    public AudioClip _foot;
    public AudioClip _attack;
    public AudioClip _hit;
    public AudioClip _death;
    private AudioSource audioSource;
    public float countdown;
    private float attackCooldown = 3, facingValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
        audioSource = gameObject.GetComponent<AudioSource>();
        SpiderData load = JsonUtility.FromJson<SpiderData>(LoadSpiderData());
        health = load.health;
        speed = load.speed;
        damage = load.Damange;
    }
    // Update is called once per frame
    void Update()
    {
        bool isPlayerDetected = CheckPlayer();
        if (isPlayerDetected && state != State.Combat)
        {
            animator.SetFloat("Speed", 0);
            countdown += Time.deltaTime;
            if (countdown >= 5)
            {
                state = State.Combat;
                GetComponent<Collider2D>().isTrigger = false;
            }
        }
        if (!isPlayerDetected && state != State.Roaming)
        {
            animator.SetBool("InCombat", false);
            state = State.Roaming;
            animator.SetFloat("Speed", 1);
            GetComponent<Collider2D>().isTrigger = true;
        }
        switch (state)
        {
            case State.Idle:
                break;
            case State.Roaming:
                MoveToWaypoint();
                break;
            case State.Combat:
                //Flip
                if (transform.position.x > player.transform.position.x)
                    facingValue = 0;
                else
                    facingValue = 1;
                animator.SetFloat("Facing", facingValue);
                //Khoang cach bang 6
                if (Vector2.Distance(transform.position, player.transform.position) < 10f)
                {
                    animator.SetFloat("Speed", 0);//bat dau tan cong
                    if (!hasAttaked)
                    {
                        animator.SetBool("InCombat", true);
                        attackCooldown = 3;
                    }
                    else
                    {
                        attackCooldown -= Time.deltaTime;
                        hasAttaked = attackCooldown <= 0;
                    }
                }
                break;
            case State.Dizzy:
                break;
            case State.Die:
                break;
        }
    }
    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            StartCoroutine(Die());
            return;
        }
        print("Take damage: Cur HP = " + health);
        animator.Play("Hit");
        animator.SetBool("InCombat", true);
        state = State.Dizzy;
        StartCoroutine(Recover());
    }
    public bool CheckPlayer()
    {
        return (facingValue >= 0.5f && (player.transform.position.x - transform.position.x < 10f)) || (facingValue <= 0.5f && (transform.position.x - player.transform.position.x < 10f));
    }
    public void Fire()
    {
        GameObject bullet = Instantiate(acid, transform.position, Quaternion.identity);
        bullet.transform.parent = transform;
        bullet.GetComponent<AcidScript>().setfacing(animator.GetFloat("Facing"));
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
    public class SpiderData
    {
        public int health;
        public float speed;
        public int Damange;
    }
    string LoadSpiderData()
    {
        TextAsset asset = Resources.Load("SpiderData") as TextAsset;
        string jsonString = asset.text;
        return jsonString;
    }
}
