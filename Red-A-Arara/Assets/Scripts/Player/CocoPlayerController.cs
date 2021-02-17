using System;
using System.Collections.Generic;
using UnityEngine;

public class CocoPlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D cocoRB;

    private float force = 100f;
    private float zRotation = 0f;

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

    }

    private void FixedUpdate()
    {
        //if (Input.GetButton("J"))//não deixar o usuario pressionar a tecla infinitamente, pois isso faz com que crie um hiper forca
        //    return;
        if (Input.GetKey(KeyCode.J))
        {
            AplicaForce();
            player.isCoco = false;
        }
    }

    private Tuple<float, float> CalculeTrigonometria()
    {
        float x, y;
        zRotation -= 10f;// 2.5f;

        //Deg2Rad = constante de conversao de graus para radianos
        x = force * (Mathf.Cos(zRotation * Mathf.Deg2Rad));
        y = force * (Mathf.Sin(zRotation * Mathf.Deg2Rad));

        return new Tuple<float, float>(x, y);
    }

    private void AplicaForce()
    {
        AjustaPosicaoCoco();
        Tuple<float, float> tuple;
        tuple = this.CalculeTrigonometria();

        cocoRB.AddForce(new Vector2(tuple.Item1, tuple.Item2));
        //cocoRB.AddForce(new Vector2(0f, force));//testar
    }

    private void AjustaPosicaoCoco()
    {
        Vector3 vetor = new Vector3(0, 0, 0);
        vetor = this.transform.position;
        //faz a bolinha ser lancada um pouco mas a frente para evitar colisao com o personagem
        vetor[0] += 1f;
        cocoRB.transform.position = vetor;
    }
}
