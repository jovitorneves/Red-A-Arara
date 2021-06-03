using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocoSpawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject cocoGameObject;

    private bool isPassou = false;
    private bool tirouCoco = false;
    //[SerializeField]
    private float SPAWN_COCO_TIME = 2.5f;
    //[SerializeField]
    private readonly float SPAWN_COCO_TIME_DEFAULT = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        //Instancia prefab
        Instantiate(cocoGameObject, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPassou)
        {
            SPAWN_COCO_TIME -= Time.deltaTime;
            if (SPAWN_COCO_TIME <= 0)
            {
                //Instancia prefab
                Instantiate(cocoGameObject, transform.position, Quaternion.identity);
                isPassou = false;
                tirouCoco = false;
                SPAWN_COCO_TIME = SPAWN_COCO_TIME_DEFAULT;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPassou) return;
        if (collision.gameObject.CompareTag(TagsConstants.CocoPartido))
            tirouCoco = true;

        if (collision.gameObject.CompareTag(TagsConstants.Player) && tirouCoco)
            isPassou = true;
    }
}
