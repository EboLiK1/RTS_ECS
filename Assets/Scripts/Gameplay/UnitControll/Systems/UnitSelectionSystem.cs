using Ecs;
using UnityEngine;

public class UnitSelectionSystem : IUpdateSystem
{
    private const float MIN_RECT_DIAGONAL = 0.5f;

    private readonly ComponentPool<SelectionDataComponent> _selectionDataPool;

    private RectSelectionInputSystem _rectSelectionInput;

    private PointUnitSelector _rayUnitSelector;
    private RectUnitSelector _rectUnitSelector;

    public UnitSelectionSystem(RectSelectionInputSystem rectSelectionInput,
                               PointUnitSelector rayUnitSelector,
                               RectUnitSelector rectUnitSelector)
    {
        _rectSelectionInput = rectSelectionInput;
        _rayUnitSelector = rayUnitSelector;
        _rectUnitSelector = rectUnitSelector;

        _rectSelectionInput.OnFinished += OnRectSelectionFinished;
    }

    public void OnUpdate(int entityIndex)
    {
        
    }

    private void OnRectSelectionFinished(int entityIndex)
    {
        ref var selectionData = ref _selectionDataPool.GetComponent(entityIndex);

        if (Vector3.Distance(selectionData.StartPoint, selectionData.EndPoint) < MIN_RECT_DIAGONAL)
        {
            SelectAsPoint(selectionData.EndPoint);
        }
        else
        {
            SelectAsRect(selectionData.StartPoint, selectionData.EndPoint);
        }
    }

    private void SelectAsPoint(Vector3 point)
    {
        _rayUnitSelector.SelectUnit(point);
    }

    private void SelectAsRect(Vector3 startPoint, Vector3 endPoint)
    {
        _rectUnitSelector.SelectUnits(startPoint, endPoint);
    }

    #region Первый вариант через ECS
    //private readonly ComponentPool<SelectionDataComponent> _inputDataComponent;
    //private readonly ComponentPool<SelectionOutputDataComponent> _outputDataComponent;

    //public void OnUpdate(int entityIndex)
    //{
    //    if (!_outputDataComponent.HasComponent(entityIndex))
    //    {
    //        return;
    //    }

    //    ref var outputComponent = ref _outputDataComponent.GetComponent(entityIndex);
    //    if (outputComponent.IsSelectingOver)
    //    {
    //        ref var inputComponent = ref _inputDataComponent.GetComponent(entityIndex);
    //        SelectAsRect(inputComponent.StartPoint, inputComponent.EndPoint);
    //        outputComponent.IsSelectingOver = false;
    //    }
    //}

    //public void SelectAsRect(Vector3 startPoint, Vector3 endPoint)
    //{
    //    EcsModule.Instance.SelectedUnitsStack.ClearUnits();

    //    Collider2D[] units = Physics2D.OverlapAreaAll(startPoint, endPoint);

    //    foreach (var unit in units)
    //    {
    //        EcsModule.Instance.SelectedUnitsStack.AddUnit(unit.gameObject);
    //    }
    //}
    #endregion
}