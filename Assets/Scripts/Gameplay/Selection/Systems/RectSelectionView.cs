using Ecs;
using UnityEngine;

public class RectSelectionView
{
    private Transform _selectionArea;

    public RectSelectionView(Transform selectionArea)
    {
        _selectionArea = selectionArea;
    }

    public void SetPositions(Vector3 lowerLeftPoint, Vector3 upperRightPoint)
    {
        _selectionArea.position = lowerLeftPoint;
        _selectionArea.localScale = upperRightPoint - lowerLeftPoint;
    }

    public void SetVisible(bool isVisible)
    {
        _selectionArea.gameObject.SetActive(isVisible);
    }

    #region Первый вариант через ECS
    //private readonly ComponentPool<SelectionDataComponent> _inputDataComponent;
    //private readonly ComponentPool<SelectionOutputDataComponent> _outputDataComponent;

    //private Transform _selectionArea;

    //public RectSelectionViewSystem(Transform selectionArea)
    //{
    //    _selectionArea = selectionArea;
    //}

    //public void OnUpdate(int entityIndex)
    //{
    //    if (!_inputDataComponent.HasComponent(entityIndex))
    //    {
    //        return;
    //    }

    //    ref var inputDataComponent = ref _inputDataComponent.GetComponent(entityIndex);
    //    if (!inputDataComponent.IsSelecting)
    //    {
    //        SetVisible(false);
    //        return;
    //    }

    //    ref var outputDataComponent = ref _outputDataComponent.GetComponent(entityIndex);

    //    SetPositions(outputDataComponent.LowerLeftPoint, outputDataComponent.UpperLeftPoint);
    //    SetVisible(true);
    //}

    //public void SetPositions(Vector3 lowerLeftPoint, Vector3 upperRightPoint)
    //{
    //    _selectionArea.position = lowerLeftPoint;
    //    _selectionArea.localScale = upperRightPoint - lowerLeftPoint;
    //}

    //public void SetVisible(bool isVisible) =>
    //    _selectionArea.gameObject.SetActive(isVisible);
    #endregion
}