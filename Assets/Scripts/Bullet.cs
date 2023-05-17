using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;
    public float speed =70f;
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
        if (other.CompareTag("Enemy"))
        {
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(effectIns,5f);
            if (explosionRadius >0)
            {
                Explode();
            }
            else
            {
                Damage(target);
            }
            
            Destroy(gameObject);
            Destroy(other.gameObject);

        }
    }

    void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }

    void Explode()
    {
       Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
       foreach (Collider collider in colliders)
       {
           if (collider.tag == "Enemy")
           {
               Damage(collider.transform);
           }
       }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}
