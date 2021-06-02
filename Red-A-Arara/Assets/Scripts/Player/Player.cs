using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 3.5f;
    private int jumpForce = 480;

    public Transform groundCheck;
    public Transform hitEnemy;

    public LayerMask layerGround;
    public LayerMask layerHit;

    public float radiusCheck;
    public float radiusCheckHit;

    private Rigidbody2D rb2D;
    private Animator anim;

    private FlyingController flyingController;

    public bool hitted;
    public bool grounded;
    private bool jumping;
    private bool facingRight = true;
    public bool isAlive = true;
    private bool levelCompleted = false;
    private bool timeIsOver = false;
    private bool isHumano = false;

    //DASH
    private bool isDash = false;
    private readonly float speedDash = 5.0f;
    private float DOUBLE_PRESS_TIME = .2f;
    private float lastPressTime;
    private KeyCode lastKeyCode;

    //Dano
    private float delayMorteTime = 3f;
    private bool isRio = false;

    public bool isCoco = false;

    public AudioClip fxWin;
    public AudioClip fxDie;
    public AudioClip fxJump;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        flyingController = GetComponent(typeof(FlyingController)) as FlyingController;
    }

    // Update is called once per frame
    void Update()
    {
        hitted = Physics2D.OverlapCircle(hitEnemy.position, radiusCheckHit, layerHit);
        grounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, layerGround);

        PlayAnimations();

        if (!isAlive)
            return;

        if (GameManager.Instance.isPause) return;

        //DASH
        //if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
        //     Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && !isDash && grounded)
        //{
        //    float timeLastPress = Time.time - lastPressTime;

        //    if (timeLastPress <= DOUBLE_PRESS_TIME)
        //        StartDash();

        //    lastPressTime = Time.time;
        //}

        if (!isDash && grounded)
        {
            SideDash(key: KeyCode.LeftArrow);
            SideDash(key: KeyCode.RightArrow);
            SideDash(key: KeyCode.A);
            SideDash(key: KeyCode.D);
        }

        // if (isDash)
        //     return;

        TiraVidas();

        if ((Input.GetButtonDown(InputTagsConstants.Jump) ||
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow)) &&
            grounded)
        {
            jumping = true;
            isDash = false;

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

        speed = flyingController.isFlying ? 4f : 3.5f;
    }

    void FixedUpdate()
    {
        // print("TEMPO: "+ Time.time);
        //DASH
        //if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
        //     Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && !isDash && grounded)
        //{
        //    float timeLastPress = Time.time - lastPressTime;

        //    if (timeLastPress <= DOUBLE_PRESS_TIME)
        //        StartDash();

        //    lastPressTime = Time.time;
        //}

        if (GameManager.Instance.isPause) return;

        if (isDash)
            return;

        if (isAlive && !levelCompleted) { 

            float move = Input.GetAxis(InputTagsConstants.Horizontal);

            rb2D.velocity = new Vector2(move * speed, rb2D.velocity.y);

            if ((move < 0 && facingRight) || (move > 0 && !facingRight))
                Flip();
            if (jumping)
            {
                rb2D.AddForce(new Vector2(0f, jumpForce));
                jumping = false;
            }
        }
        else
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);

        if (!isAlive)
            return;
    }

    void PlayAnimations()
    {
        if (isDash && !isCoco)
            anim.Play(AnimationTagsConstants.DashRed);
        else if (isDash && isCoco)
            anim.Play(AnimationTagsConstants.DashCocoRed);
        else if (levelCompleted)
            anim.Play(AnimationTagsConstants.WinRed);
        else if (!isAlive)
            anim.Play(AnimationTagsConstants.MorteRed);
        else if (grounded && rb2D.velocity.x != 0 && !isCoco)
            anim.Play(AnimationTagsConstants.AndandoRed);
        else if (grounded && rb2D.velocity.x != 0 && isCoco)
            anim.Play(AnimationTagsConstants.AndandoCocoRed);
        else if (grounded && rb2D.velocity.x == 0 && !isCoco)
            anim.Play(AnimationTagsConstants.ParadaRed);
        else if (grounded && rb2D.velocity.x == 0 && isCoco)
            anim.Play(AnimationTagsConstants.ParadaCocoRed);
        else if (!grounded && !flyingController.isFlying && !isCoco)
            anim.Play(AnimationTagsConstants.PulandoRed);
        else if (!grounded && flyingController.isFlying && !isCoco)
            anim.Play(AnimationTagsConstants.Voando);
        else if (!grounded && !flyingController.isFlying && isCoco)
            anim.Play(AnimationTagsConstants.VoandoCocoRed);

        if (!flyingController.isFlying)
            rb2D.drag = 0;
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (!isAlive) return;

        if (other.gameObject.CompareTag(TagsConstants.Enemy) ||
            other.gameObject.CompareTag(TagsConstants.Humano))
        {
            isHumano = other.gameObject.CompareTag(TagsConstants.Humano);

            var enemyController = other.gameObject.GetComponent(typeof(BaseEnemyController)) as BaseEnemyController;

            if (hitted)
                isAlive = true;
            else
            {
                if (enemyController.isDead )
                    return;
                PlayerDie();
                TakeLife();
            }

        }
        else if (other.gameObject.CompareTag(TagsConstants.Espinhos) ||
                 other.gameObject.CompareTag(TagsConstants.Rocha))
        {
            //DataBase.deleteData("sceneDB");
            PlayerDie();
            TakeLife();
        } else if (other.gameObject.CompareTag(TagsConstants.CocoPartido))
        {
            isCoco = true;
            anim.Play(AnimationTagsConstants.PegandoCocoRed);
            SoundManager.Instance.PlayFxCoco();
        }
    }

    private void SideDash(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            if(DOUBLE_PRESS_TIME > Time.time && lastKeyCode == key)
            {
                StartDash();
            } else
            {
                DOUBLE_PRESS_TIME = Time.time + 0.2f;
            }
            lastKeyCode = key;
        }
    }

    private void StartDash()
    {
        float move = Input.GetAxis(InputTagsConstants.Horizontal);
        isDash = true;

        SoundManager.Instance.PlayFxDash();
        if (move > 0)
            rb2D.velocity = Vector2.right * speedDash;
        else
            rb2D.velocity = Vector2.left * speedDash;

        if ((move < 0 && facingRight) || (move > 0 && !facingRight))
            Flip();
    }

    public void StopDash()
    {
        //rb2D.velocity = Vector2.zero;
        isDash = false;
    }

    public void PlayerDie ()
    {
        isAlive = false;
        isDash = false;
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
            TakeLife();
            isRio = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagsConstants.Rio))
        {
            isRio = false;
        }
    }

    private void TiraVidas()
    {
        if (!isRio) return;

        delayMorteTime -= Time.deltaTime;

        if (delayMorteTime <= 0)
        {
            TakeLife();
            delayMorteTime = 3f;
        }

        if (GameManager.Instance.heartCount <= 0)
            PlayerDie();

    }

    //Tira uma vida da arara
    private void TakeLife()
    {
        if (GameManager.Instance.heartCount > 0)
            GameManager.Instance.heartCount--;

        GameManager.Instance.SaveDataScene();
    }

    void DieAnimationFinished()
    {
        if (timeIsOver)
            GameManager.Instance.SetOverlay(GameStatus.LOSE);
        else
        {
            GameManager.Instance.isHumano = isHumano;
            GameManager.Instance.SetOverlay(GameStatus.DIE);
        }
    }

    void CelebrateAnimationFinished()
    {
        GameManager.Instance.SetOverlay(GameStatus.WIN);
    }
}
