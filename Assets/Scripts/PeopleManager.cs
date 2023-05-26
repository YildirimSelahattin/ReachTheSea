using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleManager : MonoBehaviour
{
    // Start is called before the first frame update
    float speed = 0.01f;
    public float maxhealth;
    public float curHealth;
    public float healthDecreaseAmount;
    public List<int> positionIndexList;
    public float offsetRange = 0.004f;
    Vector3 leftDirection = new Vector3(0, -90, 0);
    Vector3 rightDirection = new Vector3(0, 90, 0);
    Vector3 upDirection = new Vector3(0, 180, 0);
    Vector3 downDirection = new Vector3(0, -180, 0);
    Color originalSkinColor;
    public GameObject modelParent;
    public GameObject character;
    public GameObject dangerousIcon;
    public int peopleIndex;
    public bool isUnderUmbrella;
    void Start()
    {
        curHealth = maxhealth;
        originalSkinColor = character.GetComponent<MeshRenderer>().materials[0].color;
        

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MoveStart()
    {
        ChangeRotation(0);
        transform.parent = LevelSpawner.Instance.currentLevelScript.roadObjectList[0].transform;
        transform.DOLocalMove(Vector3.zero, 1f).OnComplete(() =>
        {
            MoveToNextIndex(1);
        });
        MoveShake();
    }
    public void MoveToNextIndex(int index)
    {
        Debug.Log(index+"as");
        if (index == positionIndexList.Count-1)
        {
            transform.parent = LevelSpawner.Instance.currentLevelScript.seaJumpingPos.transform;
            transform.DOLocalJump(new Vector3(Random.Range(-offsetRange,offsetRange),0,0),3,1,1f).OnComplete(()=>gameObject.SetActive(false));

            return;
        }
        transform.parent = LevelSpawner.Instance.currentLevelScript.roadObjectList[index].transform;
        if (maxhealth != curHealth && curHealth > 0)
        {
            Debug.Log("sa");
            float redRatio = (maxhealth - curHealth) / maxhealth;
            Debug.Log(213 * redRatio);
            Debug.Log(208 * redRatio);
            character.transform.GetComponent<MeshRenderer>().materials[0].DOKill();
            character.transform.GetComponent<MeshRenderer>().materials[0].DOColor(new Color32((byte)(240), (byte)(213 - 213 * redRatio), (byte)(208 - 208 * redRatio), 1), speed).SetSpeedBased();
        }
        transform.DOLocalMove(new Vector3(Random.Range(-offsetRange, offsetRange), 0, 0), speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() =>
        {
            if(isUnderUmbrella == false)
            {
                curHealth -= healthDecreaseAmount;
            }
            if (curHealth == 0)
            {
                transform.DOLocalMoveX(1, 0.5f);
                transform.DOLocalRotate(new Vector3(0,0,90),0.5f);
                AmbulanceGenerator.Instance.CreateAmbulance(peopleIndex);
                return;
            }
            if (curHealth == 1)
            {
                if (dangerousIcon != null)
                {
                    dangerousIcon.SetActive(true);
                }
            }
            MoveToNextIndex(index + 1);
            ChangeRotation(index);
        });

    }

    public void ChangeRotation(int index)
    {
        transform.DOLookAt(LevelSpawner.Instance.currentLevelScript.roadObjectList[index+1].transform.position,0.2f);
    }

    public void MoveShake()
    {
        modelParent.transform.DOLocalRotate(new Vector3(0, 0, -5.5f), curHealth/maxhealth).OnComplete(() =>
        {
            modelParent.transform.DOLocalRotate(new Vector3(0, 0, 5.5f),curHealth / maxhealth).OnComplete(() => MoveShake());
        });
    }
    public void CoolOf()
    {
        if (curHealth < maxhealth)
        {
            curHealth++;
            float redRatio = (maxhealth - curHealth) / maxhealth;
            character.transform.GetComponent<MeshRenderer>().materials[0].DOKill();
            character.transform.GetComponent<MeshRenderer>().materials[0].DOColor(new Color32((byte)(240), (byte)(213 - 213 * redRatio), (byte)(208 - 208 * redRatio), 1), speed).SetSpeedBased();
        }
    }
}
