using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGenerator : MonoBehaviour
{
    public static MachineGenerator Instance;
    public List<GameObject> machines;
    void Awake()
    {
        if(Instance == null)
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

    public void mach1Create(RaycastHit hit)
    {
        Instantiate(machines[0], hit.collider.gameObject.transform.parent.parent);
    }
    public void mach2Create(RaycastHit hit)
    {        Instantiate(machines[1], hit.collider.gameObject.transform.parent.parent);

        
    }
    public void mach3Create(RaycastHit hit)
    {
        Instantiate(machines[2], hit.collider.gameObject.transform.parent.parent);

    }
}
