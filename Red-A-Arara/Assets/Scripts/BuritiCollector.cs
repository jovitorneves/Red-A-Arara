using UnityEngine;

public class BuritiCollector : MonoBehaviour
{
    [SerializeField]
    private bool isMorte = false;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag(TagsConstants.Player))
        {
            SoundManager.Instance.PlayFxBuritiCollector();

            if (isMorte)
            {
                GameManager.Instance.score -= 5;
                GameManager.Instance.buritiCount -= 5;
            }
            else
            {
                GameManager.Instance.score++;
                GameManager.Instance.buritiCount++;
            }
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.CountHeart();
    }
}
