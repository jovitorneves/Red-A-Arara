using System.Collections;
using UnityEngine;

public class MacacoController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D macacoRigidbody;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Transform posicaoA, posicaoB;

    private Collision2D collision2DCurrent;

    public Player playerScript;

    [SerializeField]
    private float jumpForce = 300f;

    private MoverMacacoController moverMacacoController;

    private bool isLookLeft = true;

    public bool isJump = false;

    private float distancia = 0f;

    private float distanciaMax = 5f;

    private float distanciaMin = -5f;

    private float delayTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        moverMacacoController = new MoverMacacoController(gameObject, posicaoA, posicaoB);
        macacoRigidbody = GetComponent<Rigidbody2D>();
        playerScript = player.GetComponent<Player>();
        posicaoA.position = new Vector3(posicaoA.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        posicaoB.position = new Vector3(posicaoB.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        CalculaDistanciaPlayer();

        if (playerScript.isAlive)
        {
            if (distancia < distanciaMin ||
                distancia > distanciaMax)
            {
                gameObject.transform.position = moverMacacoController.MoverMacaco();
                gameObject.transform.localScale = moverMacacoController.macacoGameObject.transform.localScale;
            }
            else
            {
                PuloRule();
            }
        }
    }

    //Desenha um risco que liga o Ponto A e o Ponto B
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(posicaoA.position, posicaoB.position);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        collision2DCurrent = collision2D;

        Debug.Log("DIRECAO MACACO: " + UtilController.Instance.ReturnDirection(collision2D.contacts));

        if (collision2D.gameObject.CompareTag("chao") && isJump)
        {
            isJump = false;
        }

        if (distancia >= distanciaMin &&
            distancia <= distanciaMax &&
            playerScript.isAlive &&
            !isJump)
            Invoke("Pulo", delayTime);

    }

    private void CalculaDistanciaPlayer()
    {
        distancia = gameObject.transform.position.x - player.transform.position.x;
    }

    private void PuloRule()
    {
        if ((distancia < distanciaMin ||
            distancia > distanciaMax) &&
            !playerScript.isAlive) return;

        if (!isJump)
            Invoke("Pulo", delayTime);

        if (distancia < 0 && isLookLeft)
        {
            Flip();
        }
        else if (distancia > 0 && !isLookLeft)
        {
            Flip();
        }
    }

    private void Pulo()
    {
        PuloMacaco(collision2DCurrent);
    }

    private void PuloMacaco(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("chao") &&
            !isJump &&
            playerScript.isAlive)
        {
            macacoRigidbody.AddForce(new Vector2(isLookLeft ? - jumpForce : jumpForce, jumpForce));
            isJump = true;
        }
    }

    private void Flip()
    {
        float x = transform.localScale.x * -1;

        isLookLeft = !isLookLeft;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);
    }

}
