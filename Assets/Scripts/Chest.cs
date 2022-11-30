using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int gems;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip collect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("collect");
            audio.PlayOneShot(collect);
            GameManager.instance.AddGem(gems);
            
            Destroy(this.gameObject,1f);
            
           
            
        }
    }
}
