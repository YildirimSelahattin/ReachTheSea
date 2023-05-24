using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaMachine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("People"))
        {
            other.gameObject.GetComponent<PeopleManager>().isUnderUmbrella= true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("People"))
        {
            other.gameObject.GetComponent<PeopleManager>().isUnderUmbrella = false;
        }
    }
}
