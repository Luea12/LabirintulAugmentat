using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public Difficulty[] difficulties;
    public int selectedDifficultyIdx;
    public int selectedGamemode;

    private static GlobalControl _instance;
    public static GlobalControl instance
    {
        get { return _instance; }
        // Set only once (in the Awake method)
        private set
        {
            if (_instance != null)
            {
                return;
            }

            _instance = value;
        }
    }

    public void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
            instance = this;    
    }

    void Start()
    {
        selectedDifficultyIdx = 0;
        DontDestroyOnLoad(this);
    }
}
