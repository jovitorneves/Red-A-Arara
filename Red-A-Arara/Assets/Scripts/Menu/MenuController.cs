using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    [SerializeField]
    private GameObject menuGameObject;

    [SerializeField]
    private GameObject pauseGameObject;

    // Update is called once per frame
    void Update()
    {
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
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.isPause = false;
        pauseGameObject.SetActive(false);
    }
}
