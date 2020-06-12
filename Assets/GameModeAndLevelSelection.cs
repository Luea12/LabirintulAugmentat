using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameModeAndLevelSelection : MonoBehaviour
{
    private ProfileData profile;
    public GameObject gameModeMenu;
    public GameObject levelMenu;

    public Button[] difficultyButtons;

    // Start is called before the first frame update
    void Start()
    {
        profile = ProfileData.Load();
        SwitchToGameModeMenu();
        UpdateDifficultyButtons();
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

    public void UpdateDifficultyButtons() 
    {
        bool[] unlocked = profile.GetDifficulties();
        for (int i = 0; i < unlocked.Length; i++) 
        {
            if (!unlocked[i])
            {
                difficultyButtons[i].interactable = false;
            }
        }
    }

    public void SwitchToGameModeMenu() 
    {
        gameModeMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void SwitchToLevelMenu() 
    {
        gameModeMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void CommitLevelToGlobalObject(int level) 
    {
        GlobalControl.instance.selectedDifficultyIdx = level;
    }

    public void CommitGameModeToGlobalObject(int gamemode)
    {
        GlobalControl.instance.selectedGamemode = gamemode;
    }
}
