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
    }

    public void Fase2Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(2);
    }

    public void Fase3Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(3);
    }

    public void Fase4Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(4);
    }

    public void Fase5Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(5);
    }

    public void Fase6Game()
    {
        DataBase.deleteData("sceneDB");
        SceneManager.LoadScene(6);
    }
}
