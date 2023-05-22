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
    public List<GameObject> gridObjectsList = new List<GameObject>();
    public List<GameObject> machineGridObjectList = new List<GameObject>();
    public List<int> gridList = new List<int>();
    public GameObject[] gridPrefabArray;
    public int gridWidth;
    public float xSize;
    public float ySize;
    public int gridHeight;
    public GameObject gridParent;
    public static LevelSpawner Instance;

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
        gridHeight = GameDataManager.Instance.data.levelsArray[levelArrayIndex].gridHeight;
        gridWidth = GameDataManager.Instance.data.levelsArray[levelArrayIndex].gridWidth;
        CreateGrid(levelArrayIndex);
        if (gridHeight == 10)
        {
            gridParent.transform.DOLocalMove(new Vector3(-2.8f, 0, 8.77f), 0.5f);
        }
    }
    public void CreateGrid(int levelIndex)
    {
        GeneralDataStructure tempDataList = GameDataManager.Instance.data.levelsArray[levelIndex];
        int index = 0;
        GameObject grid = new GameObject();
        // for elevator grid
        for (int y = 0; y < tempDataList.gridHeight; y++)
        {
            for (int x = 0; x < tempDataList.gridWidth; x++)
            {
                GameObject tempGrid = Instantiate(grid, gridParent.transform);
                tempGrid.name = x.ToString() + y.ToString();
                tempGrid.transform.localPosition = new Vector3(x * xSize, 0, -y * ySize);
                gridObjectsList.Add(tempGrid);
                if (GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel - 1].roadIndexes.Contains(index))//road
                {
                    Instantiate(gridPrefabArray[1],tempGrid.transform);
                }
                else if (GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel - 1].machineSpotIndexes.Contains(index))//machinespot
                {
                    Instantiate(gridPrefabArray[2], tempGrid.transform);
                    machineGridObjectList.Add(tempGrid);
                }
                else//land
                {
                    if (y > 15)
                    {
                        Instantiate(gridPrefabArray[3], tempGrid.transform);
                    }
                    else
                    {
                        Instantiate(gridPrefabArray[0], tempGrid.transform);
                    }
                }
                index++;
            }
        }

        //Instantiate people spawner
        PeopleGenerator.Instance.GeneratePeople();
    }
}
