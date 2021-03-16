using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGatherer : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToGather;
    
    private List<Vector3> positions;
    private List<Rigidbody> bRigidbodies;
    
    private void Awake()
    {
        positions = new List<Vector3>();
        bRigidbodies = new List<Rigidbody>();
        
        foreach (var VARIABLE in objectsToGather)
        {
            positions.Add(VARIABLE.transform.localPosition);
            bRigidbodies.Add(VARIABLE.GetComponent<Rigidbody>());
        }
    }
    
    void ResetPositions()
    {
        for (int i = 0; i < objectsToGather.Count; i++)
        {
            objectsToGather[i].transform.localPosition = positions[i];
            bRigidbodies[i].velocity = Vector3.zero;
        }
    }
    
    private void OnDisable()
    {
        ResetPositions();
    }
}