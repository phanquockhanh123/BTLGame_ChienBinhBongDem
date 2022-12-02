using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _swordAnimation;
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _swordAnimation = transform.GetChild(1).GetComponent<Animator>();
    }
    public void Move(float move)
    {
        _anim.SetFloat("move",Mathf.Abs(move));
    }
    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
    }
    public void Hit()
    {
        _anim.SetTrigger("Hit");
    }
    public void Attack() {
        _anim.SetTrigger("Attack");
        _swordAnimation.SetTrigger("SwordAnimation");
    }
    public void Death()
    {
        _anim.SetTrigger("Death");
    }
    public void Restart()
    {
        _anim.SetTrigger("restart");
    }
}
