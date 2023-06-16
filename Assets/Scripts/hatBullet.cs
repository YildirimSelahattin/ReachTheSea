using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hatBullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    public void Seek(Transform _target,int hatEffectPower)
    {
        target = _target;
        target.gameObject.GetComponent<PeopleManager>().umbrellaObject = gameObject;
        ShootTarget(hatEffectPower);
    }

    public void ShootTarget( int hatEffectPower)
    {
        transform.parent = target.transform.GetChild(0);
        transform.DOScale(new Vector3(88,88,88),0.5f);
        transform.DOLocalRotate(new Vector3(-90,0,0),0.5f);
        transform.DOLocalMove(new Vector3(0, 1, 0), speed).OnComplete(() =>
        {
            GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
            target.gameObject.GetComponent<PeopleManager>().isUnderUmbrella = hatEffectPower;
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
