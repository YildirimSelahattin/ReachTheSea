using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

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

    [Header("Unity Setup Fields")]

    public Transform partToRotate;
    public float turnSpeed = 10f;
    public GameObject previewObject;
    public GameObject bulletPreFab;
    public Transform firePoint;
    int stageKeyIndex;
    float maxKeyValue = 15;
    float tweenDuration = 0.3f;
    public int price;
    public int upgradePrice;
    public bool isInShootAnimation = false;
    public float[] tweeningKeyVariables = new float[7];
    public int hatEffectPower;
    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("UpdateTarget", 0f, 0.4f);
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
        if (GameDataManager.Instance.totalMoney > upgradePrice)
        {
            hatEffectPower *= 2/3;
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
