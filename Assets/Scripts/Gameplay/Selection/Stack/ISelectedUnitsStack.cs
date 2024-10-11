using System.Collections.Generic;
using System;
using UnityEngine;

public interface ISelectedUnitsStack
{
    event Action<IEnumerable<GameObject>> OnUnitsChanged;
    event Action<GameObject> OnUnitAdded;
    event Action<GameObject> OnUnitRemoved;
    event Action OnUnitsCleared;

    IReadOnlyCollection<GameObject> GetUnits();

    void SetUnits(params GameObject[] group);
    void AddUnit(GameObject unit);
    void RemoveUnit(GameObject unit);
    void ClearUnits();
}