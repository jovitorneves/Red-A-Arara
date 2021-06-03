using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaRochaController : MonoBehaviour
{
    [SerializeField]
    private GameObject rochaParede;
    public RochaParedeController rochaParedeScript;

    private bool isPassou = false;
    [SerializeField]
    private float SPAWN_ROCHA_TIME = 1.5f;
    [SerializeField]
    private float SPAWN_ROCHA_TIME_DEFAULT = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        rochaParedeScript = rochaParede.GetComponent<RochaParedeController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (isPassou)//retirar caso queira q a rocha aparece toda vez q passar
        //{
        //    SPAWN_ROCHA_TIME -= Time.deltaTime;
        //    if (SPAWN_ROCHA_TIME <= 0)
        //    {
        //        rochaParedeScript.ChamaRocha();
        //        isPassou = false;
        //        SPAWN_ROCHA_TIME = SPAWN_ROCHA_TIME_DEFAULT;
        //    }
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPassou) return;
        if (collision.gameObject.CompareTag(TagsConstants.Player))
        {
            isPassou = true;
            rochaParedeScript.ChamaRocha();//remover
        }
    }
}
