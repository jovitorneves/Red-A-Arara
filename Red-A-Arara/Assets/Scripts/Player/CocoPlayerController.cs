using UnityEngine;

public class CocoPlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D cocoRB;

    private float force = 250f;
    private readonly float forceFixed = 250f;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        cocoRB = cocoRB.GetComponent<Rigidbody2D>();
        player = GetComponent(typeof(Player)) as Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x == 1)
            force = 250f;
        else
            force = -250f;
    }

    private void FixedUpdate()
    {
        //if (Input.GetButton("J"))//não deixar o usuario pressionar a tecla infinitamente, pois isso faz com que crie um hiper forca
        //    return;
        if (!player.isCoco)
            return;
        if (Input.GetKey(KeyCode.J))
        {
            AplicaForce();
            player.isCoco = false;
        }
    }

    private void AplicaForce()
    {
        AjustaPosicaoCoco();
        cocoRB.AddForce(new Vector2(force, forceFixed));
    }

    //faz o coco ser lancado um pouco mas a frente para evitar colisao com o personagem
    private void AjustaPosicaoCoco()
    {
        Vector3 vetor = new Vector3(0, 0, 0);

        vetor = transform.position;
        vetor[0] += transform.localScale.x == 1 ? 1 : -1;
        vetor[1] += 1f;
        cocoRB.transform.position = vetor;
    }
}
