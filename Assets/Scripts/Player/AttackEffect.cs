using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.hasFlame)
        {
            gameObject.SetActive(false);
        }
    }

    public void PlayEffect(bool isLeft, bool isAttacking)
    {
        if (isAttacking)
            return;

        if (isLeft)
        {
            animator.Play("CircleEffect_Left");
        }
        else
        {
            animator.Play("CircleEffect_Right");
        }
    }
}
