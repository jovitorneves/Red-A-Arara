using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    //Labels
    [SerializeField]
    private Text fase1textMeshPro;
    [SerializeField]
    private Text fase2textMeshPro;
    [SerializeField]
    private Text fase3textMeshPro;
    [SerializeField]
    private Text fase4textMeshPro;
    [SerializeField]
    private Text fase5textMeshPro;
    [SerializeField]
    private Text fase6textMeshPro;
    [SerializeField]
    private Text fase7textMeshPro;
    [SerializeField]
    private Text fase8textMeshPro;
    [SerializeField]
    private Text fase9textMeshPro;
    [SerializeField]
    private Text fase10textMeshPro;
    [SerializeField]
    private Text fase11textMeshPro;
    [SerializeField]
    private Text fase12textMeshPro;

    //Botoes
    [SerializeField]
    private Button fase1Button;
    [SerializeField]
    private Button fase2Button;
    [SerializeField]
    private Button fase3Button;
    [SerializeField]
    private Button fase4Button;
    [SerializeField]
    private Button fase5Button;
    [SerializeField]
    private Button fase6Button;
    [SerializeField]
    private Button fase7Button;
    [SerializeField]
    private Button fase8Button;
    [SerializeField]
    private Button fase9Button;
    [SerializeField]
    private Button fase10Button;
    [SerializeField]
    private Button fase11Button;
    [SerializeField]
    private Button fase12Button;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.LoadPreviousScene();
        }

        ActiveButtons();
    }

    public void PlayGame()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        LoadSom();
    }

    public void QuitGame()
    {
        DataBase.deleteData("sceneDB");
        Application.Quit();
    }

    //Fases
    public void Fase1Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(1);
        LoadSom();
    }

    public void Fase2Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(2);
        LoadSom();
    }

    public void Fase3Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(3);
        LoadSom();
    }

    public void Fase4Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(4);
        LoadSom();
    }

    public void Fase5Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(5);
        LoadSom();
    }

    public void Fase6Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(6);
        LoadSom();
    }

    public void Fase7Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(7);
        LoadSom();
    }

    public void Fase8Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(8);
        LoadSom();
    }

    public void Fase9Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(9);
        LoadSom();
    }

    public void Fase10Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(10);
        LoadSom();
    }

    public void Fase11Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(11);
        LoadSom();
    }

    public void Fase12Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(12);
        LoadSom();
    }

    //Som
    private void LoadSom()
    {
        SoundDB loadedData = DataBase.loadData<SoundDB>("soundDB");
        if (loadedData == null)
        {
            var model = new SoundDB
            {
                ambiente = 0.055f,
                buriti = 0.051f,
                cobraAttack = 0.555f,
                cobraChefeDamageTaken = 0.318f,
                cobraDie = 0.348f,
                player = 0.055f
            };

            //Save data from PlayerInfo to a file named players
            DataBase.saveData(model, "soundDB");
        }
    }

    //Botoes das fases
    private void ActiveButtons()
    {
        Color colorTop = new Color(194, 232, 212);
        Color colorBottom = new Color(95, 152, 201);

        ActivePhasesDB loadedData = DataBase.loadData<ActivePhasesDB>("activePhasesDB");
        if (loadedData == null)
        {
            fase1textMeshPro.color = Color.white;
            fase1Button.interactable = false;
            fase2textMeshPro.color = Color.white;
            fase2Button.interactable = false;
            fase3textMeshPro.color = Color.white;
            fase3Button.interactable = false;
            fase4textMeshPro.color = Color.white;
            fase4Button.interactable = false;
            fase5textMeshPro.color = Color.white;
            fase5Button.interactable = false;
            fase6textMeshPro.color = Color.white;
            fase6Button.interactable = false;
            fase7textMeshPro.color = Color.white;
            fase7Button.interactable = false;
            fase8textMeshPro.color = Color.white;
            fase8Button.interactable = false;
            fase9textMeshPro.color = Color.white;
            fase9Button.interactable = false;
            fase10textMeshPro.color = Color.white;
            fase10Button.interactable = false;
            fase11textMeshPro.color = Color.white;
            fase11Button.interactable = false;
            fase12textMeshPro.color = Color.white;
            fase12Button.interactable = false;
        } else
        {
            if (loadedData.activePhases <= 1)
            {
                fase2textMeshPro.color = Color.white;
                fase2Button.interactable = false;
                fase3textMeshPro.color = Color.white;
                fase3Button.interactable = false;
                fase4textMeshPro.color = Color.white;
                fase4Button.interactable = false;
                fase5textMeshPro.color = Color.white;
                fase5Button.interactable = false;
                fase6textMeshPro.color = Color.white;
                fase6Button.interactable = false;
                fase7textMeshPro.color = Color.white;
                fase7Button.interactable = false;
                fase8textMeshPro.color = Color.white;
                fase8Button.interactable = false;
                fase9textMeshPro.color = Color.white;
                fase9Button.interactable = false;
                fase10textMeshPro.color = Color.white;
                fase10Button.interactable = false;
                fase11textMeshPro.color = Color.white;
                fase11Button.interactable = false;
                fase12textMeshPro.color = Color.white;
                fase12Button.interactable = false;
            } else if (loadedData.activePhases <= 2)
            {
                fase3textMeshPro.color = Color.white;
                fase3Button.interactable = false;
                fase4textMeshPro.color = Color.white;
                fase4Button.interactable = false;
                fase5textMeshPro.color = Color.white;
                fase5Button.interactable = false;
                fase6textMeshPro.color = Color.white;
                fase6Button.interactable = false;
                fase7textMeshPro.color = Color.white;
                fase7Button.interactable = false;
                fase8textMeshPro.color = Color.white;
                fase8Button.interactable = false;
                fase9textMeshPro.color = Color.white;
                fase9Button.interactable = false;
                fase10textMeshPro.color = Color.white;
                fase10Button.interactable = false;
                fase11textMeshPro.color = Color.white;
                fase11Button.interactable = false;
                fase12textMeshPro.color = Color.white;
                fase12Button.interactable = false;
            } else if (loadedData.activePhases <= 3)
            {
                fase4textMeshPro.color = Color.white;
                fase4Button.interactable = false;
                fase5textMeshPro.color = Color.white;
                fase5Button.interactable = false;
                fase6textMeshPro.color = Color.white;
                fase6Button.interactable = false;
                fase7textMeshPro.color = Color.white;
                fase7Button.interactable = false;
                fase8textMeshPro.color = Color.white;
                fase8Button.interactable = false;
                fase9textMeshPro.color = Color.white;
                fase9Button.interactable = false;
                fase10textMeshPro.color = Color.white;
                fase10Button.interactable = false;
                fase11textMeshPro.color = Color.white;
                fase11Button.interactable = false;
                fase12textMeshPro.color = Color.white;
                fase12Button.interactable = false;
            } else if (loadedData.activePhases <= 4)
            {
                fase5textMeshPro.color = Color.white;
                fase5Button.interactable = false;
                fase6textMeshPro.color = Color.white;
                fase6Button.interactable = false;
                fase7textMeshPro.color = Color.white;
                fase7Button.interactable = false;
                fase8textMeshPro.color = Color.white;
                fase8Button.interactable = false;
                fase9textMeshPro.color = Color.white;
                fase9Button.interactable = false;
                fase10textMeshPro.color = Color.white;
                fase10Button.interactable = false;
                fase11textMeshPro.color = Color.white;
                fase11Button.interactable = false;
                fase12textMeshPro.color = Color.white;
                fase12Button.interactable = false;
            } else if (loadedData.activePhases <= 5)
            {
                fase6textMeshPro.color = Color.white;
                fase6Button.interactable = false;
                fase7textMeshPro.color = Color.white;
                fase7Button.interactable = false;
                fase8textMeshPro.color = Color.white;
                fase8Button.interactable = false;
                fase9textMeshPro.color = Color.white;
                fase9Button.interactable = false;
                fase10textMeshPro.color = Color.white;
                fase10Button.interactable = false;
                fase11textMeshPro.color = Color.white;
                fase11Button.interactable = false;
                fase12textMeshPro.color = Color.white;
                fase12Button.interactable = false;
            } else if (loadedData.activePhases <= 6)
            {
                fase7textMeshPro.color = Color.white;
                fase7Button.interactable = false;
                fase8textMeshPro.color = Color.white;
                fase8Button.interactable = false;
                fase9textMeshPro.color = Color.white;
                fase9Button.interactable = false;
                fase10textMeshPro.color = Color.white;
                fase10Button.interactable = false;
                fase11textMeshPro.color = Color.white;
                fase11Button.interactable = false;
                fase12textMeshPro.color = Color.white;
                fase12Button.interactable = false;
            } else if (loadedData.activePhases <= 7)
            {
                fase8textMeshPro.color = Color.white;
                fase8Button.interactable = false;
                fase9textMeshPro.color = Color.white;
                fase9Button.interactable = false;
                fase10textMeshPro.color = Color.white;
                fase10Button.interactable = false;
                fase11textMeshPro.color = Color.white;
                fase11Button.interactable = false;
                fase12textMeshPro.color = Color.white;
                fase12Button.interactable = false;
            } else if (loadedData.activePhases <= 8)
            {
                fase9textMeshPro.color = Color.white;
                fase9Button.interactable = false;
                fase10textMeshPro.color = Color.white;
                fase10Button.interactable = false;
                fase11textMeshPro.color = Color.white;
                fase11Button.interactable = false;
                fase12textMeshPro.color = Color.white;
                fase12Button.interactable = false;
            } else if (loadedData.activePhases <= 9)
            {
                fase10textMeshPro.color = Color.white;
                fase10Button.interactable = false;
                fase11textMeshPro.color = Color.white;
                fase11Button.interactable = false;
                fase12textMeshPro.color = Color.white;
                fase12Button.interactable = false;
            } else if (loadedData.activePhases <= 10)
            {
                fase11textMeshPro.color = Color.white;
                fase11Button.interactable = false;
                fase12textMeshPro.color = Color.white;
                fase12Button.interactable = false;
            } else if (loadedData.activePhases <= 11)
            {
                fase12textMeshPro.color = Color.white;
                fase12Button.interactable = false;
            }
        }
    }
}
