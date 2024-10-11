using System.Net;
using UnityEngine;

public sealed class RectUnitSelector
{
    private ISelectedUnitsStack _stack;

    public RectUnitSelector(ISelectedUnitsStack stack)
    {
        _stack = stack;
    }

    public void SelectUnits(Vector3 startPoint, Vector3 endPoint)
    {
        _stack.ClearUnits();

        Collider2D[] units = Physics2D.OverlapAreaAll(startPoint, endPoint);
        foreach (var unit in units)
        {
            EcsModule.Instance.SelectedUnitsStack.AddUnit(unit.gameObject);
        }
    }
}