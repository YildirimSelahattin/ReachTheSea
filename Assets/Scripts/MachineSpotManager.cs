using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineSpotManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buttonLayout;
    public GameObject machinePlaceObject;
    public GameObject sunscreenPreviewButton;
    public GameObject sunscreenBuyButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SunscreenPreviewButtonPressed()
    {
        sunscreenPreviewButton.SetActive(false);
        sunscreenBuyButton.SetActive(true);
    }

    public void SunscreenBuyButtonPressed()
    {
        MachineGenerator.Instance.CreateSunscreenMachine(transform);
    }

    public void ResetAndClose()
    {
        sunscreenBuyButton.SetActive(false);
        sunscreenPreviewButton.SetActive(true);
    }
}
