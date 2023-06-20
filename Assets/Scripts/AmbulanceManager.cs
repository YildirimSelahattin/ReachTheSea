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
            Vector3 temp = transform.position;
            temp.y +=1.68f;
            people.transform.DOJump(temp, 4, 1, 0.5f).OnComplete(() =>
            {
                people.transform.parent = transform;
                transform.DOLocalMove(new Vector3(0,0,0),2f).OnComplete(()=>Destroy(this.gameObject));
            });
        });
    }
}
