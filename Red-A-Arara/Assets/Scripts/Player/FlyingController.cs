using UnityEngine;

//Colocar um tempo de 3 segundos falando que o usuario pode pular novamente na HUD
public class FlyingController : MonoBehaviour
{

    private Rigidbody2D playerRigidbody;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask layerGround;

    private int jumpCount = 10;
    private int jumpValidCount = 9;
    private readonly int jumpFixedForce = 500;
    private readonly int dragFixed = 8;
    private readonly int jumpForce = 500;
    private bool isGrounded;
    private readonly float radiusCheck = 0.5f;
    private readonly int limitJumps = 10;

    private float timer = 0.0f;
    private readonly float waitTime = 0.10f;

    public bool isFlying = false;
    private bool isCaindo = false;
    private bool isJumped = false;
    private Animator animationPlayer;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        animationPlayer = GetComponent<Animator>();
        player = GetComponent(typeof(Player)) as Player;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (GameManager.Instance.status != GameStatus.PLAY || !player.isAlive)
            return;

        IsFalling();
        if (isFlying)
        {
            if (Input.GetButtonDown(InputTagsConstants.Jump)) {
                isFlying = false;
                playerRigidbody.drag = 0;
            } else if (isGrounded)
            {
                isFlying = false;
                playerRigidbody.drag = 0;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance.status != GameStatus.PLAY)
            return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, layerGround);

        //caso ele aperta a seta pra baixo ele cai mais rapido
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            playerRigidbody.drag = 0;
        else
        {
            //Input.GetKey(KeyCode.Space)
            if (jumpCount == limitJumps)
            {
                if (Input.GetKey(KeyCode.K))
                {
                    Flying();
                    isJumped = true;
                    timer = 0f;
                }
            }
            else if (jumpCount >= 0 && jumpCount <= jumpValidCount)
            {
                if (Input.GetKey(KeyCode.K) && timer > waitTime)
                {
                    Flying();
                    isJumped = true;
                    timer = 0f;
                }
            }
        }
    }

    private void Flying()
    {
        if (isJumped)
            playerRigidbody.drag = isGrounded ? 0 : dragFixed;

        if (jumpCount == 0 && isGrounded)
        {
            playerRigidbody.drag = 0;
            jumpCount = limitJumps;
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

        if (player.isCoco)
            animationPlayer.Play(AnimationTagsConstants.VoandoCocoRed);
        else
            animationPlayer.Play(AnimationTagsConstants.Voando);

        isFlying = true;
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
            isCaindo = true;
        else
            isCaindo = false;
    }
}
