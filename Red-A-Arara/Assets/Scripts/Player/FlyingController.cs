using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : MonoBehaviour
{

    public float horizontalSpeed = 0.08f;
    public float verticalSpeed = 1.0f;
    public float height = 1.0f;

    public Vector3 tempPosition;

    // Start is called before the first frame update
    void Start()
    {
        tempPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        return;
        tempPosition.x += horizontalSpeed;
        tempPosition.y += Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * height;
        transform.position = tempPosition;
    }
}
