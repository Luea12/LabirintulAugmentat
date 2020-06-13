using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public static bool CanPause = true;

    public GameObject pauseMenu;
    public GameObject gameUI;
    public GameObject winScreen;
    public GameObject lostScreen;

    void Awake()
    { 
        GameEvents.current.OnGameWin += ShowWinScreen;
        GameEvents.current.OnGameLost += ShowLostScreen;
    }

    void Start()
    {
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && CanPause)
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
        AudioManager.instance.UpdatePitch(2f);
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameEvents.current.GameResume();
    }

    void Pause() {
        AudioManager.instance.UpdatePitch(.5f);
        gameUI.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        AnimateScreen(pauseMenu);
        GameEvents.current.GamePause();
    }

    void ShowWinScreen()
    {
        AudioManager.instance.Play("GameWin");
        gameUI.SetActive(false);
        pauseMenu.SetActive(false);
        winScreen.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        CanPause = false;
        AnimateScreen(winScreen);
    }

    void ShowLostScreen()
    {
        AudioManager.instance.Play("GameOver");
        gameUI.SetActive(false);
        pauseMenu.SetActive(false);
        lostScreen.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        CanPause = false;
        AnimateScreen(lostScreen);
    }

    private void AnimateScreen(GameObject Screen) {
        Image image = Screen.GetComponent<Image>();
        image.canvasRenderer.SetAlpha(0.0f);
        image.CrossFadeAlpha(0.6f, 0.35f, true);
    }

    public void LoadMenu() {
        CanPause = true;
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("start");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
