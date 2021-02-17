using System;
using System.Collections.Generic;
using UnityEngine;

public class CocoPlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D cocoRB;

    private float force = 250f;
    private float zRotation = 10f;
    private float zRotation2 = 0f;

    private Player player;

    private float localScaleX = 1f;

    //TEmpoRARIO
    private Vector2 vetor2;
    private float distancia;

    // Start is called before the first frame update
    void Start()
    {
        cocoRB = cocoRB.GetComponent<Rigidbody2D>();
        player = GetComponent(typeof(Player)) as Player;
    }

    // Update is called once per frame
    void Update()
    {
        //AjustaPosicaoCoco();
        Debug.Log("TESTE DE POSISCAO: " + transform.localScale.x);
        localScaleX = transform.localScale.x;

        if (transform.localScale.x == 1)
        {
            force = 250f;
            vetor2 = new Vector3(250f, 250f, 0);
        }
        else
        {
            force = -250f;
            vetor2 = new Vector3(-250f, 250f, 0);
        }
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

    private Tuple<float, float> CalculeTrigonometria()
    {
        float x, y;
        zRotation2 -= zRotation;// 2.5f;

        //Deg2Rad = constante de conversao de graus para radianos
        x = force * (Mathf.Cos(zRotation2 * Mathf.Deg2Rad));
        y = force * (Mathf.Sin(zRotation2 * Mathf.Deg2Rad));

        return new Tuple<float, float>(x, y);
    }

    private void AplicaForce()
    {
        AjustaPosicaoCoco();
        Tuple<float, float> tuple;
        tuple = CalculeTrigonometria();

        //cocoRB.AddForce(new Vector2(tuple.Item1, tuple.Item2));
        cocoRB.AddForce(new Vector2(force, 250f));//testar
        //cocoRB.AddForce(new Vector2(350f, 350f));//FUNCIONOU para direita

        //Direita
        //cocoRB.AddForce(new Vector2(100,100));
        //Esquerda
        //cocoRB.AddForce(vector2);//Vector2(-100,100)



        //Vector3 vetor2 = new Vector3(0, 0, 0);

        //if (transform.localScale.x == 1)
        //    vetor2 = new Vector3(250f, 250f, 0);
        //else
        //    vetor2 = new Vector3(-250f, 250f, 0);


        //cocoRB.AddForce(vetor2);


        //FUNCIONOU PARA ESQUERDA NAO ALTERAR
        //cocoRB.AddForce(new Vector2(-250f, 250f));

        //FUNCIONOU PARA DIREITA NAO ALTERAR
        //cocoRB.AddForce(new Vector2(250f, 250f));
    }

    //faz o coco ser lancada um pouco mas a frente para evitar colisao com o personagem
    private void AjustaPosicaoCoco()
    {
        Vector3 vetor = new Vector3(0, 0, 0);
        vetor = this.transform.position;

        //Direita
        //vetor[0] += 1f;
        //vetor[1] += 1f;

        //esquerda
        //vetor[0] += distancia;//-1
        //vetor[1] += distancia;


        //vetor[0] += distancia;//1 para direita funcionou
        //vetor[1] += distancia;

        vetor[0] += transform.localScale.x == 1 ? 1 : -1;
        vetor[1] += 1f;
        cocoRB.transform.position = vetor;








        //if (localScaleX == 1)
        //    vetor[0] += -1;
        //else
        //    vetor[0] += 1;

        //vetor[1] += 1f;
        //cocoRB.transform.position = vetor;




        //FUNCIONOU PARA ESQUERDA NAO ALTERAR
        //vetor[0] += -1;
        //vetor[1] += 1f;
        //cocoRB.transform.position = vetor;


        //FUNCIONOU PARA DIREITA NAO ALTERAR
        //vetor[0] += 1;
        //vetor[1] += 1f;
        //cocoRB.transform.position = vetor;
    }
}
