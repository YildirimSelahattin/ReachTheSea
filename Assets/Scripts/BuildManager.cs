
using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    public Transform turretParent;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one instance");
            return;
        }
        Instance = this;
    }

    

   

    private TurretBlueprint turretToBuild;

    public bool canBuild
    {
        get { return turretToBuild != null; }
    }
    public bool hasMoney
    {
        get { return GameDataManager.Money >= turretToBuild.cost; }
    }

    public void BuildTurretOn(Node node)
    {
        if (GameDataManager.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough gold");
            return;
        } 
        GameDataManager.Money -= turretToBuild.cost; 
        GameObject turret = (GameObject)Instantiate(turretToBuild.preFab, node.GetBuildPos(), Quaternion.identity, BuildManager.Instance.turretParent); 
        node.turret = turret;
        Debug.Log("Built turret! Money left: " + GameDataManager.Money);
    }
    public void SetTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;

    }
    
    
    
}
