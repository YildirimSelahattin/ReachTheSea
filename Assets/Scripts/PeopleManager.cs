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
    public float offsetRange = 0.002f;
    Vector3 leftDirection = new Vector3(0, -90, 0);
    Vector3 rightDirection = new Vector3(0, 90, 0);
    Vector3 upDirection = new Vector3(0, 180, 0);
    Vector3 downDirection = new Vector3(0, -180, 0);
    Color originalSkinColor;
    public GameObject rotatingPart;
    public GameObject modelParent;
    public GameObject character;
    public GameObject dangerousIcon;
    public int peopleIndex;
    public int isUnderUmbrella;
    public GameObject umbrellaObject;
    public float reachMoney = 10;
    public GameObject healEffect;
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
        if (index == LevelSpawner.Instance.currentLevelScript.roadObjectList.Count - 1)// if it reach to sea
        {
            Debug.Log("qwe");
            transform.parent = LevelSpawner.Instance.currentLevelScript.seaJumpingPos.transform;
            transform.DOLocalJump(new Vector3(Random.Range(-offsetRange, offsetRange), 0, 0), 3, 1, 1f).OnComplete(() =>
            {
                gameObject.SetActive(false);
                GameManager.Instance.currentMoney += 50;
                GameDataManager.Instance.totalMoney += (int)reachMoney;
                UIManager.Instance.moneyParticle.SetActive(true);
            });
            return;
        }
        transform.parent = LevelSpawner.Instance.currentLevelScript.roadObjectList[index].transform;
        if (maxhealth != curHealth && curHealth > 0)
        {
            float redRatio = (maxhealth - curHealth) / maxhealth;
            character.transform.GetComponent<MeshRenderer>().materials[0].DOKill();
            character.transform.GetComponent<MeshRenderer>().materials[0].DOColor(new Color32((byte)(240), (byte)(213 - 213 * redRatio), (byte)(208 - 208 * redRatio), 1), speed).SetSpeedBased();
        }
        transform.DOLocalMove(new Vector3(Random.Range(-offsetRange, offsetRange), 0, 0), speed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() =>
        {
            if (isUnderUmbrella > 0)
            {
                curHealth -= healthDecreaseAmount;
                isUnderUmbrella -= 1;
                if(isUnderUmbrella == 0)
                {
                    umbrellaObject.SetActive(false);
                }
            }
            if (curHealth == 0)
            {
                modelParent.transform.DOKill();
                transform.DOKill();
                transform.parent = null;
                transform.DOLocalRotate(new Vector3(0, 0, 90), 0.5f);
                PeopleGenerator.Instance.peopleObjectList.Remove(gameObject);
                AmbulanceGenerator.Instance.CreateAmbulance(gameObject);
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
        rotatingPart.transform.DOLookAt(LevelSpawner.Instance.currentLevelScript.roadObjectList[index + 1].transform.position, 0.2f);
    }

    public void MoveShake()
    {
        modelParent.transform.DOLocalRotate(new Vector3(0, 0, -5.5f), curHealth / maxhealth).OnComplete(() =>
        {
            modelParent.transform.DOLocalRotate(new Vector3(0, 0, 5.5f), curHealth / maxhealth).OnComplete(() => MoveShake());
        });
    }
    public void CoolOf(int healthIncrease)
    {
        if (curHealth < maxhealth)
        {
            curHealth+= healthIncrease;
            float redRatio = (maxhealth - curHealth) / maxhealth;
            healEffect.SetActive(true);
            character.transform.GetComponent<MeshRenderer>().materials[0].DOKill();
            character.transform.GetComponent<MeshRenderer>().materials[0].DOColor(new Color32((byte)(240), (byte)(213 - 213 * redRatio), (byte)(208 - 208 * redRatio), 1), speed).SetSpeedBased();
        }
    }
}
