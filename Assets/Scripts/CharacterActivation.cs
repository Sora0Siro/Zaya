using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActivation : MonoBehaviour
{
    [SerializeField] private GameObject characterMesh;
    [SerializeField] private GameObject characterGhost;
    
    public void BodyState(bool state)
    {
        if (state)
        {
            characterMesh.SetActive(true);
            characterGhost.SetActive(false);
        }
        else
        {
            characterMesh.SetActive(false);
            characterGhost.SetActive(true);
        }
    }
}