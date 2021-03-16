using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Swipes : MonoBehaviour
{
    public enum SwipeDirection
    {
        none,
        left,
        right,
        up,
        down
    }
    private Vector3 _firstTouchPos;
    private Vector3 _secondTouch;
    
    [SerializeField] private float angleLimit = 5f;
    [Tooltip("In Pixels")]
    [SerializeField] private float minMoveThreshold = 5f;
    [SerializeField] private float swipeTime = 1f;
    [SerializeField] private bool waitingForSwipes;
    [Header("For Debug")]
    [SerializeField] private float X_AngleDegree = 0f;
    [SerializeField] private float Y_AngleDegree = 0f;
    [SerializeField] private float xDiff = 0f;
    [SerializeField] private float yDiff = 0f;

    public bool inputAllowed = true;
    
    private IEnumerator SwipeTimer()
    {
        waitingForSwipes = false;
        float timer = 0;
        
        while (timer < swipeTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        waitingForSwipes = true;
    }
    
    private SwipeDirection _swipeDirection = SwipeDirection.none;
    
    void Update()
    {
        if(inputAllowed)
            InputHandler();
    }
    
    bool wasUnpressed = true;
    
    void InputHandler()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            _firstTouchPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && waitingForSwipes && wasUnpressed)
        {
            _secondTouch = Input.mousePosition;
            
            PressContinue();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _firstTouchPos = Vector3.zero;
            _secondTouch = Vector3.zero;
            wasUnpressed = true;
        }
#else
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _firstTouchPos = Input.touches[0].position;
            }
            else if ((Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Stationary) && waitingForSwipes && wasUnpressed)
            {
                _secondTouch = Input.touches[0].position;
            
                PressContinue();
            }
            else if (Input.touches[0].phase == TouchPhase.Ended  || Input.touches[0].phase == TouchPhase.Canceled)
            {
                _firstTouchPos = Vector3.zero;
                _secondTouch = Vector3.zero;
                wasUnpressed = true;
            }
        }
#endif
    }
    
    void PressContinue()
    {
        xDiff = Math.Abs(_secondTouch.x) - Math.Abs(_firstTouchPos.x); //2 - 1 = right // 1 - 2 left
        yDiff = Math.Abs(_secondTouch.y) - Math.Abs(_firstTouchPos.y); // 2 - 1 = up  // 1 - 2 = down
        
        var concreteXdiff = Math.Abs(Math.Abs(_secondTouch.x) - Math.Abs(_firstTouchPos.x));
        var concreteYdiff = Math.Abs(Math.Abs(_secondTouch.y) - Math.Abs(_firstTouchPos.y));
        
        X_AngleDegree = (float)(Math.Atan(concreteYdiff / concreteXdiff) * 180 / Math.PI);
        Y_AngleDegree = (float)(Math.Atan(concreteXdiff/ concreteYdiff) * 180 / Math.PI);
        
        if (waitingForSwipes && (Math.Abs(xDiff) > minMoveThreshold || Math.Abs(yDiff) > minMoveThreshold))
        {
            if (X_AngleDegree <= angleLimit)
            {
                if (xDiff > 0)
                {
                    _swipeDirection = SwipeDirection.right;
                }
                else if (xDiff < 0)
                {
                    _swipeDirection = SwipeDirection.left;
                }
                StopCoroutine(nameof(SwipeTimer));
                StartCoroutine(nameof(SwipeTimer));
                BlockCaster.Instance.CastInDirection(_swipeDirection);
                wasUnpressed = false;
            }
            else if (Y_AngleDegree <= angleLimit)
            {
                if (yDiff > 0)
                {
                    _swipeDirection = SwipeDirection.up;
                }
                else if (yDiff < 0)
                {
                    _swipeDirection = SwipeDirection.down;
                }
                StopCoroutine(nameof(SwipeTimer));
                StartCoroutine(nameof(SwipeTimer));
                BlockCaster.Instance.CastInDirection(_swipeDirection);
                wasUnpressed = false;
            }
        }
    }
}