using UnityEngine;

public class CobraChefe : BaseEnemyController
{
    private float speed = 4f;
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

    private bool facingRight = false;
    private bool isVisible = false;

    [SerializeField]
    private GameObject iconeGameObject;
    [SerializeField]
    private GameObject heart1GameObject;
    [SerializeField]
    private GameObject heart2GameObject;
    [SerializeField]
    private GameObject heart3GameObject;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isBoss = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        grounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, layerGround);
        groundedHorizontal = Physics2D.OverlapCircle(groundCheckHorizontal.position, radiusCheckHorizontal, layerGround);

        if ((!grounded) || (groundedHorizontal))
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;
        if (isVisible)
        {
            rb2D.velocity = new Vector2(-speed, rb2D.velocity.y);
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
        Invoke(MethodNameTagsConstants.MoveEnemy, 1f);
    }

    void OnBecameInvisible()
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
            var playerController = other.gameObject.GetComponent(typeof(Player)) as Player;
            if (!playerController.isAlive) return;
            damageTaken += 1;

            if (damageTaken >= 3)
            {
                isDead = true;
                SoundManager.Instance.PlayFxCobraDie();
                anim.Play(AnimationTagsConstants.Death);
                //EnemyDie();
            }
            else
            {
                speed *= 2f;
                anim.Play(AnimationTagsConstants.LevandoDano);
                SoundManager.Instance.PlayFxCobraChefeDamageTaken();
                Invoke(MethodNameTagsConstants.MoveEnemy, 1f);
            }
            Hearts();
        }
    }

    private void Hearts()
    {
        if (damageTaken == 1)
        {
            heart3GameObject.SetActive(false);
        } else if (damageTaken == 2)
        {
            heart2GameObject.SetActive(false);
        } else if (damageTaken >= 3)
        {
            heart1GameObject.SetActive(false);
            iconeGameObject.SetActive(false);
        }    
    }
    
    void EnemyDie()
    {
        gameObject.SetActive(false);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        Destroy(gameObject);
    }
    
}
