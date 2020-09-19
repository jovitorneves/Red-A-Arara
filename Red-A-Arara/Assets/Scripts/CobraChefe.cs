using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobraChefe : MonoBehaviour
{
    public float speed;
    public Transform groundCheck;
    public Transform groundCheckHorizontal;

    public LayerMask layerGround;

    public int damageTaken;
    public float radiusCheck;
    public float radiusCheckHorizontal;

    public bool grounded;
    public bool groundedHorizontal;

    private Rigidbody2D rb2D;
    private Animator anim;

    private bool facingRight = true;
    private bool isVisible = false;

    public AudioClip fxCobraAttack;
    public AudioClip fxCobraDie;
    public AudioClip fxCobraDamageTaken;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        grounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, layerGround);
        groundedHorizontal = Physics2D.OverlapCircle(groundCheckHorizontal.position, radiusCheckHorizontal, layerGround);

        if ((!grounded) || (groundedHorizontal))
        {
            Flip();
        }
            
      
    }

    void FixedUpdate()
    {
        if (isVisible)
        {
            rb2D.velocity = new Vector2(speed, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed *= -1;
    }

    void OnBecameVisible()
    {
        Invoke("MoveEnemy", 1f);
    }

    void OnBecameInvisible()
    {

    }

    void MoveEnemy ()
    {
        isVisible = true;
        anim.Play("WALK");
    }

    void StopEnemy ()
    {
        isVisible = false;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            damageTaken = damageTaken + 1;

            if (damageTaken == 3)
            {
                SoundManager.instance.PlayFxCobraDie(fxCobraDie);
                anim.Play("DEATH");
            }
            else
            {
                anim.Play("TAKENDAMAGE");
                SoundManager.instance.PlayFxCobraChefeDamageTaken(fxCobraDamageTaken);
                Invoke("MoveEnemy", 1f);
            }
                

        }
    }
    

    void EnemyDie()
    {
        Destroy(gameObject);
    }
    
}
