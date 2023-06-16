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
    public void Seek(Transform _target,int coolEffectPower)
    {
        target = _target;
        ShootTarget(coolEffectPower);
    }

    public void ShootTarget(int coolEffectPower)
    {
        transform.parent = target.transform;
        transform.DOLocalJump(new Vector3(0,4,0),1,1, speed).OnComplete(() =>
        {
            Debug.Log("asd");
            GameObject effectIns = Instantiate(impactEffect, transform.position, impactEffect.transform.rotation);
            target.gameObject.GetComponent<PeopleManager>().CoolOf(coolEffectPower);
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