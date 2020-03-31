using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitGameDialog : MonoBehaviour
{
    private GameObject menu;

    void Start () {
        menu = GameObject.Find("ExitMenu");
        menu.SetActive(false);
    }

    void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            menu.SetActive(true);
        }
    }
}
