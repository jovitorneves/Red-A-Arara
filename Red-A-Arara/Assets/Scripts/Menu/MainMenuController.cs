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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //Fases
    public void Fase1Game()
    {
        SceneManager.LoadScene(1);
    }

    public void Fase2Game()
    {
        SceneManager.LoadScene(2);
    }

    public void Fase3Game()
    {
        SceneManager.LoadScene(3);
    }

    public void Fase4Game()
    {
        SceneManager.LoadScene(4);
    }

    public void Fase5Game()
    {
        SceneManager.LoadScene(5);
    }

    public void Fase6Game()
    {
        SceneManager.LoadScene(6);
    }
}
