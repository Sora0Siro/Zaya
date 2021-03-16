using DG.Tweening;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockType
    {
        normal,
        bridge,
        spike,
        carrot,
        finish
    }
    
    [SerializeField] private BlockType _blockType = BlockType.normal;
    
    public BlockType CurrentBlockType => _blockType;
    
    void Start()
    {
        switch (_blockType)
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
                AssignSpike();
                break;
            }
            case Block.BlockType.carrot:
            {
                AssignCarrot();
                break;
            }
            case Block.BlockType.finish:
            {
                AssignFlag();
                break;
            }
        }
    }
    
    private GameObject carrot;
    private GameObject spike;
    private GameObject flag;
    
    public GameObject Carrot => carrot;
    
    #region Assign Child References
    
    private void AssignCarrot()
    {
        foreach (Transform VARIABLE in transform)
        {
            if (VARIABLE.name.ToLower().Contains(BlockType.carrot.ToString()))
            {
                carrot = VARIABLE.gameObject;
                return;
            }
        }
    }
    
    private void AssignFlag()
    {
        //TODO implementation
    }
    
    private void AssignSpike()
    {
        //TODO implementation
    }
    
    #endregion
    
    public void CarrotEvent(float carrotDecreaseSpeed)
    {
        Vector3 startScale = carrot.transform.localScale;
        
        carrot.transform.DOScale(0, carrotDecreaseSpeed).OnComplete(delegate
        {
            carrot.SetActive(false);
            carrot.transform.localScale = startScale;
        });
    }
}