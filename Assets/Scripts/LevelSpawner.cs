using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static DataList;

public class LevelSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float xSize;
    public float ySize;
    public GameObject levelParent;
    public static LevelSpawner Instance;
    public List<GameObject> levelPrefabs;
    public LevelManager currentLevelScript;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            StartGame(GameDataManager.Instance.currentLevel - 1);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }


    public void StartGame(int levelArrayIndex)
    {
        CreateGrid(levelArrayIndex);

    }
    public void CreateGrid(int levelIndex)
    {
       
        GameObject levelObject = Instantiate(levelPrefabs[levelIndex],levelParent.transform);
        currentLevelScript = levelObject.GetComponent<LevelManager>();
        PeopleGenerator.Instance.GeneratePeople();
    }
}
