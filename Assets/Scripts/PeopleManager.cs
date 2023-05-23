using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float maxhealth;
    public float curHealth;
    public List<int> positionIndexList;
    Vector3 leftDirection = new Vector3(0, -90, 0);
    Vector3 rightDirection = new Vector3(0, 90, 0);
    Vector3 upDirection = new Vector3(0, 180, 0);
    Vector3 downDirection = new Vector3(0, -180, 0);
    Color originalSkinColor;
    public GameObject modelParent;
    public GameObject character;
    public GameObject dangerousIcon;
    public int peopleIndex;
    void Start()
    {
        positionIndexList = GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel - 1].roadIndexes;
        curHealth = maxhealth;
        originalSkinColor = character.GetComponent<MeshRenderer>().materials[0].color;
        MoveToNextIndex(0);
        MoveShake();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MoveToNextIndex(int index)
    {
        Debug.Log("qwe");
        if (index == positionIndexList.Count)
        {
            gameObject.SetActive(false);
            return;
        }
       
        transform.parent = LevelSpawner.Instance.gridObjectsList[positionIndexList[index]].transform;
        if (maxhealth!=curHealth && curHealth>0)
        {
            Debug.Log("sa");
            float redRatio = (maxhealth - curHealth) / maxhealth;
            Debug.Log(213 * redRatio);
            Debug.Log(208 * redRatio);
            character.transform.GetComponent<MeshRenderer>().materials[0].DOKill();
            character.transform.GetComponent<MeshRenderer>().materials[0].DOColor(new Color32((byte)(240), (byte)(213-213*redRatio), (byte)(208-208*redRatio), 1), speed ).SetSpeedBased();
        }
        transform.DOLocalMove(new Vector3(0, 0, 0), speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() =>
        {
            curHealth--;
            if (curHealth == 0)
            {
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

    public void MoveShake(float speed)
    {

    }
    public void ChangeRotation(int index)
    {
        if (positionIndexList[index] + 1 == positionIndexList[index + 1])
        {
            //goign right
            if (transform.localRotation.eulerAngles != rightDirection)
            {
                //not turned right already
                transform.DOLocalRotate(rightDirection, 0.2f);
            }
        }
        else if (positionIndexList[index] - 1 == positionIndexList[index + 1])
        {

            //turn left
            if (transform.localRotation.eulerAngles != leftDirection)
            {
                //not turned left already
                transform.DOLocalRotate(leftDirection, 0.2f);
            }
        }
        else if (positionIndexList[index] - GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel - 1].gridWidth == positionIndexList[index + 1])
        {
            //turn up
            if (transform.localRotation.eulerAngles != upDirection)
            {
                transform.DOLocalRotate(upDirection, 0.2f);
            }
        }
        else if (positionIndexList[index] + GameDataManager.Instance.data.levelsArray[GameDataManager.Instance.currentLevel - 1].gridWidth == positionIndexList[index + 1])
        {
            if (transform.localRotation.eulerAngles != downDirection)
            {
                transform.DOLocalRotate(downDirection, 0.2f);
            }
        }
    }

    public void MoveShake()
    {
        modelParent.transform.DOLocalRotate(new Vector3(0, 0, -5.5f), speed *3*(maxhealth-curHealth)).SetSpeedBased().OnComplete(() =>
        {
            modelParent.transform.DOLocalRotate(new Vector3(0, 0, 5.5f), speed *3* (maxhealth - curHealth)).SetSpeedBased().OnComplete(() => MoveShake());
        });
    }
}
