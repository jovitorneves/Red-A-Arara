using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;

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

    private static bool destroyed = false;
    private Stack<int> loadedLevels;
    private bool isPause = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            loadedLevels = new Stack<int>();
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
        PauseGameAction();
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
                LoadScene(buildIndex: SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                LoadScene(buildIndex: SceneManager.GetActiveScene().buildIndex);
            }

        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Application.CanStreamedLevelBeLoaded("Menu"))
            {
                LoadScene(sceneName: "Menu");
            }
        }

        PauseGameAction();
    }

    //Habilita ou desabilita a cena, passando ela por parametro
    void SetScene(string sceneName, bool sceneEnabled)
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        foreach (EditorBuildSettingsScene scene in scenes)
        {
            if (scene.path.Contains(sceneName))
            {
                scene.enabled = sceneEnabled;
            }
        }
        EditorBuildSettings.scenes = scenes;
    }

    private void PauseGameAction()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPause)
            {
                ResumeGame();
                isPause = false;
            }
            else
            {
                PauseGame();
                isPause = true;
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void SetOverlay(GameStatus parStatus)
    {
        status = parStatus;
        overlay.enabled = true;
        overlay.sprite = overlaySprites[(int)parStatus];
    }

    public static Scene GetActiveScene()
    {
        return SceneManager.GetActiveScene();
    }

    public static void LoadScene(int buildIndex)
    {
        Instance.loadedLevels.Push(GetActiveScene().buildIndex);
        SceneManager.LoadScene(buildIndex);
    }

    public static void LoadScene(string sceneName)
    {
        Instance.loadedLevels.Push(GetActiveScene().buildIndex);
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadPreviousScene()
    {
        if (Instance.loadedLevels.Count > 0)
        {
            SceneManager.LoadScene(Instance.loadedLevels.Pop());
        }
        else
        {
            Debug.LogError("No previous scene loaded");
        }
    }

    private void OnApplicationQuit()
    {
        destroyed = true;
    }

    private void OnDestroy()
    {
        destroyed = true;
    }
}
