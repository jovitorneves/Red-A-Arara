using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Colocar um tempo de 3 segundos falando que o usuario pode pular novamente na HUD
public class FlyingController : MonoBehaviour
{

    private Rigidbody2D playerRigidbody;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask layerGround;

    private Rigidbody2D tempPlayerRigidbody;
    private int jumpCount = 4;
    private readonly int jumpFixedForce = 1300;
    private readonly int dragFixed = 30;
    private readonly int jumpForce = 700;
    private bool isGrounded;
    private readonly float radiusCheck = 0.5f;

    private float timer = 0.0f;
    private float waitTime = 2.0f;

    private bool isCaindo = false;
    private bool isJumped = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        tempPlayerRigidbody = playerRigidbody;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        IsFalling();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, layerGround);

        //&& timer > waitTime
        //Input.GetKey(KeyCode.Space)
        if (Input.GetKey(KeyCode.Q))//Input.GetButton("Jump") *enquanto o usuario estiver pressionando o espaco
        {
            Flying();
            isJumped = true;
            //timer = 0f;
        }
        if (isJumped)
            playerRigidbody.drag = isGrounded ? tempPlayerRigidbody.drag : dragFixed;
    }

    private void Flying()
    {
        if (jumpCount == 0 && isGrounded)
        {
            playerRigidbody.drag = tempPlayerRigidbody.drag;
            jumpCount = 4;
            isJumped = false;
        }

        if (jumpCount == 0) return;

        if (isGrounded)
            playerRigidbody.AddForce(new Vector2(0f, isGrounded ? jumpFixedForce : jumpForce));
        else
        {
            playerRigidbody.AddForce(new Vector2(0f, jumpForce/2));
            playerRigidbody.AddForce(new Vector2(0f, jumpForce/2));
        }
        //playerRigidbody.drag = isGrounded ? tempPlayerRigidbody.drag : dragFixed;

        jumpCount--;
        print("jumpCount: "+ jumpCount);


    }

    private void IsFalling()
    {
        if (!isJumped)
        {
            isCaindo = false;
            return;
        }
        if (playerRigidbody.velocity.y < -0.1)
        {
            isCaindo = true;
        }
        else
        {
            isCaindo = false;
        }
    }
}
