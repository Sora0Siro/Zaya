using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ScaleOnEnable : MonoBehaviour
{
    private Vector3 originScale;
    [SerializeField] private float invokeTime;
    [SerializeField] private float scaleMultiplier;
    [SerializeField] private float scaleTimeUp;
    [SerializeField] private float scaleTimeDown;
    [SerializeField] private bool singleScale;
    [SerializeField] private bool disableOnScaleEnd;
    [SerializeField] private float disableOnEndDelay;

    void Awake()
    {
        var o = gameObject;
        originScale = o.transform.localScale;
    }

    public void SetBools(float t_invokeTime = 0f, 
        float t_scaleMultiplier = 0f,
        float scaleTimeUp = 0,
        float scaleTimeDown = 1f,
        bool singleScale = true,
        bool disableOnScaleEnd = true,
        float disableOnEndDelay = 0f)
    {
        
    }

    void OnEnable()
    {
        gameObject.transform.localScale = Vector3.zero;
        if (disableOnScaleEnd && singleScale)
            Invoke(nameof(SingleScaleDisable), invokeTime);
        else if(disableOnScaleEnd && !singleScale)
            Invoke(nameof(ScaleUpDownDisable), invokeTime);
        else if(singleScale)
            Invoke(nameof(SingleScale), invokeTime);
        else
            Invoke(nameof(ScaleUpDown), invokeTime);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator WaitAndCall(float time, Action call) 
    {
        yield return new WaitForSeconds(time);
        call?.Invoke();
    }
    
    void SingleScale()
    {
        gameObject.transform.DOScale(originScale * scaleMultiplier, scaleTimeUp);
    }
    
    void SingleScaleDisable()
    {
        gameObject.transform.DOScale(originScale * scaleMultiplier, scaleTimeUp).OnComplete(
            delegate
            {
                gameObject.transform.DOScale(originScale, scaleTimeDown).OnComplete(delegate
                {
                    if(disableOnScaleEnd)
                        StartCoroutine(WaitAndCall(disableOnEndDelay, DisableObject));
                });
            });
    }
    
    void ScaleUpDown()
    {
        gameObject.transform.DOScale(originScale * scaleMultiplier, scaleTimeUp)
            .OnComplete(delegate { gameObject.transform.DOScale(originScale, scaleTimeDown); });
    }
    
    void ScaleUpDownDisable()
    {
        gameObject.transform.DOScale(originScale * scaleMultiplier, scaleTimeUp)
            .OnComplete(delegate
            {
                gameObject.transform.DOScale(originScale, scaleTimeDown).OnComplete(delegate
                    {
                        if(disableOnScaleEnd)
                            StartCoroutine(WaitAndCall(disableOnEndDelay, DisableObject));
                    });
            });
    }

    void DisableObject()
    {
        gameObject.SetActive(false);
    }
}