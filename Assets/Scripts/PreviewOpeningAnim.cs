using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewOpeningAnim : MonoBehaviour
{
    public Vector3 originalScale;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.DOKill();
        transform.localScale = Vector3.zero;
        transform.DOScale(originalScale,0.5f);
        Debug.Log("sasa");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDisable()
    {
        transform.DOKill();
        transform.DOScale(Vector3.zero, 0.5f);
    }
}
