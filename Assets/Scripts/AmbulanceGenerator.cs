using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ambulancePrefab;
    public GameObject ambulanceParent;
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
    
    public void CreateAmbulance(int peopleIndex)
    {
        GameObject ambulance=Instantiate(ambulancePrefab,ambulanceParent.transform);
        ambulance.GetComponent<AmbulanceManager>().MoveToPeople(peopleIndex);
    }

}
