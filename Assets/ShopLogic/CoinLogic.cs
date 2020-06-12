using Newtonsoft.Json;
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
    public Sprite[] availableSprites;
    public int[] availablePrices;

    public ScrollRect OwnedPanel;
    public Sprite[] ownedSprites;

    private int availableYPos = 300;
    private int ownedYPos = 300;

    // Start is called before the first frame update
    void Start()
    {
        // Read the shop data
        readShop();
        coinsValue = GameObject.Find("CoinsValue").GetComponent<TextMeshProUGUI>();
        coinsValue.text = shop.coins.ToString();

        // Instantiate the characters that are available for purchase
        CreateAvailableSlots();

        // Instantiate the characters that are owned by the user
        CreateOwnedSlots();
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

    void CreateAvailableSlots()
    {
        for (int i = 0; i < availableSprites.Length; i++)
        {
            // Insantiate slot
            var slot = Instantiate(availableCharacterPrefab, new Vector3(0, availableYPos, 0), Quaternion.identity);
            slot.transform.SetParent(AvailablePanel.content.transform, false);

            // Change the character name to be the sprite name
            slot.name = availableSprites[i].name;
            var slotName = slot.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            slotName.text = availableSprites[i].name;

            // Change Image sprite
            var spriteComponent = slot.transform.Find("Image").GetComponent<Image>();
            spriteComponent.sprite = availableSprites[i];
            spriteComponent.preserveAspect = true;
            spriteComponent.useSpriteMesh = true;
            spriteComponent.raycastTarget = true;
            spriteComponent.type = Image.Type.Simple;

            // Change character price
            slot.transform.Find("PriceValue").GetComponent<TextMeshProUGUI>().text = availablePrices[i].ToString();

            // Change the next instantiation position
            availableYPos -= 150;

            // Save in shop object (maybe that's not needed)
            shop.availableCharacters.Add(slot);
        }
    }

    void CreateOwnedSlots()
    {
        for (int i = 0; i < ownedSprites.Length; i++)
        {
            // Insantiate slot
            var slot = Instantiate(ownedCharacterPrefab, new Vector3(0, ownedYPos, 0), Quaternion.identity);
            slot.transform.SetParent(OwnedPanel.content.transform, false);

            // Change the character name to be the sprite name
            slot.name = ownedSprites[i].name;
            var slotName = slot.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            slotName.text = ownedSprites[i].name;

            // Change Image sprite
            var spriteComponent = slot.transform.Find("Image").GetComponent<Image>();
            spriteComponent.sprite = ownedSprites[i];
            spriteComponent.preserveAspect = true;
            spriteComponent.useSpriteMesh = true;
            spriteComponent.raycastTarget = true;
            spriteComponent.type = Image.Type.Simple;

            // Change the next instantiation position
            ownedYPos -= 150;

            // Save in shop object (maybe that's not needed)
            shop.ownedCharacters.Add(slot);
        }
    }

    GameObject CreateOwnedSlot(Vector3 v)
    {
        var slot = Instantiate(ownedCharacterPrefab, v, Quaternion.identity);
        slot.transform.SetParent(OwnedPanel.content.transform, false);

        return slot;
    }

    public void BuyCharacter(GameObject availableCharacter)
    {

        //var availableCharacter = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        shop.availableCharacters.Remove(availableCharacter);

        var newOwned = CreateOwnedSlot(new Vector3(0, ownedYPos, 0));
        ownedYPos -= 150;

        /**
         * TODO: Copy values from available character over to the newly owned character
         * and persist everything to storage
         */

        shop.ownedCharacters.Add(newOwned);

        Destroy(availableCharacter);

        //Getting back to the shop menu
        var dialog = FindObjectOfType<BuyCheckDialog>();
        dialog.SwitchToShopMenu();
    }
}
