using UnityEngine;

public class CocoController : MonoBehaviour
{
    private int lifeCount = 0;

    // Start is called before the first frame update
    void Start()
    {

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.Rio))
            Destroy(gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.Rio))
            Destroy(gameObject);
    }
}
