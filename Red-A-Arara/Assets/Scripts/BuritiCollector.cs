using UnityEngine;

public class BuritiCollector : MonoBehaviour
{

    public AudioClip fxCollect;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag(TagsConstants.Player))
        {
            GameManager.Instance.score++;
            SoundManager.Instance.PlayFxBuritiCollector(fxCollect);
            GameManager.Instance.buritiCount++;
            Destroy(gameObject);
        }
    }
}
