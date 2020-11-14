using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Sprite[] overlaySprites;
    public Image overlay;
    public Text timeHud;
    public Text scoreHud;

    public float time;
    public int score;
    public GameStatus status;
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 50f;
        score = 0;
        status = GameStatus.PLAY;
        overlay.enabled = false;
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (status == GameStatus.PLAY)
        {
            time -= Time.deltaTime;
            int timeInt = (int)time;

            if (timeInt >= 0)
            {
                timeHud.text = "Tempo: " + timeInt.ToString();
                scoreHud.text = "Buritis " + score.ToString();
            }
        }
        else if (Input.GetButtonDown(InputTagsConstants.Jump)) 
        { 
            if (status == GameStatus.WIN)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

        }
    }

    public void SetOverlay(GameStatus parStatus)
    {
        status = parStatus;
        overlay.enabled = true;
        overlay.sprite = overlaySprites[(int)parStatus];
    }
}
