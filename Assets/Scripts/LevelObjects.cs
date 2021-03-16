using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjects : MonoBehaviour
{
    [SerializeField] private List<GameObject> carrots;

    public void CarrotsState(bool state)
    {
        foreach (var VARIABLE in carrots)
        {
            VARIABLE.SetActive(state);
        }
    }
}