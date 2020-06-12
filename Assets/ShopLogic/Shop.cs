using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop
{
    public int coins { get; set; }
    public List<GameObject> ownedCharacters { get; set; } = new List<GameObject>();
    public List<GameObject> availableCharacters { get; set; } = new List<GameObject>();
}
