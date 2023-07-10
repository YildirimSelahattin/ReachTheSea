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
        if (GameDataManager.Instance.totalMoney >= GameDataManager.Instance.sunScreenPrice)
        {
            GameDataManager.Instance.totalMoney -= (int)GameDataManager.Instance.sunScreenPrice;
            UIManager.Instance.moneyText.text = GameDataManager.Instance.totalMoney.ToString();
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
        if (GameDataManager.Instance.totalMoney >= GameDataManager.Instance.hatMachinePrice)
        {
            GameDataManager.Instance.totalMoney -= (int)GameDataManager.Instance.hatMachinePrice;
            UIManager.Instance.moneyText.text = GameDataManager.Instance.totalMoney.ToString();
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
        if (GameDataManager.Instance.totalMoney >= GameDataManager.Instance.catapultPrice)
        {
            GameDataManager.Instance.totalMoney -= (int)GameDataManager.Instance.catapultPrice;
            UIManager.Instance.moneyText.text = GameDataManager.Instance.totalMoney.ToString();
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
            hatPreviewButton.GetComponent<SpriteRenderer>().color = Color.white;
            catapultPreviewButton.GetComponent<SpriteRenderer>().color = Color.white;
            sunscreenPreviewButton.GetComponent<SpriteRenderer>().color = Color.white;
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
            switch (machinePrefabName)
            {
                case "sunScreen":
                    upgradeMachinePriceText.text =  currentMachineOnIt.GetComponent<Turret>().upgradePrice.ToString();
                    if(GameDataManager.Instance.totalMoney< currentMachineOnIt.GetComponent<Turret>().upgradePrice)
                    {
                        upgradeMachinePriceText.transform.parent.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                    deleteMachinePriceText.text = currentMachineOnIt.GetComponent<Turret>().deletePrice.ToString();
                    break;
                case "hat":
                    upgradeMachinePriceText.text =currentMachineOnIt.GetComponent<HatTurretManager>().upgradePrice.ToString();
                    if (GameDataManager.Instance.totalMoney < currentMachineOnIt.GetComponent<HatTurretManager>().upgradePrice)
                    {
                        upgradeMachinePriceText.transform.parent.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                    deleteMachinePriceText.text = currentMachineOnIt.GetComponent<HatTurretManager>().deletePrice.ToString();
                    break;
                case "catapult":
                    upgradeMachinePriceText.text =currentMachineOnIt.GetComponent<CatapultManager>().upgradePrice.ToString();
                    if (GameDataManager.Instance.totalMoney < currentMachineOnIt.GetComponent<CatapultManager>().upgradePrice)
                    {
                        upgradeMachinePriceText.transform.parent.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                    deleteMachinePriceText.text = currentMachineOnIt.GetComponent<CatapultManager>().deletePrice.ToString();
                    break;
            }
            upgradeButtonLayout.SetActive(true);
        }
    }
    public void OpenBuyPanel()
    {
        buttonLayout.transform.localScale = Vector3.zero;
        buttonLayout.transform.DOScale(Vector3.one,0.5f);
        buttonLayout.SetActive(true);
        if (GameDataManager.Instance.totalMoney < GameDataManager.Instance.sunScreenPrice)
        {
            sunscreenPreviewButton.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else
        {

        }
        if (GameDataManager.Instance.totalMoney < GameDataManager.Instance.catapultPrice)
        {
            catapultPreviewButton.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else
        {

        }
        if (GameDataManager.Instance.totalMoney < GameDataManager.Instance.hatMachinePrice)
        {
            hatPreviewButton.GetComponent<SpriteRenderer>().color = Color.gray;
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
        haveMachineOnIt = false;
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
