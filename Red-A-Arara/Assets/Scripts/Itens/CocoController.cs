using UnityEngine;

public class CocoController : MonoBehaviour
{
    private int lifeCount = 0;

    private Rigidbody2D cocoRB;
    private BoxCollider2D cocoBoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        cocoRB = GetComponent<Rigidbody2D>();
        cocoBoxCollider = GetComponent<BoxCollider2D>();

        cocoBoxCollider.sharedMaterial.friction = 0f;
        cocoBoxCollider.sharedMaterial.bounciness = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCount >= 2)
            Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(TagsConstants.Enemy) ||
            other.gameObject.CompareTag(TagsConstants.Humano))
            lifeCount++;

        if (other.gameObject.CompareTag(TagsConstants.Rio) ||
            other.gameObject.CompareTag(TagsConstants.Espinhos) ||
            other.gameObject.CompareTag(TagsConstants.Player))
            Destroy(gameObject);

        if (other.gameObject.CompareTag(TagsConstants.Botao))
        {
            cocoBoxCollider.sharedMaterial.friction = 0f;
            cocoBoxCollider.sharedMaterial.bounciness = 0f;
            cocoRB.velocity = new Vector2();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.Rio))
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.Botao))
        {
            cocoBoxCollider.sharedMaterial.friction = 0f;
            cocoBoxCollider.sharedMaterial.bounciness = 0f;
            cocoRB.mass = 5f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.Botao))
        {
            cocoBoxCollider.sharedMaterial.friction = 0f;
            cocoBoxCollider.sharedMaterial.bounciness = 0f;
            cocoRB.mass = 5f;
        }

        if (collision.gameObject.CompareTag(TagsConstants.Rio) ||
            collision.gameObject.CompareTag(TagsConstants.Espinhos) ||
            collision.gameObject.CompareTag(TagsConstants.Player))
            Destroy(gameObject);

    }
}
