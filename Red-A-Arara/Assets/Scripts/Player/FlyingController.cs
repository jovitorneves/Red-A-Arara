using UnityEngine;
using UnityEngine.SceneManagement;

public class FlyingController : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask layerGround;

    private readonly int dragFixed = 8;
    private bool isGrounded;
    private readonly float radiusCheck = 0.5f;

    private float timer = 0.0f;

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
        if (GameManager.Instance.status != GameStatus.PLAY || !player.isAlive)
            return;

        IsFalling();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, layerGround);

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            playerRigidbody.drag = 0;
            player.StopDash();
        }
        else
        {
            if (Input.GetButtonDown(InputTagsConstants.Jump))
                timer += Time.deltaTime;

            if (Input.GetKey(KeyCode.Space) && !isGrounded)
            {
                timer += Time.deltaTime;
                if (timer < 0.2f)
                    return;

                isJumped = true;

                Flying();
                player.StopDash();
            }
            else
            {
                isJumped = false;
                isFlying = false;
                playerRigidbody.drag = 0;
                timer = 0f;
            }
        }
    }

    private void Flying()
    {
        if (isJumped)
            playerRigidbody.drag = isGrounded ? 0 : dragFixed;

        if (isGrounded)
        {
            playerRigidbody.drag = 0;
            isJumped = false;
        }

        if (player.isCoco)
            animationPlayer.Play(AnimationTagsConstants.VoandoCocoRed);
        else
            animationPlayer.Play(AnimationTagsConstants.Voando);

        isFlying = true;
        player.StopDash();
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
