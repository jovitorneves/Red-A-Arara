using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.LoadPreviousScene();
        }
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
}
