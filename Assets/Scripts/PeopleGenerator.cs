using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> peoplePrefabList;
    public static PeopleGenerator Instance;
    public List<GameObject> peopleObjectList;
    void Awake()
    {
        if(Instance == null)
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
        StartCoroutine(Generate(0));
    }

    public IEnumerator Generate(int numberSoFar)
    {
        yield return new WaitForSeconds(6);
        if(numberSoFar != GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel - 1].howManyPeopleToGenerate)
        {
            GameObject temp=Instantiate(peoplePrefabList[0], LevelSpawner.Instance.gridObjectsList[GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel - 1].roadIndexes[0]].transform);
            temp.name = "People";
            temp.GetComponent<PeopleManager>().peopleIndex = numberSoFar;
            peopleObjectList.Add(temp);
            StartCoroutine(Generate(numberSoFar+1));
        }
    }
}
