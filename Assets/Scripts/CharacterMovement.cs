using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject bodyMesh;

    private Vector3 bodyStartPos;

    void Start()
    {
        bodyStartPos = body.transform.position;
    }

    public Vector3 BodyStartPos => bodyStartPos;

    public void MoveToPosition(Vector3 targetPos, bool x, bool y, bool z, float characterMoveSpeed)
    {
        var transform1 = body.transform;
        Vector3 prevPos = transform1.position;
        if(x)
            prevPos.x = targetPos.x;
        if(y)
            prevPos.y = targetPos.y;
        if(z)
            prevPos.z = targetPos.z;
        body.transform.DOMove(prevPos, characterMoveSpeed);
    }
    
    public void MoveToPosition(Vector3 targetPos, float characterMoveSpeed)
    {
        body.transform.DOMove(targetPos, characterMoveSpeed);
    }
    
    public void AssignRotation(Swipes.SwipeDirection swipeDirection)
    {
        switch (swipeDirection)
        {
            case Swipes.SwipeDirection.down:
            {
                bodyMesh.transform.localRotation = Quaternion.Euler(Vector3.zero);
                break;
            }
            case Swipes.SwipeDirection.up:
            {
                bodyMesh.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                break;
            }
            case Swipes.SwipeDirection.left:
            {
                bodyMesh.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
//                bodyMesh.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                break;
            }
            case Swipes.SwipeDirection.right:
            {
                bodyMesh.transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
                break;
            }
        }
    }
}