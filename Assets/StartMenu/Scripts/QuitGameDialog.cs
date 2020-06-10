using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGameDialog : MonoBehaviour
{
    private GameObject exitMenu;

    void Start () {
        exitMenu = GameObject.Find("ExitMenu");
        HideExitMenu();
    }

    void Update () {
        if (Input.GetKey(KeyCode.Escape) && !exitMenu.activeSelf)
        {
            ShowExitMenu();
        }
    }
    
    public void ShowExitMenu() {
    	exitMenu.SetActive(true);
    }

    public void HideExitMenu() {
        exitMenu.SetActive(false);
    }

    public void ExitGame() {
    	Application.Quit();
    }
}
