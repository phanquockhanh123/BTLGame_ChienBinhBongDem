using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public static Diamond _intance;
    public int gems = 1;
    public static int soLuongDiamod = 0;
    private void Awake()
    {
        _intance = this;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.AddGem(gems);
                soLuongDiamod++;
                //player.diamod += gems;
                Destroy(this.gameObject);
            }      
        }
    }
}
