using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingTouchManager : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    public GameObject touchedMachineSpot;
    public GameObject touchedMachine;
    [SerializeField] LayerMask touchableMachineSpotLayer;
    [SerializeField] LayerMask floorTouchableLayer;
    [SerializeField] LayerMask machinebuyingButtonsLayer;
    [SerializeField] LayerMask machineLayer;

    public Vector3 touchStartPos;
    public GameObject turret;
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
            //For ray purposes
            curTouchPosition = Input.GetTouch(0).position;
            ray = Camera.main.ScreenPointToRay(curTouchPosition);

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)// This is actions when finger/cursor hit screen
            {
                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, machinebuyingButtonsLayer)) // if it hit to a machine button
                {
                    GameObject pressedButtonObject = hit.collider.gameObject;
                    switch (pressedButtonObject.transform.tag)
                    {
                        case "sunScreenPreviewButton":
                            touchedMachineSpot.GetComponent<MachineSpotManager>().SunscreenPreviewButtonPressed();
                            break;
                        case "catapultPreviewButton":
                            touchedMachineSpot.GetComponent<MachineSpotManager>().CatapultPreviewButtonPressed();
                            break;
                        case "sunHatPreviewButton":
                            touchedMachineSpot.GetComponent<MachineSpotManager>().HatPreviewButtonPressed();
                            break;
                        case "upgradeButton":
                            touchedMachineSpot.GetComponent<MachineSpotManager>().UpgradeMachine();
                            break;
                        case "deleteButton":
                            touchedMachineSpot.GetComponent<MachineSpotManager>().SellMachine();
                            break;
                    }
                    touchedMachine = null;
                }
           
                else if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, touchableMachineSpotLayer)) // if it hit to a machine object
                {
                    if (touchedMachineSpot == null)
                    {
                        touchedMachineSpot = hit.collider.gameObject;
                        if (touchedMachineSpot.GetComponent<MachineSpotManager>().haveMachineOnIt == true)
                        {
                            touchedMachineSpot.GetComponent<MachineSpotManager>().UpgradePanel();
                        }
                        else
                        {

                            touchedMachineSpot.GetComponent<MachineSpotManager>().OpenBuyPanel();
                        }
                    }
                    else if (touchedMachineSpot == hit.collider.gameObject) // presses 
                    {
                        Debug.Log("heay");
                        touchedMachineSpot.GetComponent<MachineSpotManager>().ResetAndClose();
                        touchedMachineSpot = null;
                    }
                    else
                    {
                        touchedMachineSpot.GetComponent<MachineSpotManager>().ResetAndClose();
                        touchedMachineSpot = hit.collider.gameObject;
                        if (touchedMachineSpot.GetComponent<MachineSpotManager>().haveMachineOnIt == true)
                        {
                            touchedMachineSpot.GetComponent<MachineSpotManager>().UpgradePanel();
                        }
                        else
                        {
                            touchedMachineSpot.GetComponent<MachineSpotManager>().OpenBuyPanel();
                        }
                    }
                }
           
                else if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, floorTouchableLayer)) // if it hit to a machine object
                {
                    Debug.Log("Emir");
                    if (touchedMachineSpot != null)
                    {
                        touchedMachineSpot.GetComponent<MachineSpotManager>().ResetAndClose();
                        
                    }
                    if(touchedMachine != null)
                    {
                        touchedMachine.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    touchedMachineSpot = null;
                    touchedMachine = null;
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
    public void CloseOtherLayouts()
    {
        foreach (GameObject machineSpot in LevelSpawner.Instance.currentLevelScript.machineObjectList)
        {
            machineSpot.GetComponent<MachineSpotManager>().buttonLayout.SetActive(false);
        }
    }

}