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
    public TextMeshProUGUI burnedPeopleText;
    public TextMeshProUGUI swimmingPeopleText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI totalWaveText;
    public GameObject moneyParticle;
    public static UIManager Instance;
    public GameObject SafeArea;
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        totalWaveText.text = GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel-1].waveNumber.ToString();
        moneyText.text = GameDataManager.Instance.totalMoney.ToString();
        totalPeopleText.text = GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel-1].howManyPeopleToGenerate.ToString();
        swimmingPeopleText.text = 0.ToString();
        burnedPeopleText.text = 0.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
