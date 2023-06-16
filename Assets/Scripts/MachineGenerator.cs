using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGenerator : MonoBehaviour
{
    public static MachineGenerator Instance;
    public List<GameObject> machines;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject CreateSunscreenMachine(Transform parent)
    {
         return Instantiate(machines[0], parent);
    }
    public GameObject CreateCatapultMachine(Transform parent)
    {
        return Instantiate(machines[1], parent);
    }
    public GameObject CreateSunHatMachine(Transform parent)
    {
        return Instantiate(machines[2], parent);
    }
}
