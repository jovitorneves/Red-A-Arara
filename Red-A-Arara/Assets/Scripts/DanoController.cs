using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
