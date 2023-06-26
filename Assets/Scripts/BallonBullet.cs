using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonBullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    public float speed = 0.3f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    public GameObject boomEffect;
    public void Seek(Transform _target,int coolEffectPower)
    {
        target = _target.parent;
        ShootTarget(coolEffectPower);
    }

    public void ShootTarget(int coolEffectPower)
    {
        transform.parent = target.transform;
        transform.DOScale(transform.localScale*2,0.15f);
        transform.DOJump(target.transform.position,5f,1, speed).OnComplete(() =>
        {
            Instantiate(boomEffect, new Vector3(target.transform.position.x,13, target.transform.position.z), boomEffect.transform.rotation);
            GameObject effectIns = Instantiate(impactEffect, target.transform.position,impactEffect.transform.rotation);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject,0.6f);
            foreach (Transform child in target.transform)
            {
                PeopleManager script = child.gameObject.GetComponent<PeopleManager>();
                if (script != null)
                {
                    script.CoolOf(coolEffectPower);
                }
            }
            foreach (Transform child in LevelSpawner.Instance.currentLevelScript.roadObjectList[LevelSpawner.Instance.currentLevelScript.roadObjectList.IndexOf(target.gameObject) + 1].transform)
            {
                PeopleManager script = child.gameObject.GetComponent<PeopleManager>();
                if (script != null)
                {
                    script.CoolOf(coolEffectPower);
                }
            }
            foreach (Transform child in LevelSpawner.Instance.currentLevelScript.roadObjectList[LevelSpawner.Instance.currentLevelScript.roadObjectList.IndexOf(target.gameObject) - 1].transform)
            {
                PeopleManager script = child.gameObject.GetComponent<PeopleManager>();
                if (script != null)
                {
                    script.CoolOf(coolEffectPower);
                }
            }
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
