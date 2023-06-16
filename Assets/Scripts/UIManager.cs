using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI moneyText;
    public GameObject moneyParticle;
    public static UIManager Instance;
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        moneyText.text = GameDataManager.Instance.totalMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
