using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    private int numberOfSeconds;

    private TMP_Text timerText;

    private bool gamePaused = false;

    void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        GameEvents.current.OnGameStart += OnGameStart;
        GameEvents.current.OnGamePause += OnGamePause;
        GameEvents.current.OnGameResume += OnGameResume;
    }

    void Start()
    {

    }
    
    void GetLevelTime()
    {
        numberOfSeconds = GameManager.instance.currentDifficulty.numberOfSeconds;
    }

    void OnGameStart()
    {
        StartTimer();
    }

    void OnLevelUp()
    {
        StartTimer();
    }
    
    void OnGamePause()
    {
        gamePaused = true;
    }

    void OnGameResume()
    {
        gamePaused = false;
    }

    void StartTimer()
    {
        if (GameManager.instance.selectedGamemode == 0)
        { 
            timerText.text = "";
            return;
        }
        GetLevelTime();
        StartCoroutine(TimerCoroutine());
    }

    public IEnumerator TimerCoroutine()
    {
        while (numberOfSeconds >= 0)
        {
            timerText.text = numberOfSeconds.ToString();
            yield return new WaitForSeconds(1.0f);
            if(!gamePaused)
                numberOfSeconds--;
        }

        GameEvents.current.GameLost();
    }
}
