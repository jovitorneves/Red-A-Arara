using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocoController : MonoBehaviour
{
    [SerializeField]
    private bool isCenario = true;
    [SerializeField]
    private Vector3 fixedVector;

    // Start is called before the first frame update
    void Start()
    {
        //fixedVector = new Vector3(-36f, -9f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(TagsConstants.Player))
            if (isCenario)
                Destroy(gameObject);
            else
                gameObject.transform.position = fixedVector;
        if (other.gameObject.CompareTag(TagsConstants.Enemy))
            gameObject.transform.position = fixedVector;
    }
}
