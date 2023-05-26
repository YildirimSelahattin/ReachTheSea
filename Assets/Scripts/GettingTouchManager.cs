using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.Burst.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class GettingTouchManager : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    public GameObject touchedMachineSpot;
    [SerializeField] LayerMask touchableMachineSpotLayer;
    [SerializeField] LayerMask floorTouchableLayer;
    [SerializeField] LayerMask machinebuyingButtonsLayer;

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
                        case "sunScreenBuyButton":
                            touchedMachineSpot.GetComponent<MachineSpotManager>().SunscreenBuyButtonPressed();
                            break;
                    }
                }
                else if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, touchableMachineSpotLayer)) // if it hit to a machine object
                {
                    if(touchedMachineSpot == hit.collider.gameObject)
                    {
                        touchedMachineSpot.GetComponent<MachineSpotManager>().ResetAndClose();
                        touchedMachineSpot = null;
                    }
                    
                    else
                    {
                        touchedMachineSpot = hit.collider.gameObject;
                        touchedMachineSpot.GetComponent<MachineSpotManager>().buttonLayout.SetActive(true);
                    }

                }
                else if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, floorTouchableLayer)) // if it hit to a machine object
                {
                    touchedMachineSpot.GetComponent<MachineSpotManager>().ResetAndClose();
                    touchedMachineSpot = null;
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