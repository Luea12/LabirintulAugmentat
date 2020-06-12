using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CoinLogic : MonoBehaviour
{
    private ProfileData profile;
    private Shop shop;
    private TextMeshProUGUI coinsValue;
    private TextMeshProUGUI equipName;
    private Image equippedAvatar;

    public GameObject availableCharacterPrefab;
    public GameObject ownedCharacterPrefab;
    
    public ScrollRect AvailablePanel;
    public Sprite[] availableSprites;
    public int[] availablePrices;
    public int[] availableIndex;

    public ScrollRect OwnedPanel;
    public Sprite[] ownedSprites;

    private int availableYPos = 300;
    private int ownedYPos = 300;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize some data
        equipName = GameObject.Find("Equipped").GetComponent<TextMeshProUGUI>();
        equippedAvatar = GameObject.Find("CurrentAvatar").GetComponent<Image>();
        profile = ProfileData.Load();

        // Read the shop data
        ReadShopData();
        coinsValue = GameObject.Find("CoinsValue").GetComponent<TextMeshProUGUI>();
        coinsValue.text = shop.coins.ToString();

        // Instantiate the characters that are available for purchase
        CreateAvailableSlots();

        // Instantiate the characters that are owned by the user
        CreateOwnedSlots();

        // Read owned characters from ProfileData
        UpdateShopCharacters();
        UpdateEquippedCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        coinsValue.text = shop.coins.ToString();
    }

    public bool consumeCoins(int value)
    {
        int coins = shop.coins;
        coins -= value;

        if (coins < 0)
        {
            return false;
        } 
        else
        {
            shop.coins = coins;
            return true;
        }
    }

    void ReadShopData()
    {
        shop = new Shop();
        shop.coins = profile.GetCoins();
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

            // Store index value
            slot.GetComponent<IndexValue>().index = availableIndex[i];

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
        // Update ProfileData
        profile.UpdateCoins(shop.coins);
        profile.UnlockCharacter(availableCharacter.GetComponent<IndexValue>().index);

        shop.availableCharacters.Remove(availableCharacter);

        var newOwned = CreateOwnedSlot(new Vector3(0, ownedYPos, 0));
        ownedYPos -= 150;

        newOwned.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = availableCharacter.transform.Find("Name").GetComponent<TextMeshProUGUI>().text;
        newOwned.transform.Find("Image").GetComponent<Image>().sprite = availableCharacter.transform.Find("Image").GetComponent<Image>().sprite;

        var spriteComponent = newOwned.transform.Find("Image").GetComponent<Image>();

        spriteComponent.preserveAspect = true;
        spriteComponent.useSpriteMesh = true;
        spriteComponent.raycastTarget = true;
        spriteComponent.type = Image.Type.Simple;

        newOwned.GetComponent<IndexValue>().index = availableCharacter.GetComponent<IndexValue>().index;

        shop.ownedCharacters.Add(newOwned);

        Destroy(availableCharacter);

        FixPosition();

        // Getting back to the shop menu
        var dialog = FindObjectOfType<BuyCheckDialog>();
        dialog.SwitchToShopMenu();
    }

    public void EquipCharacter(GameObject ownedCharacter)
    {
        equipName.text = ownedCharacter.transform.Find("Name").GetComponent<TextMeshProUGUI>().text;

        // Copy info from the owned section to the Equipped Character area
        equippedAvatar.sprite = ownedCharacter.transform.Find("Image").GetComponent<Image>().sprite;
        equippedAvatar.preserveAspect = true;
        equippedAvatar.useSpriteMesh = true;
        equippedAvatar.raycastTarget = true;
        equippedAvatar.type = Image.Type.Simple;

        // Update ProfileData
        profile.UpdateCurrentCharacter(ownedCharacter.GetComponent<IndexValue>().index);

        // Getting back to the shop menu
        var dialog = FindObjectOfType<EquipDialog>();
        dialog.SwitchToShopMenu();
    }

    private void FixAvailableCharactersPosition() 
    {
        int defaultYPos = -150;
        foreach(GameObject character in shop.availableCharacters)
        {
            character.transform.SetParent(AvailablePanel.content.transform, false);
            character.transform.localPosition = new Vector3(0, defaultYPos, 0);
            defaultYPos -= 150;
        }
    }

    private void FixPosition()
    {
        int defaultYPos = -150;
        foreach(GameObject character in shop.availableCharacters)
        {
            character.transform.SetParent(AvailablePanel.content.transform, false);
            character.transform.localPosition = new Vector3(250, defaultYPos, 0);
            defaultYPos -= 150;
        }
    }

    private GameObject GetCharacterByIndex(List<GameObject> characters, int idx)
    {
        foreach (GameObject character in characters)
        {
            if (character.GetComponent<IndexValue>().index == idx)
            {
                return character;
            }
        }
        return null;
    }

    private void UpdateShopCharacters()
    {
        bool[] owned = profile.GetCharacters();
        for (int i = 0; i < owned.Length; i++)
        {
            if (owned[i])
            {
                GameObject availableCharacter = GetCharacterByIndex(shop.availableCharacters, i);

                shop.availableCharacters.Remove(availableCharacter);

                var newOwned = CreateOwnedSlot(new Vector3(0, ownedYPos, 0));
                ownedYPos -= 150;

                newOwned.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = availableCharacter.transform.Find("Name").GetComponent<TextMeshProUGUI>().text;
                newOwned.transform.Find("Image").GetComponent<Image>().sprite = availableCharacter.transform.Find("Image").GetComponent<Image>().sprite;

                var spriteComponent = newOwned.transform.Find("Image").GetComponent<Image>();

                spriteComponent.preserveAspect = true;
                spriteComponent.useSpriteMesh = true;
                spriteComponent.raycastTarget = true;
                spriteComponent.type = Image.Type.Simple;

                newOwned.GetComponent<IndexValue>().index = availableCharacter.GetComponent<IndexValue>().index;

                shop.ownedCharacters.Add(newOwned);

                Destroy(availableCharacter);
            }
        }

        FixAvailableCharactersPosition();
    }

    private void UpdateEquippedCharacter()
    {
        int equipped = profile.GetCurrentCharacter();
        GameObject equippedCharacter = GetCharacterByIndex(shop.ownedCharacters, equipped);

        if (equippedCharacter != null)
        {
            equipName.text = equippedCharacter.transform.Find("Name").GetComponent<TextMeshProUGUI>().text;

            // Copy info from the equipped character to the Equipped Character area
            equippedAvatar.sprite = equippedCharacter.transform.Find("Image").GetComponent<Image>().sprite;
            equippedAvatar.preserveAspect = true;
            equippedAvatar.useSpriteMesh = true;
            equippedAvatar.raycastTarget = true;
            equippedAvatar.type = Image.Type.Simple;
        }
    }
}
