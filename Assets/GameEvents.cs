using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    void Awake()
    {
        current = this;
    }

    public event Action OnCoinPickup;
    public void CoinPickup()
    {
        OnCoinPickup?.Invoke();
    }

    public event Action OnLevelUp;
    public void LevelUp()
    {
        OnLevelUp?.Invoke();
    }

    public event Action OnGameStart;
    public void GameStart()
    {
        OnGameStart?.Invoke();
    }

    public event Action OnGamePause;
    public void GamePause()
    {
        OnGamePause?.Invoke();
    }


    public event Action OnGameResume;
    public void GameResume()
    {
        OnGameResume?.Invoke();
    }

    public event Action OnGameWin;
    public void GameWin()
    {
        OnGameWin?.Invoke();
    }

    public event Action OnGameLost;
    public void GameLost()
    {
        OnGameLost?.Invoke();
    }

}
