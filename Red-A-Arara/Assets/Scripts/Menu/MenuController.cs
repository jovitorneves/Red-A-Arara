using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    [SerializeField]
    private GameObject menuGameObject;

    [SerializeField]
    private GameObject pauseGameObject;

    [SerializeField]
    private GameObject faseGameObject;
    [SerializeField]
    private GameObject heart1GameObject;
    [SerializeField]
    private GameObject heart2GameObject;
    [SerializeField]
    private GameObject heart3GameObject;

    private float timer = 0.0f;
    private readonly float waitTime = 2.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!GameManager.Instance.isPause)
            {
                pauseGameObject.SetActive(false);
            } else
            {
                pauseGameObject.SetActive(true);
            }
        }
        ShowHeart();

        if (timer > waitTime)
        {
            faseGameObject.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.isPause = false;
        pauseGameObject.SetActive(false);
    }

    private void ShowHeart()
    {
        if (GameManager.Instance.heartCount <= 0)
        {
            heart1GameObject.SetActive(false);
            heart2GameObject.SetActive(false);
            heart3GameObject.SetActive(false);
        } else if (GameManager.Instance.heartCount == 1)
        {
            heart1GameObject.SetActive(true);
            heart2GameObject.SetActive(false);
            heart3GameObject.SetActive(false);
        } else if (GameManager.Instance.heartCount == 2)
        {
            heart1GameObject.SetActive(true);
            heart2GameObject.SetActive(true);
            heart3GameObject.SetActive(false);
        } else if (GameManager.Instance.heartCount >= 3)
        {
            heart1GameObject.SetActive(true);
            heart2GameObject.SetActive(true);
            heart3GameObject.SetActive(true);
        }
    }
}
