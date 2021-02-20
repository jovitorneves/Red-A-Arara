using UnityEngine;

public class CocoPlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject cocoGameObject;

    private float force = 250f;
    private readonly float forceFixed = 250f;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKey(KeyCode.J) && player.isCoco && player.isAlive)
        {
            AjustaPosicaoCoco();
            player.isCoco = false;
        }
    }

    //faz o coco ser lancado um pouco mas a frente para evitar colisao com o personagem
    private void AjustaPosicaoCoco()
    {
        Vector3 vetor = transform.position;

        vetor[0] += transform.localScale.x == 1 ? 1 : -1;
        vetor[1] += 1f;

        //Instancia prefab 
        GameObject cocoGO = Instantiate(cocoGameObject, vetor, Quaternion.identity);

        //Aplica Force
        Rigidbody2D cocoRB = cocoGO.GetComponent<Rigidbody2D>();
        cocoRB.AddForce(new Vector2(force, forceFixed));
    }
}
