using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToPeople(GameObject people)
    {
        Vector3 pos = people.transform.position;
        pos.y += 0.5f;
        transform.DOMove(pos, 2f).OnComplete(() =>
        {
            people.transform.parent = transform;
            Vector3 temp = transform.position;
            temp.y +=1.68f;

            people.transform.DOLocalRotate(new Vector3(73, 161, 69), 0.5f);
            people.transform.DOLocalMove(new Vector3(-0.013f, -0.0013f, 0.016f), 0.5f).OnComplete(() =>
            {
                transform.DOLocalMove(new Vector3(0,0,0),2f).OnComplete(()=>Destroy(this.gameObject));
            });
        });
    }
}
