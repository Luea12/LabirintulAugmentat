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
<<<<<<< HEAD
        if(instance != null)
        {
            Destroy(this);
        }
            instance = this;    
=======
        if (instance == null) {
            DontDestroyOnLoad(this);
        }
        instance = this;    
>>>>>>> 4031eff40dd776ed58ef0233df57dba242cf3b9e
    }

    void Start()
    {
        selectedDifficultyIdx = 0;
    }
}
