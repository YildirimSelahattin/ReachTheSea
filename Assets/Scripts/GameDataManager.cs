using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public DataList data;
    public TextAsset JSONText;
    public static GameDataManager Instance;
    public int currentLevel;
    public int playSound = 1;
    public int totalMoney;
    public float catapultPrice;
    public float hatMachinePrice;
    public float sunScreenPrice;
    public AudioClip splashEffect;
    public AudioClip boomEffect;
    public AudioClip deathEffect;
    public AudioClip creamEffect;

    // Start is called before the first frame update

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            LoadData();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {

    }


    public void LoadData()
    {
        data = JsonUtility.FromJson<DataList>(JSONText.text);
        totalMoney=data.levelsArray[currentLevel-1].startMoney; 
        //totalMoney = PlayerPrefs.GetInt("totalMoney",0 );
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("CurrentLevel",currentLevel);
    }
    public void ControlMoneyButtons()
    {
    }
}