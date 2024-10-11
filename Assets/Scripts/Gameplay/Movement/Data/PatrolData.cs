using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct PatrolData
{
    public List<Vector3> Points;
    public int Pointer;

    public float StoppingDistance;

    public Vector3 GetCurrentPoint()
    {
        return Points[Pointer];
    }

    public void MoveNext()
    {
        Pointer = (Pointer + 1) % Points.Count;
    }

    public void AddPoint(Vector3 point)
    {
        Points.Add(point);
    }
}