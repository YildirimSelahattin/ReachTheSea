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
    bool isInShootAnimation = false;
    public float[] tweeningKeyVariables = new float[7];

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
        /*
         *        if (target == null)
               {
                   return;
               }
               //target lock on
               Vector3 dir = target.position - transform.position;
               Quaternion lookRotation = Quaternion.LookRotation(dir);
               Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
               partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        */
        if (fireCountdown <= 0f)
        {
            StartCoroutine(StartMoveAfterTime(0));
            isInShootAnimation = false;
            fireCountdown = 1f / fireRate;
        }
        if(isInShootAnimation != true)
        {
            fireCountdown -= Time.deltaTime;
        }

      
    }
    void Shoot()
    {
        Debug.Log("shooooooooooooooot");
        transform.DORotate(new Vector3(-12,0,0),0.1f);
        GameObject bulletGO = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    public void AnimateCannonExplosion(int index)
    {
        Tween keyTween = DOTween.To(() => tweeningKeyVariables[index],
        x => tweeningKeyVariables[index] = x,30, tweenDuration).OnComplete(() =>
        {
            Tween expandTween = DOTween.To(() => tweeningKeyVariables[index],
            x => tweeningKeyVariables[index] = x, 0, tweenDuration);
            expandTween.OnUpdate( ()=>UpdateCannonMesh(index, tweeningKeyVariables[index]));
        });
    }

    public void UpdateCannonMesh(int index,float tweeningKeyVariable)
    {
        Debug.Log("sa");
        skinnedMeshCannon.SetBlendShapeWeight(index,tweeningKeyVariable);
    }

    public IEnumerator StartMoveAfterTime(int index)
    {
        yield return new WaitForSeconds(0.05f);
        AnimateCannonExplosion(index);
        if(index <7)
        {
            StartCoroutine(StartMoveAfterTime(index+1));
        }
    }
    public void OnCannonMeshCompleted()
    {
        if (stageKeyIndex == 6)
        {
            isInShootAnimation = false;
            Shoot();
        }
        else
        {
            stageKeyIndex++;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
