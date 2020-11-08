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

    private Collision2D collision2DCurrent;
    private Animator animator;

    public Player playerScript;

    [SerializeField]
    private float jumpForce = 300f;

    private MoverMacacoController moverMacacoController;

    [SerializeField]
    private bool isLookLeft = false;

    public bool isJump = false;

    private float distancia = 0f;

    private readonly float distanciaMonkeyAndPointMax = 2f;

    private readonly float distanciaMax = 3f;

    private readonly float distanciaMin = -3f;

    private readonly float delayTime = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        macacoRigidbody = GetComponent<Rigidbody2D>();
        playerScript = player.GetComponent<Player>();
        animator = GetComponent<Animator>();

        if (posicaoA.Equals(null) || posicaoB.Equals(null))
            return;

        //moverMacacoController = ScriptableObject.CreateInstance<MoverMacacoController>();//recomendado pela unity, se der error ao mover o macaco substituir pelo de baixo
        moverMacacoController = new MoverMacacoController(gameObject, posicaoA, posicaoB);
        posicaoA.position = new Vector3(posicaoA.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        posicaoB.position = new Vector3(posicaoB.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
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

        if (playerScript.isAlive)
        {
            if (player.gameObject.transform.position.x >= posicaoA.position.x &&
                player.gameObject.transform.position.x <= posicaoB.position.x)
            {
                if (disA >= distanciaMonkeyAndPointMax &&
                    disB >= distanciaMonkeyAndPointMax)
                    PuloRule();
                else
                    MoveMonkey();
            }
            else
                MoveMonkey();
        }
    }

    private void FixedUpdate()
    {
        PlayAnimations();
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        collision2DCurrent = collision2D;

        Debug.Log("DIRECAO MACACO: " + UtilController.Instance.ReturnDirection(collision2D.contacts));

        if (collision2D.gameObject.CompareTag(TagsConstants.Chao) && isJump)
        {
            isJump = false;
        }

        if (player.gameObject.transform.position.x >= posicaoA.position.x &&
            player.gameObject.transform.position.x <= posicaoB.position.x &&
            playerScript.isAlive &&
            !isJump)
            Invoke(MethodNameTagsConstants.Pulo, delayTime);

    }

    private void PlayAnimations()
    {
        if (isJump)
            animator.Play(AnimationTagsConstants.Jump);
        else
            animator.Play(AnimationTagsConstants.Walk);
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
    }

    private void PuloRule()
    {
        if (!playerScript.isAlive) return;

        if (player.gameObject.transform.position.x < posicaoA.position.x ||
            player.gameObject.transform.position.x > posicaoB.position.x)
            return;

        if (!isJump)
            Invoke(MethodNameTagsConstants.Pulo, delayTime);

        if (distancia < 0 && isLookLeft)//direita
            Flip();
        else if (distancia > 0 && !isLookLeft)//esquerda
            Flip();
    }

    private void Pulo()
    {
        PuloMacaco(collision2DCurrent);
    }

    private void PuloMacaco(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(TagsConstants.Chao) &&
            !isJump &&
            playerScript.isAlive)
        {
            macacoRigidbody.AddForce(new Vector2(isLookLeft ? - jumpForce : jumpForce, jumpForce));
            isJump = true;
        }
    }

    private void Flip()
    {
        float x = isLookLeft ? -1 : 1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        isLookLeft = !isLookLeft;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);
    }

}
