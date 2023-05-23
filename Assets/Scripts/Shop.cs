using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
 
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void PurchaseStandardTurret()
    {
        Debug.Log("PurchaseTurret");
        buildManager.SetTurretToBuild(standardTurret);
    }
    public void PurchaseMissileTurret()
    {
        Debug.Log("PurchaseAnotherTurret");
        buildManager.SetTurretToBuild(missileLauncher);
    }
}
