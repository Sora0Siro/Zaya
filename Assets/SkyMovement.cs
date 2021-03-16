using System;
using DG.Tweening;
using UnityEngine;

public class SkyMovement : MonoBehaviour
{
    [Range(1f, 60f)]
    [SerializeField] private float moveTime;
    [SerializeField] private Vector3 moveAxis;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    
    private void OnEnable()
    {
        MoveToEnd(moveTime/2f);
    }
    
    void MoveToEnd(float time)
    {
        transform.DOMoveX(endPoint.position.x, time);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        transform.DOKill();
        
        var x = moveAxis.x;
        var y = moveAxis.y;
        var z = moveAxis.z;
        
        Vector3 targetPos = startPoint.position;
        Vector3 currPos = transform.position;
        
        if (Math.Abs(x) > 0.1f)
        {
            currPos.x = targetPos.x;
        }
        if (Math.Abs(y) > 0.1f)
        {
            currPos.y = targetPos.y;
        }
        if (Math.Abs(z) > 0.1f)
        {
            currPos.z = targetPos.z;
        }

        transform.position = currPos;
        MoveToEnd(moveTime);
    }
}