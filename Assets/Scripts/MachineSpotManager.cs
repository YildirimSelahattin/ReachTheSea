using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class MachineSpotManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buttonLayout;
    public GameObject upgradeButtonLayout;
    public GameObject machinePlaceObject;
    public GameObject sunscreenPreviewButton;
    public GameObject sunscreenBuyButton;
    public GameObject catapultPreviewButton;
    public GameObject catapultBuyButton;
    public GameObject hatPreviewButton;
    public GameObject hatBuyButton;
    public GameObject currentMachineOnIt;
    public string machinePrefabName;
    public bool haveMachineOnIt = false;
    public TextMeshPro sunscreenMoneyText;
    public TextMeshPro hatMachineMoneyText;
    public TextMeshPro catapultMoneyText;
    public int machineLevelNumber;
    public TextMeshPro upgradeMachinePriceText;
    public TextMeshPro deleteMachinePriceText;
    void Start()
    {
        sunscreenMoneyText.text = GameDataManager.Instance.sunScreenPrice.ToString();
        hatMachineMoneyText.text = GameDataManager.Instance.hatMachinePrice.ToString();
        catapultMoneyText.text = GameDataManager.Instance.catapultPrice.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SunscreenPreviewButtonPressed()
    {
        if (GameDataManager.Instance.totalMoney > GameDataManager.Instance.sunScreenPrice)
        {
            GameDataManager.Instance.totalMoney -= (int)GameDataManager.Instance.sunScreenPrice;
            MachineGenerator.Instance.CreateSunscreenMachine(transform);
            machinePrefabName = "sunscreen";
            haveMachineOnIt = true;
            ResetAndClose();
            //machinePlaceObject.SetActive(false);
        }
    }

    public void SunscreenBuyButtonPressed()
    {
  
    }

    public void HatPreviewButtonPressed()
    {
        if (GameDataManager.Instance.totalMoney > GameDataManager.Instance.hatMachinePrice)
        {
            GameDataManager.Instance.totalMoney -= (int)GameDataManager.Instance.hatMachinePrice;
            currentMachineOnIt = MachineGenerator.Instance.CreateSunHatMachine(transform);
            haveMachineOnIt = true;
            machinePrefabName = "hat";
            ResetAndClose();
            //machinePlaceObject.SetActive(false);
        }
    }

    public void HatBuyButtonPressed()
    {
        

    }
    public void CatapultPreviewButtonPressed()
    {
        if (GameDataManager.Instance.totalMoney > GameDataManager.Instance.catapultPrice)
        {
            GameDataManager.Instance.totalMoney -= (int)GameDataManager.Instance.catapultPrice;
            currentMachineOnIt = MachineGenerator.Instance.CreateCatapultMachine(transform);
            haveMachineOnIt = true;
            machinePrefabName = "catapult";
            ResetAndClose();
            //machinePlaceObject.SetActive(false);
        }
    }

    public void CatapultBuyButtonPressed()
    {


    }
    public void ResetAndClose()
    {
        buttonLayout.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
        {
            sunscreenPreviewButton.SetActive(true);
            catapultPreviewButton.SetActive(true);
            hatPreviewButton.SetActive(true);
            buttonLayout.SetActive(false);
            upgradeButtonLayout.SetActive(false);
        });
   
    }

    public void UpgradePanel()
    {
        if (upgradeButtonLayout.active == true)
        {
            upgradeButtonLayout.SetActive(false);
        }
        else
        {

            upgradeButtonLayout.SetActive(true);
        }
    }
    public void OpenBuyPanel()
    {
        buttonLayout.transform.localScale = Vector3.zero;
        buttonLayout.transform.DOScale(Vector3.one,0.5f);
        buttonLayout.SetActive(true);
        if (GameDataManager.Instance.totalMoney > GameDataManager.Instance.sunScreenPrice)
        {

        }
        else
        {

        }
        if (GameDataManager.Instance.totalMoney > GameDataManager.Instance.catapultPrice)
        {

        }
        else
        {

        }
        if (GameDataManager.Instance.totalMoney > GameDataManager.Instance.sunScreenPrice)
        {

        }
        else
        {

        }
    }
    public void SellMachine()
    {
        switch (machinePrefabName)
        {
            case "sunScreen":
                currentMachineOnIt.GetComponent<Turret>().Sell();
                break;
            case "hat":
                currentMachineOnIt.GetComponent<HatTurretManager>().Sell();
                break;
            case "catapult":
                currentMachineOnIt.GetComponent<CatapultManager>().Sell();
                break;
        }
        machinePrefabName = string.Empty;
        currentMachineOnIt = null;
        ResetAndClose();
    }

    public void UpgradeMachine()
    {
        switch (machinePrefabName)
        {
            case "sunScreen":
                currentMachineOnIt.GetComponent<Turret>().Upgrade();
                break;
            case "hat":
                currentMachineOnIt.GetComponent<HatTurretManager>().Upgrade();
                break;
            case "catapult":
                currentMachineOnIt.GetComponent<CatapultManager>().Upgrade();
                break;
        }
        ResetAndClose();
    }


}
