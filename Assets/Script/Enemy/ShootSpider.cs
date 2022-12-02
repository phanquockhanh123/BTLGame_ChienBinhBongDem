using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpider : MonoBehaviour
{
    //private Spider spider;
    private void Start()
    {
        //spider = transform.parent.GetComponent<Spider>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //spider.isShoot = true;
        }
    }
}
