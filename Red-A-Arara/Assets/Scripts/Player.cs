using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public int jumpForce;

    public Transform groundCheck;
    public Transform hitEnemy;

    public LayerMask layerGround;
    public LayerMask layerHit;

    public float radiusCheck;
    public float radiusCheckHit;

    private Rigidbody2D rb2D;
    private Animator anim;

    public bool hitted;
    public bool grounded;
    private bool jumping;
    private bool facingRight = true;
    public bool isAlive = true;
    private bool levelCompleted = false;
    private bool timeIsOver = false;

    public AudioClip fxWin;
    public AudioClip fxDie;
    public AudioClip fxJump;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        hitted = Physics2D.OverlapCircle(hitEnemy.position, radiusCheckHit, layerHit);
        grounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, layerGround);

        if (Input.GetButtonDown(InputTagsConstants.Jump) && grounded)
        {
            jumping = true;

            if (isAlive && !levelCompleted)
            {
                SoundManager.Instance.PlayFxPlayer(fxJump);
            }
        }

        if (((int)GameManager.Instance.time <= 0) && !timeIsOver)
        {
            timeIsOver = true;
            PlayerDie();
        }

        PlayAnimations();

    }

    void FixedUpdate()
    {
        
        if (isAlive && !levelCompleted) { 

            float move = Input.GetAxis(InputTagsConstants.Horizontal);

            rb2D.velocity = new Vector2(move * speed, rb2D.velocity.y);

            if ((move < 0 && facingRight) || (move > 0 && !facingRight))
            {
                Flip();
            }
            if (jumping)
            {
                rb2D.AddForce(new Vector2(0f, jumpForce));
                jumping = false;
            }

        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
    }

    void PlayAnimations()
    {
        if (levelCompleted)
        {
            anim.Play(AnimationTagsConstants.Celebrar);
        }
        else if (!isAlive)
        {
            anim.Play(AnimationTagsConstants.Morte);
        }
        else if (grounded && rb2D.velocity.x != 0)
        {
            anim.Play(AnimationTagsConstants.Walk);
        }
        else if (grounded && rb2D.velocity.x == 0)
        {
            anim.Play(AnimationTagsConstants.Idle);
        }
        else if (!grounded)
        {
            anim.Play(AnimationTagsConstants.Jump);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void OnCollisionEnter2D (Collision2D other)
    {

        Debug.Log("DIRECAO PLAYER: " + UtilController.Instance.ReturnDirection(other.contacts));

        if (other.gameObject.CompareTag (TagsConstants.Enemy))
        { 
            if (hitted)
            {
                isAlive = true;
            }
            else
            {
                PlayerDie();
                TakeLife();
            }

        }
        else if (other.gameObject.CompareTag (TagsConstants.Espinhos))
        {
            PlayerDie();
            //TakeLife();
        }
    }

    public void PlayerDie ()
    {
        isAlive = false;
        Physics2D.IgnoreLayerCollision(9, 10);
        SoundManager.Instance.PlayFxPlayer(fxDie);
    }

    void OnTriggerEnter2D (Collider2D other)
    {

        if (other.CompareTag(TagsConstants.Exit))
        {
            levelCompleted = true;
            SoundManager.Instance.PlayFxPlayer(fxWin);
        }
        else if (other.CompareTag(TagsConstants.Rio))
        {
            PlayerDie();
            //TakeLife();
        }
    }

    //Tira uma vida da arara
    private void TakeLife()
    {
        if (GameManager.Instance.heartCount > 0)
        {
            GameManager.Instance.heartCount--;
        }
    }

    void DieAnimationFinished()
    {
        if (timeIsOver)
        {
            GameManager.Instance.SetOverlay(GameStatus.LOSE);
        }
        else
        {
            GameManager.Instance.SetOverlay(GameStatus.DIE);
        }
    }

    void CelebrateAnimationFinished()
    {
        GameManager.Instance.SetOverlay(GameStatus.WIN);
    }
}
