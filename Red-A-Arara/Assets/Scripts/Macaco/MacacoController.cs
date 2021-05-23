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

    //[SerializeField]
    private float hitPoints;
    //[SerializeField]
    private float maxHitPoints = 3;
    [SerializeField]
    private HealthbarController healthbar;

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
    private int cocoCount = 0;
    private float delayAtordoadoTime = 5f;
    private float disA;
    private float disB;

    // Start is called before the first frame update
    void Start()
    {
        macacoRigidbody = GetComponent<Rigidbody2D>();
        playerScript = player.GetComponent<Player>();
        animator = GetComponent<Animator>();
        hitPoints = maxHitPoints;
        healthbar.SetHealth(hitPoints, maxHitPoints);
        delayAtordoadoTime = Time.deltaTime * 5;

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

        if (isDead) return;

        disA = Vector2.Distance(posicaoA.gameObject.transform.position, gameObject.transform.position);
        disB = Vector2.Distance(posicaoB.gameObject.transform.position, gameObject.transform.position);

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

        MoveMonkey();

        //Mecanica do pulo desativada
        //if (disA <= 2f ||
        //    disB <= 2f)
        //{
        //    MoveMonkey();
        //}
        //else if (player.gameObject.transform.position.x >= posicaoA.position.x &&
        //    player.gameObject.transform.position.x <= posicaoB.position.x &&
        //    disA >= distanciaMonkeyAndPointMax &&
        //    disB >= distanciaMonkeyAndPointMax)
        //    PuloRule();
        //else
        //    MoveMonkey();
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        if (isAtordoada) return;
        IsJumping();
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
                    MacacoMorto();
                else
                    SoundManager.Instance.PlayDanoInimigo();
            }
        }
        if (collision2D.gameObject.CompareTag(TagsConstants.CocoPartido) && !isBoss)
        {
            if (!isAtordoada)
                cocoCount++;

            animator.Play(AnimationTagsConstants.AtordoadoMacaco);
            isAtordoada = true;
            delayAtordoadoTime = Time.deltaTime * 5;
            SoundManager.Instance.PlayFxAtordoado();

            hitPoints -= 2;
            healthbar.SetHealth(hitPoints, maxHitPoints);
            if (hitPoints <= 0)
                MacacoMorto();
        }
        if (collision2D.gameObject.CompareTag(TagsConstants.Rio) ||
            collision2D.gameObject.CompareTag(TagsConstants.Espinhos))
            MacacoMorto();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagsConstants.Rio) ||
            other.CompareTag(TagsConstants.Espinhos))
            MacacoMorto();
    }

    private void MacacoMorto()
    {
        animator.Play(AnimationTagsConstants.MortoMacaco);
        isDead = true;
        SoundManager.Instance.PlayFxCobraDie();
    }

    public void DestroyMacaco()
    {
        Destroy(gameObject);
    }

    private void DistanciaPlayerIntervalMacaco()
    {
        distancia = Vector2.Distance(gameObject.transform.position, player.transform.position);
    }

    private void MoveMonkey()
    {
        if (isJump) return;

        isLookLeft = !moverMacacoController.isLookLeft;
        gameObject.transform.position = moverMacacoController.MoverMacaco(gameObject.transform.position);
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
                SoundManager.Instance.PlayFxJumpEnemy();
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
