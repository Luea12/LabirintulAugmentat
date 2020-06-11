using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public int coins { get; set; }
    public List<GameObject> ownedCharacters { get; set; } = new List<GameObject>();
    public List<GameObject> availableCharacters { get; set; } = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
