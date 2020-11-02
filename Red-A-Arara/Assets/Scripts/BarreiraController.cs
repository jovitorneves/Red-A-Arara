using UnityEngine;

public class BarreiraController : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag(TagsConstants.Player))
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.Player))
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
