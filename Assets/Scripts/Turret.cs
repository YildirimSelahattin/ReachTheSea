using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    public GameObject sunscreenGuy;
    public GameObject tube;


    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float range = 15f;
    public int coolEffectPower;
    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public GameObject previewObject;
    public GameObject bulletPreFab;
    public Transform firePoint;
    public int price;
    public int upgradePrice;
    public int deletePrice;
    public List<GameObject> starList;
    public int machineLevel=1;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
        upgradePrice = (int)(price * 1.5f);
        deletePrice = price / 2;
        OpenStars(1);
    }

    void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject people in PeopleGenerator.Instance.peopleObjectList)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, people.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = people;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        //target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        float originalYPos = sunscreenGuy.transform.localPosition.y;
        sunscreenGuy.transform.DOLocalMoveY(originalYPos + 2, 0.5f).OnComplete(() =>
        {
            sunscreenGuy.transform.DOLocalMoveY(originalYPos, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                float originalZScale = tube.transform.localScale.z;
                tube.transform.DOScaleZ(originalZScale * 0.8f, 0.2f).OnComplete(() =>
                {
                    tube.transform.DOScaleZ(originalZScale, 0.2f);
                });
                if (target != null)
                {
                    GameObject bulletGO = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
                    Bullet bullet = bulletGO.GetComponent<Bullet>();

                    if (bullet != null)
                    {
                        bullet.Seek(target, coolEffectPower);
                    }
                }
            });
        });
        
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void Upgrade()
    {
        if (GameDataManager.Instance.totalMoney >= upgradePrice)
        {
            GameDataManager.Instance.totalMoney -= upgradePrice;
            UIManager.Instance.moneyText.text = GameDataManager.Instance.totalMoney.ToString();
            transform.parent.gameObject.GetComponent<MachineSpotManager>().ResetAndClose();
            coolEffectPower *= 2;
            range += 1;
            machineLevel++;
            OpenStars(machineLevel);
            transform.DOScale(transform.localScale * 1.1f, 1f).OnComplete(() =>
            {
                //BURADA BÝR PARTÝCLE EFFECT GEREKLÝ
            });
            
            upgradePrice = (int)(upgradePrice * 1.5f);
            deletePrice = (int)(deletePrice * 1.5f);
        }
       
    }
    public void Sell()
    {
        Debug.Log("SUNSCREEN");
        GameDataManager.Instance.totalMoney += price / 2;
        UIManager.Instance.moneyText.text = GameDataManager.Instance.totalMoney.ToString();
        UIManager.Instance.moneyParticle.SetActive(true);
        transform.DOScale(Vector3.one * 0.2f, 0.5f).SetEase(Ease.InOutBack).OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    }
    public void OpenStars(int level)
    {
        foreach (GameObject star in starList)
        {
            star.SetActive(false);
        }
        if (level == 1)
        {
            starList[1].SetActive(true);
        }
        else if (level == 2)
        {
            starList[0].SetActive(true);
            starList[2].SetActive(true);
        }
        else if (level == 3)
        {
            starList[0].SetActive(true);
            starList[1].SetActive(true);
            starList[2].SetActive(true);
        }
    }
}