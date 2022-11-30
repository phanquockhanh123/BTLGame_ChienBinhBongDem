using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

#pragma warning disable 0649
public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] Animator animator;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float gravity;
    [SerializeField] float jumpForce;
    [SerializeField] float maxHeight;
    [SerializeField] bool isGroundTest;

    [SerializeField] bool isAttacking;
    [SerializeField] AnimationClip attackAnim;
    [SerializeField] AttackEffect attackEffect;
    [SerializeField] bool facingIsLeft;

    [SerializeField] public int damage ;
    [SerializeField] bool isDizzy;
    [SerializeField] public State state;

    [SerializeField] GameManager gm;
    [SerializeField] AudioSource audi;
    [SerializeField] AudioClip step;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip sword;
    [SerializeField] AudioClip die;
    [SerializeField] AudioClip itemCollect;
    private int _jump;

    public bool isGrounded;

    public int Damage
    {
        get { return damage; }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gm = GameManager.instance;
        isDizzy = false;
        audi = GetComponent<AudioSource>();
        rb.mass =10;
        damage =1;
    }

    // Update is called once per frame
    void Update()
    {
        if(state== State.Die)
        {
            animator.Play("Die");
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            return;
            
        }

        if (GameManager.instance.hasFlame)
        {
            damage =2;
            animator.SetBool("HadSword",true);
        }


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            return;


       /*if (isDizzy)
        {
            Dizzy();
            return;
        }*/
    if (animator.GetCurrentAnimatorStateInfo(0).IsName("FlameLeft") ||animator.GetCurrentAnimatorStateInfo(0).IsName("FlameRight") )
        {
            return;
        }
     if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }
        Attack();
        InputMovement();

        if (Input.GetKeyDown(KeyCode.T))
        {
            Test();
        }
    }

    private void FixedUpdate()
    {
        StayOnTheGround();
    }

    void InputMovement()
    {
        // Move sideway
        float horizontal = Input.GetAxisRaw("Horizontal"); //CrossPlatformInputManager.GetAxis("Horizontal");
        

        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        // Jump
        if (CheckForGround()||_jump ==1)
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                _jump = (_jump+1)%2;
                animator.SetTrigger("Jump");
                animator.ResetTrigger("HitGround");
                isGrounded = false;
            }
        }



        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("VelocityY", rb.velocity.y);


        if (horizontal != 0)
        {
            animator.SetFloat("Facing", rb.velocity.x);
            if (rb.velocity.x > 0)
            {
                facingIsLeft = false;
            }
            else
            {
                facingIsLeft = true;
            }

        }

        isGroundTest = CheckForGround();
    }

    bool CheckForGround()
    {
        if (isGrounded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void StayOnTheGround()
    {
        

        // Land on feet
        if (rb.velocity.y <= 0f && CheckForGround())
        {
            animator.SetTrigger("HitGround");
            animator.ResetTrigger("Jump");
            _jump =0;
           // rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }

    void Dizzy()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void Attack()
    {
        if (CheckForGround() && /*CrossPlatformInputManager.GetButtonDown("Attack") ||*/ Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        attackEffect.PlayEffect(facingIsLeft, isAttacking);
        float coolTime = attackAnim.length;
        isAttacking = true;
        animator.SetTrigger("Attack");
        //Luu lai vi tri khi danh
        Vector3 currentPos  = transform.position;
        while (coolTime > 0)
        {
            //Danh thi dung yen trong qua trinh danh
            transform.position = currentPos;
            yield return coolTime -= Time.deltaTime;
        }
        isAttacking = false;
        animator.ResetTrigger("Attack");
    }

    public void TakeDamage(int amount)
    {
        if (gm.health < 1)
        {
            Die();
        }
        
        

        gm.health = Mathf.Clamp(gm.health - amount, 0, gm.healthMax);
        UIManager.instance.UpdateHealth();

       
        animator.Play("Hit");
        //isDizzy = true;

        StopCoroutine(Recover());
        StartCoroutine(Recover());
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(1f);
        isDizzy = false;
    }

    public void Die()
    {
        state = State.Die;
        StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine()
    {
        //animator.Play("Die");
        EnemyBase[] enemies = FindObjectsOfType<EnemyBase>();
        foreach (EnemyBase enemy in enemies)
        {
            enemy.StopAction();
        }

        yield return new WaitForSeconds(2f);
        print("Die");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Stop movement when hit ground while jumping
        if (collision.gameObject.CompareTag("Ground") && !CheckForGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }


        if(collision.gameObject.CompareTag("Ground")&&rb.velocity.y ==0)
        {
            isGrounded = true;
        }
    }


    void Test()
    {
        TextAsset a = Resources.Load("text.txt") as TextAsset;
        string[] arrayText = a.text.Split('\n');
        for (int i = 0; i < arrayText.Length; i++)
        {
            print(arrayText[i]);
        }
    }
    public void audiStep()
    {
        audi.PlayOneShot(step);
    }
    public void audiJump()
    {
        audi.PlayOneShot(jump);
    }
    public void audiDeath()
    {
        audi.PlayOneShot(die);
    }
    public void audiSword()
    {
        audi.PlayOneShot(sword);
    }
    public void audiCollect()
    {
        audi.PlayOneShot(itemCollect);
    }

}
