using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoOptionExit : MonoBehaviour
{
	private GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("ExitMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CloseExitMenu() {
    	menu.SetActive(false);
    }
}
