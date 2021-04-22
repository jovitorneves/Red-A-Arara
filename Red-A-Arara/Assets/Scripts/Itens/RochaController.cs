using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RochaController : MonoBehaviour
{

    private Rigidbody2D rb2D;
    private float speed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = new Vector2(1 * speed, rb2D.velocity.y);
    }
}
