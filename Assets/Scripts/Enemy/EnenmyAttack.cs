using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenmyAttack : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(GetComponentInParent<EnemyBase>().damage );
            
        }
    }
}
