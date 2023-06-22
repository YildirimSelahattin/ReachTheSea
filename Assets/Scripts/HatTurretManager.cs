using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class HatTurretManager : MonoBehaviour
{
    private Transform target;
    public GameObject firePeople;
    public SkinnedMeshRenderer skinnedMeshCannon;
    public float waitingTime;
    public GameObject particleEffect;
    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float range = 15f;
    public List<GameObject> starList;

    [Header("Unity Setup Fields")]

    public Transform partToRotate;
    public float turnSpeed = 10f;
    public GameObject previewObject;
    public GameObject bulletPreFab;
    public Transform firePoint;
    int stageKeyIndex;
    float maxKeyValue = 15;
    float tweenDuration = 0.2f;
    public int price;
    public int upgradePrice;
    public int deletePrice;
    public bool isInShootAnimation = false;
    public float[] tweeningKeyVariables = new float[7];
    public int hatEffectPower;
    public GameObject upgradeEffect;
    public int machineLevel=1;
    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("UpdateTarget", 0f, 0.4f);
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
            StartCoroutine(StartMoveAfterTime(0));
            isInShootAnimation = true;
            fireCountdown = 1f / fireRate;
        }
        if (isInShootAnimation != true)
        {
            fireCountdown -= Time.deltaTime;
        }
    }
    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
        hatBullet bullet = bulletGO.GetComponent<hatBullet>();
        particleEffect.SetActive(true);
        if (bullet != null)
        {
            bullet.Seek(target, hatEffectPower);
        }
    }

    public void AnimateCannonExplosion(int index)
    {
        Tween keyTween = DOTween.To(() => tweeningKeyVariables[index],
        x => tweeningKeyVariables[index] = x, 100, tweenDuration).OnComplete(() =>
        {
            Tween expandTween = DOTween.To(() => tweeningKeyVariables[index],
            x => tweeningKeyVariables[index] = x, 0, tweenDuration);
            expandTween.OnUpdate(() => UpdateCannonMesh(index, tweeningKeyVariables[index])).OnComplete(() =>
            {
                if(index == 5)
                {
                    isInShootAnimation = false;
                    Shoot();
                }
            });
        });
    }

    public void UpdateCannonMesh(int index, float tweeningKeyVariable)
    {
        skinnedMeshCannon.SetBlendShapeWeight(index, tweeningKeyVariable);
    }

    public IEnumerator StartMoveAfterTime(int index)
    {
        yield return new WaitForSeconds(0.05f);
        AnimateCannonExplosion(index);
        if (index < 7)
        {
            StartCoroutine(StartMoveAfterTime(index + 1));
        }
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
            transform.parent.gameObject.GetComponent<MachineSpotManager>().ResetAndClose();
            Debug.Log("buyut");
            machineLevel++;
            OpenStars(machineLevel);
            hatEffectPower *= 2/3;
            upgradePrice *= 2;
            upgradeEffect.SetActive(true);
            range += 1;
            transform.DOScale(transform.localScale * 1.1f, 1f).OnComplete(() =>
            {
                //BURADA BÝR PARTÝCLE EFFECT GEREKLÝ
            });
            GameDataManager.Instance.totalMoney -= upgradePrice;
            upgradePrice = (int)(upgradePrice * 1.5f);
            deletePrice =(int)( deletePrice * 1.5f);
            UIManager.Instance.moneyText.text = GameDataManager.Instance.totalMoney.ToString();
        }

    }
    public void Sell()
    {
        Debug.Log("Hat");
        GameDataManager.Instance.totalMoney -= price / 2;
        UIManager.Instance.moneyText.text = GameDataManager.Instance.totalMoney.ToString();
        transform.DOScale(Vector3.one*0.2f,1).OnComplete(() =>
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
        if(level == 1)
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
