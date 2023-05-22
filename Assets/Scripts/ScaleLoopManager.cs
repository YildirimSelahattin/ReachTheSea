using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLoopManager : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 originalScale;
    void Start()
    {
        originalScale= transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ScaleLoop()
    {
        transform.DOScale(originalScale * 1.2f, 0.5f).OnComplete(() =>
        {
            transform.DOScale(originalScale, 0.5f).OnComplete(() =>
            {
                ScaleLoop();
            });
        });
    }
    private void OnDisable()
    {
        transform.DOKill();
    }
}
