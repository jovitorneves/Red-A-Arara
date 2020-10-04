using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacacoController : MonoBehaviour
{
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Rigidbody2D macacoRb;

    private bool isGrounded;
    public float jumpForce = 200.0f;

    // Start is called before the first frame update
    void Start()
    {
        macacoRb.AddForce(new Vector2(0, jumpForce));
    }

    // Update is called once per frame
    void Update()
    {
        //Input.GetButtonDown("Jump") &&
        //if ( isGrounded)
        //{
        //    macacoRb.AddForce(new Vector2(0, jumpForce));
            

        //}

        PuloMacaco();
    }

    private void FixedUpdate()
    {
        //cria um colisor de cicurlo, em baixo do colisor do protagonista, quando ele estiver no cao
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
    }

    private void PuloMacaco()
    {

    }
}
