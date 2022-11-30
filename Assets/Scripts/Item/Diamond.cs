using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.AddGem(1);
        gameObject.SetActive(false);
    }

    IEnumerator DisapearCountdown()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
}
