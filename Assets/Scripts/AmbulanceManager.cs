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

    public void MoveToPeople(int peopleIndex)
    {
        Vector3 pos = PeopleGenerator.Instance.peopleObjectList[peopleIndex].transform.position;
        pos.y = 2;
        transform.DOMove(pos, 2f).OnComplete(() =>
        {
            PeopleGenerator.Instance.peopleObjectList[peopleIndex].transform.parent = transform;
            PeopleGenerator.Instance.peopleObjectList[peopleIndex].transform.DOLocalJump(new Vector3(0, 0, 0), 4, 1, 0.5f).OnComplete(() =>
            {
                transform.DOLocalMove(new Vector3(0,0,0),1f);
            });
        });
    }
}
