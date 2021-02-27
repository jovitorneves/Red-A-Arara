using UnityEngine;

public class CocoController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(TagsConstants.Player) ||
            other.gameObject.CompareTag(TagsConstants.Enemy) ||
            other.gameObject.CompareTag(TagsConstants.Humano))
            Destroy(gameObject);
    }
}
