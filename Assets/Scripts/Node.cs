
using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 offSet;
    public GameObject turret;
   

    private Color startColor;
    private Renderer rend;

    BuildManager buildManager;
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.Instance;
    }

    public Vector3 GetBuildPos()
    {
        return transform.position + offSet; 
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManager.canBuild)
        {
            return;
        }
        
        
        
        if (turret != null)
        {
            Debug.Log("Cant build");
            return;
        }

        buildManager.BuildTurretOn(this); 
        
    }

    private void OnMouseEnter() 
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManager.canBuild)
        {
            return;
        }

        if (buildManager.hasMoney)
        {
        rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = Color.black;
            
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;

    }
}
