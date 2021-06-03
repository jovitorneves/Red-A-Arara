using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreOneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(TagsConstants.Player))
        {
            GameManager.Instance.heartCount += 1;
            SoundManager.Instance.PlayFxMoreOne();
            Destroy(gameObject);
        }
    }
}
