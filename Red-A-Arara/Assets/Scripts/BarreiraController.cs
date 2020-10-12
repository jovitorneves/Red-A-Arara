using UnityEngine;

public class BarreiraController : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
