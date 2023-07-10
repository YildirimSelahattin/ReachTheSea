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
    public int _coolEffectPower;
    public GameObject model;
    public void Seek(Transform _target, int coolEffectPower)
    {
        target = _target;
        ShootTarget(coolEffectPower);
    }

    public void ShootTarget(int coolEffectPower)
    {
        _coolEffectPower = coolEffectPower;
        transform.parent = target.transform;
        transform.DOLocalJump(new Vector3(0, 4, 0), 1, 1, speed).OnComplete(() =>
        {
            Destroy(this.gameObject, 0.2f);
            model.GetComponent<MeshRenderer>().enabled = false;
            GameObject effectIns = Instantiate(impactEffect, transform.position, impactEffect.transform.rotation);
            Debug.Log(target.name);
            target.gameObject.GetComponent<PeopleManager>().CoolOf(_coolEffectPower);

        });

    }
    // Update is called once per frame


    void Update()
    {



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