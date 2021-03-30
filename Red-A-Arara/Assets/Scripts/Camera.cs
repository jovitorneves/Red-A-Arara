using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform background;
    [SerializeField]
    private Vector2 smoothTime;
    [SerializeField]
    private Vector2 maxLimit;
    [SerializeField]
    private Vector2 minLimit;
    [SerializeField]
    private float eixoY = -0.35f;

    [SerializeField]
    private Transform posicaoA, posicaoB;

    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        LoadedData();
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        background.position = new Vector3(player.position.x, player.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (player.Equals(null)) return;

        CenarioAntigo();
        CenarioNovo();
    }

    private void CenarioAntigo()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.position.x, ref velocity.x, smoothTime.x);
        float posY = Mathf.SmoothDamp(transform.position.y, player.position.y, ref velocity.y, smoothTime.y);

        transform.position = new Vector3(posX, posY, transform.position.z);

        float posNewX = Mathf.Clamp(transform.position.x, minLimit.x, maxLimit.x);
        float posNewY = Mathf.Clamp(transform.position.y, minLimit.y, maxLimit.y);

        transform.position = new Vector3(posNewX, posNewY, transform.position.z);
    }

    private void CenarioNovo()
    {
        if (background.Equals(null)) return;

        float posX = Mathf.SmoothDamp(transform.position.x, player.position.x, ref velocity.x, smoothTime.x);
        float posY = Mathf.SmoothDamp(transform.position.y, player.position.y, ref velocity.y, smoothTime.y);

        background.position = new Vector3(posX, posY, 0);

        float posNewX = Mathf.Clamp(transform.position.x, minLimit.x, maxLimit.x);
        float posNewY = Mathf.Clamp(transform.position.y, minLimit.y, maxLimit.y);
        //float posNeyModifedY = ((posNewY * -0.35f) < maxLimit.y && (posNewY * -0.35f) > minLimit.y) ? (posNewY * -0.35f) : posNewY;

        background.position = new Vector3(posNewX, (posNewY * eixoY), 0);
    }

    //Desenha um risco que liga o Ponto A e o Ponto B
    private void OnDrawGizmos()
    {
        if (posicaoA.Equals(null) ||
            posicaoB.Equals(null))
            return;

        Gizmos.DrawLine(posicaoA.position, posicaoB.position);
    }

    //Carrega os dados da cena anterior
    private void LoadedData()
    {
        SceneDB loadedData = DataBase.loadData<SceneDB>("sceneDB");
        if (loadedData == null) { return; }

        GameManager.Instance.heartCount = loadedData.heartCount;
    }
}
