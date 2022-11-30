using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamagable hit = collision.gameObject.GetComponent<IDamagable>();
            hit.TakeDamage(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamagable hit = collision.gameObject.GetComponent<IDamagable>();
            hit.TakeDamage(GetComponentInParent<PlayerController>().Damage);
            Debug.Log("Damage:" + GetComponentInParent<PlayerController>().Damage);
        }
    }

}
