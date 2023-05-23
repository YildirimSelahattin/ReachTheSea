using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MoneyText : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + GameDataManager.Money.ToString();
    }
}
