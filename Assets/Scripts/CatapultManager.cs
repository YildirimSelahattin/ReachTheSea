using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    public GameObject catapultArm;
    public GameObject fireArm;

    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float range = 15f;
    public int price;
    public int upgradePrice;
    [Header("Unity Setup Fields")]

    public Transform partToRotate;
    public float turnSpeed = 10f;
    public GameObject previewObject;
    public GameObject bulletPreFab;
    public Transform firePoint;
    public bool isInAnimation = false;
    public GameObject bulletGo;
    public int coolEffectPower;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("wer");
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
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
            fireCountdown = 1f / fireRate;
            isInAnimation = true;
            Shoot();
        }
        if(isInAnimation == false)
        {
            fireCountdown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        Debug.Log("sa");
        fireArm.transform.DOLocalRotate(new Vector3(-30, -90, -90), 0.3f).OnComplete(() =>
        {
            fireArm.transform.DOLocalRotate(new Vector3(-90, -90, -90), 0.3f);
            catapultArm.transform.DOLocalRotate(new Vector3(0, -80, 0), 0.2f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                BallonBullet bullet = bulletGo.GetComponent<BallonBullet>();

                if (bullet != null)
                {
                    bullet.Seek(target, coolEffectPower);
                }
                isInAnimation = false;
                bullet = null;
                catapultArm.transform.DOLocalRotate(new Vector3(0, -20, 0), 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
                {
                    bulletGo = Instantiate(bulletPreFab, firePoint.transform);
                });
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
        if (GameDataManager.Instance.totalMoney > upgradePrice)
        {
            coolEffectPower *= 2;
            upgradePrice *= 2;
            transform.DOScale(transform.localScale * 1.2f, 1f).OnComplete(() =>
            {
                //BURADA BÝR PARTÝCLE EFFECT GEREKLÝ
            });
            GameDataManager.Instance.totalMoney -= upgradePrice;
            UIManager.Instance.moneyText.text = GameDataManager.Instance.totalMoney.ToString();
        }

    }
    public void Sell()
    {
        Debug.Log("SUNSCREEN");
        GameDataManager.Instance.totalMoney -= price / 2;
        UIManager.Instance.moneyText.text = GameDataManager.Instance.totalMoney.ToString();
        transform.DOShakeRotation(1, 50, 3, 50).OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    }
}
