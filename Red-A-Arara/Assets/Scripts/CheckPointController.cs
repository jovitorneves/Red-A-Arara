using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyGameObject;

    private BaseEnemyController enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        if (!enemyGameObject.Equals(null))
        {
            enemyScript = enemyGameObject.GetComponent<BaseEnemyController>();
            if (!enemyScript.Equals(null))
                gameObject.GetComponent<BoxCollider2D>().isTrigger = !enemyScript.isBoss;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyGameObject.Equals(null) && !enemyScript.Equals(null))
        {
            if (enemyScript.isDead && enemyScript.isBoss)
            {
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
    }
}
