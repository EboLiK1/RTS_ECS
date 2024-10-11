using System;
using UnityEngine;

[Serializable]
public struct SelectionDataComponent
{
    public MouseInput Mouse;

    public Vector3 StartPoint;
    public Vector3 EndPoint;
    public bool IsSelecting;
}