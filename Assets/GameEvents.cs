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
