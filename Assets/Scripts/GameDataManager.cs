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

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadData()
    {
        data = JsonUtility.FromJson<DataList>(JSONText.text);
        // currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    }
}