using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProfileData
{
    private bool[] difficulty;
    private bool[] character;
    private int coins;
    private float volume;
    private int currentCharacter;

    private ProfileData() {
        difficulty = new bool[] { true, false, false, false, false };
        character = new bool[] { true, false, false, false, false };
        currentCharacter = 0;
        coins = 500;
        volume = 0.5f;
    }

    public static ProfileData Default() {
        return new ProfileData();
    }

    public static ProfileData Load() {
        return SaveSystem.Load();
    }

    // Setters
    public void UnlockDifficulty(int idx) {
        if (idx < difficulty.Length) 
        {
            difficulty[idx] = true;
            SaveSystem.Save(this);
        }
    }

    public void UnlockCharacter(int idx) {
        if (idx < character.Length) 
        {
            character[idx] = true;
            SaveSystem.Save(this);
        }
    }

    public void UpdateCurrentCharacter(int value) {
        if (value >= 0 && value < 5)
        {
            currentCharacter = value;
            Debug.Log(value);
            SaveSystem.Save(this);
        }
    }

    public void UpdateCoins(int value) {
        if (value >= 0)
        {
            coins = value;
            SaveSystem.Save(this);
        }
    }

    public void UpdateVolume(float value) {
        if (value >= 0.0f && value <= 1.0f) 
        {
            volume = value;
            SaveSystem.Save(this);
        }
    }

    // Getters
    public bool[] GetDifficulties() {
        return difficulty;
    }

    public bool[] GetCharacters() {
        return character;
    }

    public int GetCurrentCharacter() {
        return currentCharacter;
    }

    public int GetCoins() {
        return coins;
    }

    public float GetVolume() {
        return volume;
    }

}
