using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinLogic : MonoBehaviour
{
    private Shop shop;
    private TextMeshProUGUI coinsValue;
    public GameObject availableCharacterPrefab;
    public GameObject ownedCharacterPrefab;
    public ScrollRect AvailablePanel;
    public ScrollRect OwnedPanel;

    // Start is called before the first frame update
    void Start()
    {
        // Read the shop data
        readShop();
        coinsValue = GameObject.Find("CoinsValue").GetComponent<TextMeshProUGUI>();
        coinsValue.text = shop.coins.ToString();

        // Instantiate the characters that are available for purchase
        // Call this as many times as necessary
        CreateAvailableSlot(new Vector3(0, 50, 1));
        CreateAvailableSlot(new Vector3(0, 250, 1));

        // Instantiate the characters that are owned by the user
        // Call this as many times as necessary
        CreateOwnedSlot(new Vector3(0, 50, 1));
        CreateOwnedSlot(new Vector3(0, 250, 1));
    }

    // Update is called once per frame
    void Update()
    {
        coinsValue.text = shop.coins.ToString();
    }

    public void consumeCoins(int coins)
    {
        shop.coins -= coins;
    }

    void readShop()
    {
        TextAsset file = Resources.Load("Shop") as TextAsset;
        string json = file.ToString();
        shop = JsonConvert.DeserializeObject<Shop>(json);
    }

    void CreateAvailableSlot(Vector3 position)
    {
        GameObject availableSlot = Instantiate(availableCharacterPrefab, position, Quaternion.identity);
        availableSlot.transform.SetParent(AvailablePanel.content.transform, false);
        shop.availableCharacters.Add(availableSlot);
    }

    void CreateOwnedSlot(Vector3 position)
    {
        GameObject ownedSlot = Instantiate(ownedCharacterPrefab, position, Quaternion.identity);
        ownedSlot.transform.SetParent(OwnedPanel.content.transform, false);
        shop.ownedCharacters.Add(ownedSlot);
    }

    class Shop
    {
        public int coins { get; set; }
        public List<GameObject> ownedCharacters { get; set; } = new List<GameObject>();
        public List<GameObject> availableCharacters { get; set; } = new List<GameObject>();
    }
}
