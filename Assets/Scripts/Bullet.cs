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
    }

    public void ShootTarget()
    {

        transform.DOMove(target.position, speed).SetSpeedBased(true);
    }
    // Update is called once per frame


    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        ShootTarget();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("People"))// it hitted to people 
        {
            //GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            //Destroy(effectIns, 5f);
            other.gameObject.GetComponent<PeopleManager>().CoolOf();
            Destroy(gameObject);
        }
    }
 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}