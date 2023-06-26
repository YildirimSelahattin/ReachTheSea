using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    public List<GameObject> coins;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCoins(Transform target)
    {
        foreach (GameObject coin in coins)
        {
            coin.transform.DOMove(target.position,1f).SetEase(Ease.InOutBack).OnComplete(()=>Destroy(coin));
        }
    }
}
