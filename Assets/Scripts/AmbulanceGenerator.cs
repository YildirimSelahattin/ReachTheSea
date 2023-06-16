using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ambulancePrefab;
    public static AmbulanceGenerator Instance;
    void Start()
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
    
    public void CreateAmbulance(GameObject people)
    {
        GameObject ambulance=Instantiate(ambulancePrefab,LevelSpawner.Instance.currentLevelScript.ambulanceSpawnerPos.transform);
        ambulance.GetComponent<AmbulanceManager>().MoveToPeople(people);
    }

}
