using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[AddComponentMenu("Vy/GameManager")]


public class GameManager : MonoBehaviour
{
    private int coin;
    public UnityEvent<int> CoinEvent;

    public static GameManager Instance
    {
        get => instance;
    }
    private static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(instance);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if (CoinEvent == null)
        {
            CoinEvent = new UnityEvent<int>();
        }
        coin = Data.CoinData;
    }
    public void IncreaseCoin(int coin)
    {
        this.coin += coin;
        CoinEvent?.Invoke(this.coin);
        Data.CoinData = this.coin;
    }
}
