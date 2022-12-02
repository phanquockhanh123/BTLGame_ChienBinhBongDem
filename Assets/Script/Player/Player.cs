using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour,IDamageable
{
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jump = 10.0f;
    private bool resetJumpNeeded = false;
    [SerializeField]
    private float _speed = 5f;
    private PlayerAnimator _playAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;
    private bool _ground = false;
    private Spider spider;
    //public float heath = 10f;
    public int diamod;
    private bool Deal = false;
    private bool stop = false;
    public panelWin PanelWin;
    public panelLose PanelLose;
    public int Health { get; set; }
    void Start()
    {
        
        _rigid = GetComponent<Rigidbody2D>();
        _playAnim = GetComponent<PlayerAnimator>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        //spider = GameObject.FindGameObjectWithTag("Spider").GetComponent<Spider>();
        Health = 4;
        diamod = PlayerPrefs.GetInt("diamod");

    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded() == true && Deal == false)
        {
            _playAnim.Attack();
        }
        if (Health < 1)
        {
            stop = true;
            PanelLose.PanelLose.SetActive(true);
            //StartCoroutine(Restart());
        }
        UIManager.Instance.UpdateGemCount(diamod);
        PlayerPrefs.SetInt("diamod",diamod);
    }
    void Movement()
    {
        if (stop == false)
        {
            float move = CrossPlatformInputManager.GetAxisRaw("Horizontal");/* Input.GetAxisRaw("Horizontal");*/
            float move1 = Input.GetAxisRaw("Horizontal");
            _ground = IsGrounded();

            if ((move > 0 || move1 > 0) && Deal == false)
            {
                Flip(true);
            }
            else if ((move < 0 || move1 < 0) && Deal == false)
            {
                Flip(false);
            }

            if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && IsGrounded() == true && Deal == false)
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, _jump);
                resetJumpNeeded = true;
                StartCoroutine(ResetJumpNeededRoutine());
                _playAnim.Jump(true);
            }

            _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
            _playAnim.Move(move);
            _rigid.velocity = new Vector2(move1 * _speed, _rigid.velocity.y);
            _playAnim.Move(move1);
        }
    }
    void Flip(bool right)
    {
        if (right == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 0;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (right == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -0;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }
    /*public void Restart()
    {
        SceneManager.LoadScene("Main_Menu");
    }*/
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Main_Menu");
    }
    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << 8);
        if (hitInfo.collider != null)
        {
            if (resetJumpNeeded == false)
            {
                _playAnim.Jump(false);
                return true;
            }
        }
        return false;
    }
    IEnumerator ResetJumpNeededRoutine()
    {
        resetJumpNeeded = true;
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
    }
    public void Damage()
    {
        
        if (Health < 1)
        {
            return;
        }
        Health--;
        UIManager.Instance.UpdateLives(Health);
        if (Health < 1)
        {
            _playAnim.Death();
            _speed = 0;
            Deal = true;
        }
        if (Health >=1)
        {
            _playAnim.Hit();
        }
    }

    public void AddGem(int amount)
    {
        diamod += amount;
        
    }
    IEnumerator ResetPlayer()
    {
        yield return new WaitForSeconds(1);
        _playAnim.Restart();
        transform.position = new Vector2(transform.position.x - 3f, transform.position.y + 6f);
        stop = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "isDeath")
        {
            stop = true;
            Debug.Log("Death!!!!!!!!!!!!!");
            Damage();
            if (Health < 1)
            {
                return;
            }
            StartCoroutine(ResetPlayer());
        }
        if(other.gameObject.tag == "LoadLevel")
        {
            stop = true;
            Debug.Log("hoan thanh !!!!!!!!!!");
            //SceneManager.LoadScene("Level2");
            PanelWin.PanelWin.SetActive(true);
        }
    }
}
