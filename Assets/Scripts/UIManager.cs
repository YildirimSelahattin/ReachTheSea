using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI totalPeopleText;
    public TextMeshProUGUI currentPeopleText;
    public GameObject moneyParticle;
    public static UIManager Instance;
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        moneyText.text = GameDataManager.Instance.totalMoney.ToString();
        totalPeopleText.text = GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel].howManyPeopleToGenerate.ToString();
        currentPeopleText.text = 0.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
