using UnityEngine;

public class Enemy : BaseEnemyController
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
    private bool isAtordoada = false;
    private float delayTime;

    public AudioClip fxCobraAttack;
    public AudioClip fxCobraDie;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        delayTime = Time.deltaTime * 120f;
        if (!isBoss)
            anim.Play(AnimationTagsConstants.Walk);
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, layerGround);
        groundedHorizontal = Physics2D.OverlapCircle(groundCheckHorizontal.position, radiusCheckHorizontal, layerGround);

        if (isAtordoada)
            delayTime -= Time.deltaTime;

        if (delayTime <= 0 && isAtordoada)
        {
            isAtordoada = false;
            anim.Play(AnimationTagsConstants.Walk);
        }

        if (isAtordoada)
            return;

        if ((!grounded) || (groundedHorizontal))
            Flip();
         
    }

    void FixedUpdate()
    {
        if (isAtordoada)
            return;

        rb2D.velocity = new Vector2(isVisible ? speed : 0f, rb2D.velocity.y);
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
            anim.Play(AnimationTagsConstants.Death);
        if (other.gameObject.CompareTag(TagsConstants.CocoPartido) && !isBoss)
        {
            anim.Play(AnimationTagsConstants.CobraAtordoada);
            isAtordoada = true;
            delayTime = Time.deltaTime * 120f;
        }
    }
    
    void EnemyDie()
    {
        SoundManager.Instance.PlayFxCobraDie(fxCobraDie);
        Destroy(gameObject);
    }
    
}
