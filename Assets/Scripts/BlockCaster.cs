using System;
using UnityEngine;

public class BlockCaster : MonoBehaviour
{
    [SerializeField] private Transform rayOrigin;
    [Tooltip("Distance In Meters")]
    [SerializeField] private float raycastDistance = 0.1f;

    public static BlockCaster Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void CastInDirection(Swipes.SwipeDirection swipeDirection)
    {
        int xDirection = 0;
        int zDirection = 0;

        switch (swipeDirection)
        {
            case Swipes.SwipeDirection.down:
            {
                zDirection = 1;
                break;
            }
            case Swipes.SwipeDirection.up:
            {
                zDirection = -1;
                break;
            }
            case Swipes.SwipeDirection.left:
            {
                xDirection = 1;
                break;
            }
            case Swipes.SwipeDirection.right:
            {
                xDirection = -1;
                break;
            }
        }
        
        Ray ray = new Ray(rayOrigin.position, rayOrigin.transform.TransformDirection(new Vector3(xDirection, -1, zDirection)));
        
        if (Physics.Raycast(ray, out var raycastHit, raycastDistance))
        {
            Debug.DrawLine(ray.origin, raycastHit.point, Color.green, 1f);
            Debug.Log("hit name: " + raycastHit.collider.gameObject.name);

            Block block = raycastHit.collider.gameObject.GetComponent<Block>();
            
            GameManager.Instance.StepOnBlock(block);
            GameManager.Instance.CharacterJumpAnim();
            
            StartCoroutine(GameManager.Instance.ActionWithDelay(delegate
            {
                GameManager.Instance.MoveCharacterToPosition(block.transform.position, true, false, true);
            }, GameManager.Instance.jumpDelay));
            
            GameManager.Instance.RotateCharacterMesh(swipeDirection);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin+ray.direction, Color.red, 1f);
        }
    }
}