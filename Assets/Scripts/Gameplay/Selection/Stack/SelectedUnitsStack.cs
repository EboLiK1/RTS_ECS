using System.Collections.Generic;
using System;
using UnityEngine;

public sealed class SelectedUnitsStack : MonoBehaviour, ISelectedUnitsStack
{
    public event Action<IEnumerable<GameObject>> OnUnitsChanged;
    public event Action<GameObject> OnUnitAdded;
    public event Action<GameObject> OnUnitRemoved;
    public event Action OnUnitsCleared;

    private readonly HashSet<GameObject> _selectedUnits = new();

    public IReadOnlyCollection<GameObject> GetUnits()
    {
        return _selectedUnits;
    }

    public void SetUnits(params GameObject[] group)
    {
        _selectedUnits.Clear();
        _selectedUnits.UnionWith(group);
        OnUnitsChanged?.Invoke(group);
    }

    public void AddUnit(GameObject unit)
    {
        if (_selectedUnits.Add(unit))
        {
            OnUnitAdded?.Invoke(unit);
        }
    }

    public void RemoveUnit(GameObject unit)
    {
        if (_selectedUnits.Remove(unit))
        {
            OnUnitRemoved?.Invoke(unit);
        }
    }

    public void ClearUnits()
    {
        _selectedUnits.Clear();
        OnUnitsCleared?.Invoke();
    }
}