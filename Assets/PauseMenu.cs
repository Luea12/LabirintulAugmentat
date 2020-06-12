using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenu;
    public GameObject gameUI;

    void Start()
    {
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume() {
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        gameUI.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        AnimatePauseMenu();
    }
    private void AnimatePauseMenu() {
        Image image = pauseMenu.GetComponent<Image>();
        image.canvasRenderer.SetAlpha(0.0f);
        image.CrossFadeAlpha(0.6f, 0.35f, true);
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("start");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
