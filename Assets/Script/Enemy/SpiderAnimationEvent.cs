using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    private Spider _spider;
    private Animator animSpider;
    private void Start()
    {
        _spider = transform.parent.GetComponent<Spider>();
        animSpider = GetComponent<Animator>();
    }
    public void Fire()
    {
        _spider.Attack();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _spider.isShoot = true;
            animSpider.SetTrigger("Attack");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _spider.isShoot = false;
        }
    }
}
