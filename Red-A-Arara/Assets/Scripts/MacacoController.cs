using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacacoController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D macacoRigidbody;

    [SerializeField]
    private Transform playerTransform;

    private Collision2D collision2DCurrent;

    [SerializeField]
    private float jumpForce = 200f;

    private bool isLookLeft = true;

    private float distancia = 0f;

    private float distanciaMax = 5f;

    private float distanciaMin = -5f;

    private float delayTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        macacoRigidbody = GetComponent<Rigidbody2D>();
        distancia = gameObject.transform.position.x - playerTransform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        PuloRule();
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        collision2DCurrent = collision2D;
        if (distancia >= distanciaMin && distancia <= distanciaMax)
            Invoke("Pulo", delayTime);
    }

    private void PuloRule()
    {
        if (distancia < distanciaMin || distancia > distanciaMax) return;

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
        //Debug.Log("OnCollisionEnter2D TAG: " + collision2D.gameObject.tag);

        if (collision2D.gameObject.CompareTag("chao"))
        {
            Delay();
            macacoRigidbody.AddForce(new Vector2(isLookLeft ? - jumpForce : jumpForce, jumpForce));
        }
    }

    private void Flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x * -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.y);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);
    }

}
