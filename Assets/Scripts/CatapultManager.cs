using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    public GameObject sunscreenGuy;
    public GameObject tube;


    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float range = 15f;

    [Header("Unity Setup Fields")]

    public Transform partToRotate;
    public Transform partToRotate2;
    public float turnSpeed = 10f;
    public GameObject previewObject;
    public GameObject bulletPreFab;
    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
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
        partToRotate.parent.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        partToRotate2.parent.rotation = Quaternion.Euler(0f, rotation.y, 0f);

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

                GameObject bulletGO = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
                Bullet bullet = bulletGO.GetComponent<Bullet>();

                if (bullet != null)
                {
                    bullet.Seek(target);
                }

            });
        });

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
