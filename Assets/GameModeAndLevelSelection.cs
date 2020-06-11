using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeAndLevelSelection : MonoBehaviour
{

    private GameObject gameModeMenu;
    private GameObject levelMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameModeMenu = GameObject.Find("GameModeMenu");
        levelMenu = GameObject.Find("LevelMenu");
        SwitchToGameModeMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            if (levelMenu.activeSelf) 
            {
                SwitchToGameModeMenu();
            } else
            {
                SceneManager.LoadSceneAsync("start");
            }
        }
    }

    public void SwitchToGameModeMenu() {
        gameModeMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void SwitchToLevelMenu() {
        gameModeMenu.SetActive(false);
        levelMenu.SetActive(true);
    }
}
