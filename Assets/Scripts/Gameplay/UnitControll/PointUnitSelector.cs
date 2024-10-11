using UnityEngine;

public sealed class PointUnitSelector
{
    private ISelectedUnitsStack _stack;

    public PointUnitSelector(ISelectedUnitsStack stack)
    {
        _stack = stack;
    }

    public void SelectUnit(Vector3 position)
    {
        if (UnitAtPoint(position, out var target))
        {
            _stack.SetUnits(target);
        }
        else
        {
            _stack.ClearUnits();
        }
    }

    public void AddUnit(Vector3 position)
    {
        if (UnitAtPoint(position, out var target))
        {
            _stack.AddUnit(target);
        }
    }

    public void RemoveUnit(Vector3 position)
    {
        if (UnitAtPoint(position, out var target))
        {
            _stack.RemoveUnit(target);
        }
    }

    private bool UnitAtPoint(Vector3 position, out GameObject unit)
    {
        unit = default;

        Collider2D obj = Physics2D.OverlapPoint(position);
        if (obj == null)
        {
            Debug.Log("NO RAYCAST TO POINT");
            return false;
        }

        var targetTransform = obj.transform;
        if (targetTransform.CompareTag("Unit"))
        {
            Debug.Log("NO TAG <UNIT>");
            return false;
        }

        unit = targetTransform.gameObject;
        Debug.Log($"SELECTED <UNIT> {unit.name}");
        return true;
    }

    private bool RaycastUnit(Ray ray, out GameObject target)
    {
        target = default;

        if (!Physics.Raycast(ray, out var hit))
        {
            Debug.Log("NO RAYCAST TO POINT");
            return false;
        }

        var targetTransform = hit.transform;
        if (targetTransform.CompareTag("Unit"))
        {
            Debug.Log("NO TAG <UNIT>");
            return false;
        }

        target = targetTransform.gameObject;
        Debug.Log($"SELECTED <UNIT> {target.name}");
        return true;
    }
}