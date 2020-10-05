using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreiraController : MonoBehaviour
{

    [SerializeField]
    private Collider2D macacoBoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        macacoBoxCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player")){
            macacoBoxCollider.isTrigger = false;
        } else
        {
            macacoBoxCollider.isTrigger = true;
        }
    }
}
