using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidScript : MonoBehaviour
{
    Vector2 direction = Vector2.zero;
    Rigidbody2D rb;
    float speed = 2.5f;
    Vector2 playerpos,target;
    public float _facing;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_facing);
        if(_facing>=0.5f)
        {
            
            target = new Vector2(transform.position.x + 10f, transform.position.y);
        }
        else
        {           
            target = new Vector2(transform.position.x - 10f, transform.position.y);
        }
    }


    public void setfacing(float x)
    {
        _facing =x;
    }

    
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {   
       transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
       if (transform.position.x == target.x)
       {
           Destroy(gameObject);
       }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(GetComponentInParent<Spider>().Damage);
            Destroy(gameObject);
        }
            
        
    }
}
