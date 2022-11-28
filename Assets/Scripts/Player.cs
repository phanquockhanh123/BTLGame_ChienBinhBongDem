using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce = 5.0f;
    [SerializeField]
    private bool _grounded = false;
    // Start is called before the first frame update
    void Start()
    {
        // assign hendle of rigibody
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // horizontal input for left/right
        float move = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space) && _grounded == true)
        {
            // jump
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            _grounded = false;
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        
        if(hitInfo.collider != null)
        {
            Debug.Log("Hit: " + hitInfo.collider.name); 
            _grounded = true;

        }

        _rigid.velocity = new Vector2(move, _rigid.velocity.y);
        
    }
}
