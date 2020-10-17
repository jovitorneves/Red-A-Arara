using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Transform groundCheck;
    public Transform groundCheckHorizontal;

    public LayerMask layerGround;
    
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

    void OnBecameVisible ()
    {
        Invoke(MethodNameTagsConstants.MoveEnemy, 3f);
    }

    void OnBecameInvisible ()
    {

    }

    void MoveEnemy ()
    {
        isVisible = true;
        anim.Play(AnimationTagsConstants.Walk);
    }

    void StopEnemy ()
    {
        isVisible = false;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        
        if (other.gameObject.CompareTag(TagsConstants.Player))
        {
            anim.Play(AnimationTagsConstants.Morte);
        }
    }
    
    void EnemyDie()
    {
        SoundManager.Instance.PlayFxCobraDie(fxCobraDie);
        Destroy(gameObject);
    }
    
}
