using System;
using UnityEngine;

[Serializable]
public struct MoveToPositionData
{
    public bool IsReached;
    public Vector3 Destination;
    public float StoppingDistance;
}