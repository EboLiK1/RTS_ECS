using CodeMonkey.Utils;
using Ecs;
using System;

public sealed class RectSelectionInputSystem : IUpdateSystem
{
    #region Первый варик
    //private readonly ComponentPool<SelectionInputDataComponent> _inputComponent;
    //private readonly ComponentPool<SelectionOutputDataComponent> _outputComponent;

    //public void OnUpdate(int entityIndex)
    //{
    //    if(!_inputComponent.HasComponent(entityIndex))
    //    {
    //        return;
    //    }

    //    ref var inputComponent = ref _inputComponent.GetComponent(entityIndex);
    //    ref var outputComponent = ref _outputComponent.GetComponent(entityIndex);

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        inputComponent.IsSelecting = true;
    //        inputComponent.StartPoint = UtilsClass.GetMouseWorldPosition();
    //    }
    //    else if (Input.GetMouseButton(0))
    //    {
    //        inputComponent.EndPoint = UtilsClass.GetMouseWorldPosition();
    //    }
    //    else if (Input.GetMouseButtonUp(0))
    //    {
    //        inputComponent.IsSelecting = false;
    //        inputComponent.EndPoint = UtilsClass.GetMouseWorldPosition();
    //        outputComponent.IsSelectingOver = true;
    //    }
    //}
    #endregion

    public event Action OnStarted;
    public event Action<int> OnFinished;

    private readonly ComponentPool<SelectionDataComponent> _selectionComponent;

    private MouseInput _mouse;

    public RectSelectionInputSystem(MouseInput mouse)
    {
        _mouse = mouse;
    }

    public void OnUpdate(int entityIndex)
    {
        if (!_selectionComponent.HasComponent(entityIndex))
        {
            return;
        }

        ref var selectionComponent = ref _selectionComponent.GetComponent(entityIndex);

        var buttonState = _mouse.LeftButton;
        if (buttonState == MouseInput.ButtonState.DOWN)
        {
            selectionComponent.IsSelecting = true;
            selectionComponent.StartPoint = UtilsClass.GetMouseWorldPosition();
            selectionComponent.EndPoint = selectionComponent.StartPoint;
            OnStarted?.Invoke();
        }
        else if (buttonState == MouseInput.ButtonState.PRESS)
        {
            selectionComponent.EndPoint = UtilsClass.GetMouseWorldPosition();
        }
        else if (buttonState == MouseInput.ButtonState.UP)
        {
            selectionComponent.IsSelecting = false;
            selectionComponent.EndPoint = UtilsClass.GetMouseWorldPosition();
            OnFinished?.Invoke(entityIndex);
        }
    }
}