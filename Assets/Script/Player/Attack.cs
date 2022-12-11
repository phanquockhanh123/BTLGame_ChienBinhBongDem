using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canDamage = true;
    [SerializeField] AudioSource audi;
    [SerializeField] AudioClip AudiHit;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);
        IDamageable hit = other.GetComponent<IDamageable>();
        if(hit != null)
        {
            if(_canDamage == true)
            {
                hit.Damage();
                _canDamage = false;
                StartCoroutine(ResetDamage());
                
            }
            audi.PlayOneShot(AudiHit);
        }
    }
    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }
}
