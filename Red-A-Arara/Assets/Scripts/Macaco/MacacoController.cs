using System.Collections;
using UnityEngine;

public class MacacoController : BaseEnemyController
{
    [SerializeField]
    private Rigidbody2D macacoRigidbody;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Transform posicaoA, posicaoB;

    private Animator animator;

    public Player playerScript;

    [SerializeField]
    private float jumpForce = 300f;

    private MoverMacacoController moverMacacoController;

    [SerializeField]
    private bool isLookLeft = false;

    public bool isJump = false;

    private float distancia = 0f;

    private readonly float distanciaMonkeyAndPointMax = 0.1f;

    private float delayTime = 2f;
    private bool isAtordoada = false;
    private float delayAtordoadoTime;

    // Start is called before the first frame update
    void Start()
    {
        macacoRigidbody = GetComponent<Rigidbody2D>();
        playerScript = player.GetComponent<Player>();
        animator = GetComponent<Animator>();
        delayAtordoadoTime = Time.deltaTime * 220f;

        if (posicaoA.Equals(null) || posicaoB.Equals(null))
            return;

        moverMacacoController = new MoverMacacoController(gameObject, posicaoA, posicaoB);
        posicaoA.position = new Vector3(posicaoA.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        posicaoB.position = new Vector3(posicaoB.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        animator.Play(AnimationTagsConstants.Walk);
    }

    // Update is called once per frame
    void Update()
    {
        if (posicaoA.Equals(null) || posicaoB.Equals(null))
            return;

        float disA = gameObject.transform.position.x - posicaoA.position.x;
        float disB = gameObject.transform.position.x - posicaoB.position.x;
        disA = disA > 0 ? (disA * 1) : disA * -1;
        disB = disB > 0 ? (disB * 1) : disB * -1;

        DistanciaPlayerIntervalMacaco();

        delayTime -= Time.deltaTime;

        if (isAtordoada)
            delayAtordoadoTime -= Time.deltaTime;

        if (!playerScript.isAlive) return;

        if (delayTime <= 0 && isAtordoada)
        {
            isAtordoada = false;
            animator.Play(AnimationTagsConstants.Walk);
        }

        if (isAtordoada) return;

        if (player.gameObject.transform.position.x >= posicaoA.position.x &&
            player.gameObject.transform.position.x <= posicaoB.position.x &&
            disA >= distanciaMonkeyAndPointMax &&
            disB >= distanciaMonkeyAndPointMax)
            PuloRule();
        else
            MoveMonkey();
    }

    private void FixedUpdate()
    {
        IsJumping();
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(TagsConstants.Player))
            animator.Play(AnimationTagsConstants.MortoMacaco);
        if (collision2D.gameObject.CompareTag(TagsConstants.CocoPartido) && !isBoss)
        {
            animator.Play(AnimationTagsConstants.AtordoadoMacaco);
            isAtordoada = true;
            delayTime = Time.deltaTime * 220f;
        }
    }

    private void DistanciaPlayerIntervalMacaco()
    {
        distancia = gameObject.transform.position.x - player.transform.position.x;
    }

    private void MoveMonkey()
    {
        if (isJump) return;

        isLookLeft = !moverMacacoController.isLookLeft;
        gameObject.transform.position = moverMacacoController.MoverMacaco(gameObject.transform.position);
        gameObject.transform.localScale = moverMacacoController.macacoGameObject.transform.localScale;
        animator.Play(AnimationTagsConstants.Walk);
    }

    private void PuloRule()
    {
        if (!playerScript.isAlive) return;

        if (player.gameObject.transform.position.x < posicaoA.position.x ||
            player.gameObject.transform.position.x > posicaoB.position.x)
            return;

        if (!isJump)
            if (delayTime <= 0)
            {
                macacoRigidbody.AddForce(new Vector2(isLookLeft ? -jumpForce : jumpForce, jumpForce));
                delayTime = 2f;
                animator.Play(AnimationTagsConstants.Jump);
            } else
                animator.Play(AnimationTagsConstants.Walk);

        if (distancia < 0 && isLookLeft)//direita
            Flip();
        else if (distancia > 0 && !isLookLeft)//esquerda
            Flip();
    }

    private void Flip()
    {
        float x = isLookLeft ? -1 : 1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        isLookLeft = !isLookLeft;
    }

    private void IsJumping()
    {
        if (macacoRigidbody.velocity.y < -0.1)
            isJump = true;
        else 
            isJump = false;
    }

}
