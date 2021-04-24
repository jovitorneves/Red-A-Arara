using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RochaParedeController : MonoBehaviour
{
    [SerializeField]
    private GameObject rochaGameObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChamaRocha()
    {
        //Instancia prefab
        Instantiate(rochaGameObject, transform.position, Quaternion.identity);
    }
}
