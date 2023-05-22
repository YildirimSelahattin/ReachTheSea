using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingTouchManager : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    public GameObject touchedMachineSpot;
    [SerializeField] LayerMask touchableMachineSpotLayer;
    [SerializeField] LayerMask floorTouchableLayer;

    public Vector3 touchStartPos;
    public Vector3 curTouchPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            curTouchPosition = Input.GetTouch(0).position;
            ray = Camera.main.ScreenPointToRay(curTouchPosition);

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)                // This is actions when finger/cursor hit screen
            {
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, touchableMachineSpotLayer)) // if it hit to a machine object
                {
                    touchedMachineSpot = hit.collider.gameObject;
                    Debug.Log(touchedMachineSpot.name);
                    if (touchedMachineSpot.GetComponent<MachineSpotManager>().buttonLayout.active != true)
                    {
                        touchedMachineSpot.GetComponent<MachineSpotManager>().buttonLayout.SetActive(true);
                    }
                    else
                    {
                        touchedMachineSpot.GetComponent<MachineSpotManager>().buttonLayout.SetActive(false);
                    }
                }
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, floorTouchableLayer)) // if it hit to a machine object
                {
                    touchedMachineSpot = hit.collider.gameObject;
                    Debug.Log(touchedMachineSpot.name);

                    foreach (GameObject machineSpot in LevelSpawner.Instance.machineGridObjectList)
                    {
                        machineSpot.transform.GetChild(0).GetComponent<MachineSpotManager>().buttonLayout.SetActive(false);
                    }

                }
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {

            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {

            }
        }
    }
}