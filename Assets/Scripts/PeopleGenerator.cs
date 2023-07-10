using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PeopleGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> peoplePrefabList;
    public static PeopleGenerator Instance;
    public List<GameObject> peopleObjectList;
    public float waveNumber;
    public int numberOfPeople;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GeneratePeople()
    {
        //firstMove
        LevelSpawner.Instance.currentLevelScript.spawnGates.transform.DOScaleZ(0, 3f).OnComplete(() => StartCoroutine(Generate(0, 2, (int)(GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel - 1].howManyPeopleToGenerate * (waveNumber / (GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel - 1].waveNumber * (GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel - 1].waveNumber + 1) / 2))))));
    }

    public IEnumerator Generate(int numberSoFar, int timer, int maxInWave)
    {
        yield return new WaitForSeconds(timer);
        Debug.Log("numberSoFar" + numberSoFar);
        if (numberSoFar == maxInWave)//ba�ka wave geliyor
        {
            waveNumber++;
            Debug.Log("wave" + waveNumber);
            UIManager.Instance.waveText.text = waveNumber.ToString();
            if (waveNumber != GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel - 1].waveNumber + 1)
            {
                
                  int kisi = (int)(GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel - 1].howManyPeopleToGenerate * (waveNumber / (GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel - 1].waveNumber * (GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel - 1].waveNumber + 1) / 2)));
                   StartCoroutine(Generate(0, timer, kisi));
            }
         
        }
        else
        {
            GameObject temp = null;
            if (GameDataManager.Instance.currentLevel == 1)
            {
                float random = Random.Range(0, 10);
                if (waveNumber == 1)
                {
                    // h�zl� ortalama 

                    if (random > 4)
                    {
                        temp = Instantiate(peoplePrefabList[0], LevelSpawner.Instance.currentLevelScript.spawnerPos.transform);
                    }
                    else
                    {
                        temp = Instantiate(peoplePrefabList[1], LevelSpawner.Instance.currentLevelScript.spawnerPos.transform);
                    }
                }
                else if (waveNumber == 2)
                {
                    //ortalama h�zl� yaavas
                    if (random > 4)
                    {
                        temp = Instantiate(peoplePrefabList[1], LevelSpawner.Instance.currentLevelScript.spawnerPos.transform);
                    }
                    else if (random > 1)
                    {
                        temp = Instantiate(peoplePrefabList[0], LevelSpawner.Instance.currentLevelScript.spawnerPos.transform);
                    }
                    else
                    {
                        temp = Instantiate(peoplePrefabList[2], LevelSpawner.Instance.currentLevelScript.spawnerPos.transform);
                    }
                }

                else if (waveNumber == 3)
                {
                    //yavas ortalama 
                    if (random > 3)
                    {
                        temp = Instantiate(peoplePrefabList[2], LevelSpawner.Instance.currentLevelScript.spawnerPos.transform);
                    }
                    else
                    {
                        temp = Instantiate(peoplePrefabList[1], LevelSpawner.Instance.currentLevelScript.spawnerPos.transform);
                    }
                }
            }
            else
            {
                int random = Random.Range(0, 100);
                if (random > 60)//sisko
                {
                    temp = Instantiate(peoplePrefabList[1], LevelSpawner.Instance.currentLevelScript.spawnerPos.transform);
                }
                else if (random > 40)
                {
                    if (Random.Range(0, 1) == 1)
                    {

                        temp = Instantiate(peoplePrefabList[1], LevelSpawner.Instance.currentLevelScript.spawnerPos.transform);

                    }
                    else
                    {
                        temp = Instantiate(peoplePrefabList[1], LevelSpawner.Instance.currentLevelScript.spawnerPos.transform);
                    }
                }
                else if (random > 20)
                {
                    if (Random.Range(0, 1) == 1)
                    {

                        temp = Instantiate(peoplePrefabList[1], LevelSpawner.Instance.currentLevelScript.spawnerPos.transform);

                    }
                    else
                    {
                        temp = Instantiate(peoplePrefabList[1], LevelSpawner.Instance.currentLevelScript.spawnerPos.transform);
                    }
                }
                else//kaykay
                {
                    temp = Instantiate(peoplePrefabList[0], LevelSpawner.Instance.currentLevelScript.spawnerPos.transform);
                }
            }
            numberOfPeople++;
            temp.name = "People";
            temp.GetComponent<PeopleManager>().peopleIndex = numberOfPeople;
            if(GameDataManager.Instance.currentLevel == 1)
            {
                temp.GetComponent<PeopleManager>().maxhealth *= Mathf.Pow(0.7f, waveNumber - 1);
            }
            else
            {
                temp.GetComponent<PeopleManager>().maxhealth *= Mathf.Pow(0.95f, waveNumber - 1);
            }
            peopleObjectList.Add(temp);
            temp.GetComponent<PeopleManager>().MoveStart();

            StartCoroutine(Generate(numberSoFar + 1, 4, maxInWave));
        }

    }
}
