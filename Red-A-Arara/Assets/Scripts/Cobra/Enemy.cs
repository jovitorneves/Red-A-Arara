using UnityEngine;

public class Enemy : BaseEnemyController
{
    public float speed;
    public Transform groundCheck;
    public Transform groundCheckHorizontal;

    //[SerializeField]
    private float hitPoints;
    //[SerializeField]
    private float maxHitPoints = 3;
    [SerializeField]
    private HealthbarController healthbar;

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
    private int cocoCount = 0;
    private float delayTime;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hitPoints = maxHitPoints;
        healthbar.SetHealth(hitPoints, maxHitPoints);
        delayTime = Time.deltaTime * 220f;
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

        if (isDead) return;

        if ((!grounded) || (groundedHorizontal))
            Flip();
         
    }

    void FixedUpdate()
    {
        if (isAtordoada) return;

        if (isDead) return;

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

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(TagsConstants.Player))
        {
            var playerController = collision2D.gameObject.GetComponent(typeof(Player)) as Player;
            if (!playerController.isAlive) return;
            if (UtilController.Instance.ReturnDirection(collision2D.contacts) == HitDirection.Top)
            {
                hitPoints -= 1;
                healthbar.SetHealth(hitPoints, maxHitPoints);
                if (hitPoints <= 0)
                    CobraMorta();
                else
                    SoundManager.Instance.PlayDanoInimigo();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.Player))
        {
            var playerController = collision.gameObject.GetComponent(typeof(Player)) as Player;
            if (!playerController.isAlive) return;
            if (UtilController.Instance.ReturnDirection(collision.contacts) == HitDirection.Top)
            {
                hitPoints -= 1;
                healthbar.SetHealth(hitPoints, maxHitPoints);
                if (hitPoints <= 0)
                    CobraMorta();
            }
                //CobraMorta();
        }

    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag(TagsConstants.CocoPartido) && !isBoss)
        {
            if (!isAtordoada)
                cocoCount++;


            hitPoints -= 2;
            healthbar.SetHealth(hitPoints, maxHitPoints);
            if (hitPoints <= 0)
                CobraMorta();

            if (cocoCount < 2)
            {
                anim.Play(AnimationTagsConstants.CobraAtordoada);
                isAtordoada = true;
                delayTime = Time.deltaTime * 220f;
                SoundManager.Instance.PlayFxAtordoado();
            }
            //else
            //{
            //    CobraMorta();
            //}
        }

        //if (other.gameObject.CompareTag(TagsConstants.Player))
        //{
        //    var playerController = other.gameObject.GetComponent(typeof(Player)) as Player;
        //    if (!playerController.isAlive) return;
        //    if (UtilController.Instance.ReturnDirection(other.contacts) == HitDirection.Top)
        //        CobraMorta();
        //}
    }

    private void CobraMorta()
    {
        anim.Play(AnimationTagsConstants.Death);
        isDead = true;
    }
    
    void EnemyDie()
    {
        SoundManager.Instance.PlayFxCobraDie();
        Destroy(gameObject);
    }
    
}
