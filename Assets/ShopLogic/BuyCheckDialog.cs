using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyCheckDialog : MonoBehaviour
{
    private GameObject exitMenu;
    private GameObject shopMenu;
    private GameObject selectedSlot;

    // Start is called before the first frame update
    void Start()
    {
        exitMenu = GameObject.Find("ExitMenu");
        shopMenu = GameObject.Find("ShopInterface");
        SwitchToShopMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToShopMenu()
    {
        shopMenu.SetActive(true);
        exitMenu.SetActive(false);
    }

    public void SwitchToExitMenu(GameObject slot)
    {
        shopMenu.SetActive(false);
        exitMenu.SetActive(true);

        selectedSlot = slot;
    }

    public void YesBuy()
    {
        var mainLogic = FindObjectOfType<CoinLogic>();
        mainLogic.BuyCharacter(selectedSlot);
    }
}
