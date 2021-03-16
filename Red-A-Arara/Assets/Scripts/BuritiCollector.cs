using UnityEngine;

public class BuritiCollector : MonoBehaviour
{

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag(TagsConstants.Player))
        {
            GameManager.Instance.score++;
            SoundManager.Instance.PlayFxBuritiCollector();
            GameManager.Instance.buritiCount++;
            Destroy(gameObject);
        }
    }
}
