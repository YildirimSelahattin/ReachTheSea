using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;
    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    public void Seek(Transform _target)
    {
        target = _target;
        ShootTarget();
    }

    public void ShootTarget()
    {
        transform.parent = target.transform;
        transform.DOLocalMove(new Vector3(0,3,0), speed).OnComplete(() =>
        {
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            target.gameObject.GetComponent<PeopleManager>().CoolOf();
            Destroy(gameObject);
        });

    }
    // Update is called once per frame


    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
           
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People"))// it hitted to people 
        {
        }
    }
 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}