using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private CharacterAnimations _characterAnimations;
    [SerializeField] private CharacterActivation _characterActivation;
    
    [SerializeField] private UIActivation uiActivation;
    
    [SerializeField] private Swipes swipes;
    
    [SerializeField] private float carrotDecreaseSpeed = 1f;
    [SerializeField] private float characterMoveTime = 1f;
    [SerializeField] private float showRestartButtonTime = 3f;
    [SerializeField] private float returnToStartTime = 0.1f;
    [SerializeField] private float fadePanelTime = 2f;
    [SerializeField] private float spikeEndGameDelay = 5f;
    
    [SerializeField] private LevelObjects _levelObjects;
    
    private static bool active = true;
    private static bool inactive = false;

    public float jumpDelay = 0.2f;
    
    public void Awake()
    {
        Instance = this;
        swipes.inputAllowed = true;
    }
    
    public void MoveCharacterToPosition(Vector3 targetPos, bool x, bool y, bool z)
    {
        _characterMovement.MoveToPosition(targetPos, x, y, z, characterMoveTime);
    }

    public IEnumerator ActionWithDelay(Action targetAction, float delay)
    {
        yield return new WaitForSeconds(delay);
        targetAction?.Invoke();
    }
    
    public void MoveCharacterToPosition(Vector3 targetPos, float time)
    {
        _characterMovement.MoveToPosition(targetPos, time);
    }
    
    public void CharacterJumpAnim()
    {
        _characterAnimations.Jump();
    }
    
    public void RotateCharacterMesh(Swipes.SwipeDirection swipeDirection)
    {
        _characterMovement.AssignRotation(swipeDirection);
    }
    
    public void StepOnBlock(Block block)
    {
        switch (block.CurrentBlockType)
        {
            case Block.BlockType.normal:
            {
                break;
            }
            case Block.BlockType.bridge:
            {
                break;
            }
            case Block.BlockType.spike:
            {
                Invoke(nameof(Spike), characterMoveTime);
                break;
            }
            case Block.BlockType.carrot:
            {
                Carrot(block);
                break;
            }
            case Block.BlockType.finish:
            {
                swipes.inputAllowed = false;
                Invoke(nameof(Finish), characterMoveTime);
                break;
            }
        }
    }
    
    public void Carrot(Block block)
    {
        block.CarrotEvent(carrotDecreaseSpeed);
    }
    
    public void Spike()
    {
        swipes.inputAllowed = false;
        _characterActivation.BodyState(false);
        Invoke(nameof(FadeRestart), spikeEndGameDelay);
    }
    
    public void FadeRestart()
    {
        uiActivation.FadePanelState(true);
        //TEMP
        Invoke(nameof(RestartLevel), fadePanelTime / 2f);
        Invoke(nameof(HideFadePanel), fadePanelTime);
    }
    
    public void Finish()
    {
        _characterAnimations.Finish();
        Invoke(nameof(ShowRestartButton), showRestartButtonTime);
    }
    
    void ShowRestartButton()
    {
        uiActivation.RestartButtonState(true);
    }

    void HideFadePanel()
    {
        uiActivation.FadePanelState(false);
    }
    
    public void RestartLevel()
    {
        uiActivation.RestartButtonState(false);
        ReturnToStartPosition();
        ReturnToStartRotation();
        _levelObjects.CarrotsState(active);
        _characterActivation.BodyState(active);
        _characterAnimations.Idle();
        swipes.inputAllowed = true;
    }
    
    void ReturnToStartPosition()
    {
        MoveCharacterToPosition(_characterMovement.BodyStartPos, returnToStartTime);
    }

    void ReturnToStartRotation()
    {
        RotateCharacterMesh(Swipes.SwipeDirection.down);
    }
}