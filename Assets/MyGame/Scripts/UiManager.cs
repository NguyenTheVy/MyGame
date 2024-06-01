using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[AddComponentMenu("Vy/UiManager")]


public class UiManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textCoinGUI;

    void Start()
    {
        textCoinGUI.text = Data.CoinData.ToString();
        //textLabGui.text = Data.LabData.ToString();
        //textFuelGui.text = Data.FuelData.ToString();

        GameManager.Instance.CoinEvent.AddListener(UpdateCoin);
        //GameManager.Instance.LabEvent.AddListener(UpdateLab);
        //GameManager.Instance.FuelEvent.AddListener(UpdateFuel);


    }


    public void UpdateCoin(int coin)
    {
        if (textCoinGUI != null)
        {
            textCoinGUI.text = coin.ToString();

        }
    }
}
