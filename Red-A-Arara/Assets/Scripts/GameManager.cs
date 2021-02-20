using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public Text timeHud;
    public Text scoreHud;
    public Text faseHud;

    //PopUp
    public Text msgPopUp;
    public GameObject popUpGO;

    public float time;
    public int score;
    public GameStatus status = GameStatus.PLAY;
    public static GameManager Instance;

    private static bool destroyed = false;
    private Stack<int> loadedLevels;
    public bool isPause = false;
    public int buritiCount = 0;
    public int heartCount = 0;
    private bool isBackToFirstStage = false;
    private bool isBonusLife = false;

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
        if (SceneManager.GetActiveScene().buildIndex == 7)
            time = 200f;
        else
            time = 50f;
        score = 0;
        status = GameStatus.PLAY;
        popUpGO.SetActive(false);
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
                faseHud.text = "Fase: " + SceneManager.GetActiveScene().buildIndex;
            }
        }
        else if (Input.GetButtonDown(InputTagsConstants.Jump)) 
        { 
            if (status == GameStatus.WIN)
            {
                LoadScene(buildIndex: SceneManager.GetActiveScene().buildIndex + 1);
                ActivePhases();
            }
            else if (status == GameStatus.DIE || status == GameStatus.LOSE)
            {
                DataBase.deleteData("sceneDB");
                if (isBackToFirstStage)
                    LoadScene(buildIndex: 1);
                else
                    LoadScene(buildIndex: SceneManager.GetActiveScene().buildIndex);
            }

        }
        CountHeart();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && status == GameStatus.PLAY)
        {
            if (Application.CanStreamedLevelBeLoaded("Menu"))
            {
                LoadScene(sceneName: "Menu");
            }
        }

        PauseGameAction();
        SetBackToFirstStage();
    }

    private void ActivePhases()
    {
        var model = new ActivePhasesDB
        {
            activePhases = SceneManager.GetActiveScene().buildIndex
        };

        //Save data from PlayerInfo to a file named players
        DataBase.saveData(model, "activePhasesDB");
    }

    //verifica se o usuario tinha mais de uma vida e se pedeu todas
    private void SetBackToFirstStage()
    {
        if (heartCount > 0)
            isBonusLife = true;

        if (isBonusLife && heartCount == 0)
            isBackToFirstStage = true;
        else
            isBackToFirstStage = false;
    }

    private void CountHeart()
    {
        if (buritiCount == 10)
        {
            if (heartCount >= 3)
                heartCount = 3;
            else
                heartCount++;
            buritiCount = 0;
        }
    }

    //DATA BASE
    public void SaveDataScene()
    {
        var model = new SceneDB
        {
            time = this.time,
            fase = GetActiveScene().buildIndex,
            score = score,
            buritiCount = buritiCount,
            heartCount = heartCount
        };

        //Save data from PlayerInfo to a file named players
        DataBase.saveData(model, "sceneDB");
    }

    //Habilita ou desabilita a cena, passando ela por parametro
    void SetScene(string sceneName, bool sceneEnabled)
    {
        #if UNITY_EDITOR
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
       
        foreach (EditorBuildSettingsScene scene in scenes)
        {
            if (scene.path.Contains(sceneName))
                scene.enabled = sceneEnabled;
        }
        EditorBuildSettings.scenes = scenes;
        #endif
    }

    private void PauseGameAction()
    {
        if (Input.GetKeyDown(KeyCode.P) && status == GameStatus.PLAY)
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
        popUpGO.SetActive(true);
        if (parStatus == GameStatus.WIN) 
            msgPopUp.text = "voce concluiu a fase";
        else
            msgPopUp.text = "voce morreu!";
    }

    public static Scene GetActiveScene()
    {
        return SceneManager.GetActiveScene();
    }

    public static void LoadScene(int buildIndex)
    {
        Instance.loadedLevels.Push(GetActiveScene().buildIndex);
        SceneManager.LoadScene(buildIndex);
        Instance.SaveDataScene();
    }

    public static void LoadScene(string sceneName)
    {
        Instance.loadedLevels.Push(GetActiveScene().buildIndex);
        SceneManager.LoadScene(sceneName);
        Instance.SaveDataScene();
    }

    public static void LoadPreviousScene()
    {
        if (Instance.loadedLevels.Count > 0)
        {
            DataBase.deleteData("sceneDB");
            SceneManager.LoadScene(Instance.loadedLevels.Pop());
        }
        else
            Debug.LogError("No previous scene loaded");
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
