
using System;
using UnityEngine;
using DG.Tweening;
public class Enemy : MonoBehaviour
{
    
    public float speed = 3f;
    private Transform target;
    private int wavePointIndex = 0;

    void Start()
    {
        target = Waypoints.points[0];
    }

    void Update()
    {
        transform.DOMove(target.position,speed).SetSpeedBased(true);
        if (Vector3.Distance(transform.position,target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

     void GetNextWaypoint()
     {
         if (wavePointIndex >= Waypoints.points.Length-1)
         {
             Destroy(gameObject);
             return;
         }
         wavePointIndex++;
         target = Waypoints.points[wavePointIndex];
     }

    
}
