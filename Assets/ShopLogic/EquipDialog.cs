using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipDialog : MonoBehaviour
{
    private GameObject equipMenu;
    private GameObject shopMenu;
    private GameObject selectedSlot;

    // Start is called before the first frame update
    void Start()
    {
        equipMenu = GameObject.Find("EquipMenu");
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
        equipMenu.SetActive(false);
    }

    public void SwitchToEquipMenu(GameObject slot)
    {
        shopMenu.SetActive(false);
        equipMenu.SetActive(true);

        selectedSlot = slot;
    }

    public void YesEquip()
    {
        var mainLogic = FindObjectOfType<CoinLogic>();
        mainLogic.EquipCharacter(selectedSlot);
    }
}
