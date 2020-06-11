using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class CoinLogic : MonoBehaviour
{
    private Shop shop;
    private TextMeshProUGUI coinsValue;

    // Start is called before the first frame update
    void Start()
    {
        readShop();

        coinsValue = GameObject.Find("CoinsValue").GetComponent<TextMeshProUGUI>();
        coinsValue.text = shop.coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void readShop()
    {
        string json = File.ReadAllText(Application.dataPath + "/ShopLogic/Shop.json");
        shop = JsonConvert.DeserializeObject<Shop>(json);
    }

    class Shop
    {
        public int coins { get; set; }
    }
}
