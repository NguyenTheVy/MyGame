using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static int CoinData
    {
        get => PlayerPrefs.GetInt(DataCointainer.CoinID, 0);
        set => PlayerPrefs.SetInt(DataCointainer.CoinID, value);
    }
}
