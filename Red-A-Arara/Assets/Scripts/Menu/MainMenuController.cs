using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    //Labels
    [SerializeField]
    private TMP_Text fase1textMeshPro;
    [SerializeField]
    private TMP_Text fase2textMeshPro;
    [SerializeField]
    private TMP_Text fase3textMeshPro;
    [SerializeField]
    private TMP_Text fase4textMeshPro;
    [SerializeField]
    private TMP_Text fase5textMeshPro;
    [SerializeField]
    private TMP_Text fase6textMeshPro;

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
            fase1textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
            fase1Button.interactable = false;
            fase2textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
            fase2Button.interactable = false;
            fase3textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
            fase3Button.interactable = false;
            fase4textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
            fase4Button.interactable = false;
            fase5textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
            fase5Button.interactable = false;
            fase6textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
            fase6Button.interactable = false;
        } else
        {
            if (loadedData.activePhases <= 1)
            {
                fase2textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase2Button.interactable = false;
                fase3textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase3Button.interactable = false;
                fase4textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase4Button.interactable = false;
                fase5textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase5Button.interactable = false;
                fase6textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase6Button.interactable = false;
            } else if (loadedData.activePhases <= 2)
            {
                fase3textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase3Button.interactable = false;
                fase4textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase4Button.interactable = false;
                fase5textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase5Button.interactable = false;
                fase6textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase6Button.interactable = false;
            } else if (loadedData.activePhases <= 3)
            {
                fase4textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase4Button.interactable = false;
                fase5textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase5Button.interactable = false;
                fase6textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase6Button.interactable = false;
            } else if (loadedData.activePhases <= 4)
            {
                fase5textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase5Button.interactable = false;
                fase6textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase6Button.interactable = false;
            } else if (loadedData.activePhases <= 5)
            {
                fase6textMeshPro.colorGradientPreset = new TMP_ColorGradient(colorTop, colorTop, colorBottom, colorBottom);
                fase6Button.interactable = false;
            }
        }
    }
}
