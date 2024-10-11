using CodeMonkey.Utils;
using Ecs;
using UnityEngine;

public class RectSelectionViewControllerSystem : IUpdateSystem
{
    private readonly ComponentPool<SelectionDataComponent> _inputDataComponent;

    private RectSelectionView _rectSelectionView;

    public RectSelectionViewControllerSystem(RectSelectionView rectSelectionView)
    {
        _rectSelectionView = rectSelectionView;
    }

    public void OnUpdate(int entityIndex)
    {
        if (!_inputDataComponent.HasComponent(entityIndex))
        {
            return;
        }

        ref var inputComponent = ref _inputDataComponent.GetComponent(entityIndex);

        if (inputComponent.IsSelecting)
        {
            Vector3 currentMousePosition = UtilsClass.GetMouseWorldPosition();

            Vector3 lowerLeftPoint = new Vector3(Mathf.Min(inputComponent.StartPoint.x, currentMousePosition.x),
                                                 Mathf.Min(inputComponent.StartPoint.y, currentMousePosition.y));
            Vector3 upperLeftPoint = new Vector3(Mathf.Max(inputComponent.StartPoint.x, currentMousePosition.x),
                                                 Mathf.Max(inputComponent.StartPoint.y, currentMousePosition.y));

            _rectSelectionView.SetPositions(lowerLeftPoint, upperLeftPoint);
            _rectSelectionView.SetVisible(true);
        }
        else
        {
            _rectSelectionView.SetVisible(false);
        }
    }
}