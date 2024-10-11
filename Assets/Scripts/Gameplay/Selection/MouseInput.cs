using CodeMonkey.Utils;
using Ecs;
using System;
using UnityEngine;

public sealed class MouseInput : IUpdateSystem
{
    private const int LEFT_BUTTON = 0;
    private const int RIGHT_BUTTON = 1;

    public event Action<Vector2> OnLeftClicked;
    public event Action<Vector2> OnLeftDoubleClicked;
    public event Action<Vector3> OnRightClicked;

    private Vector3 _mousePosition;
    private ButtonState _leftButton;
    private ButtonState _rightButton;

    public Vector3 Position => _mousePosition;
    public ButtonState LeftButton => _leftButton;
    public ButtonState RightButton => _rightButton;

    private float _leftClickedTime;
    private float _rightClickedTime;

    public enum ButtonState
    {
        IDLE,
        DOWN,
        PRESS,
        UP
    }

    public void OnUpdate(int entityIndex)
    {
        _mousePosition = UtilsClass.GetMouseWorldPosition();

        HandleLeftButton();
        HandleRightButton();
    }

    private void HandleLeftButton()
    {
        if(Input.GetMouseButtonDown(LEFT_BUTTON))
        {
            _leftButton = ButtonState.DOWN;
            return;
        }

        if (Input.GetMouseButton(LEFT_BUTTON))
        {
            _leftButton = ButtonState.PRESS;
            return;
        }

        if (Input.GetMouseButtonUp(LEFT_BUTTON))
        {
            _leftButton = ButtonState.UP;
            OnLeftClicked?.Invoke(_mousePosition);
            return;
        }

        _leftButton = ButtonState.IDLE;
    }

    private void HandleRightButton()
    {
        if (Input.GetMouseButtonDown(RIGHT_BUTTON))
        {
            _rightButton = ButtonState.DOWN;
            return;
        }

        if (Input.GetMouseButton(RIGHT_BUTTON))
        {
            _rightButton = ButtonState.PRESS;
            return;
        }

        if (Input.GetMouseButtonUp(RIGHT_BUTTON))
        {
            _rightButton = ButtonState.UP;
            OnRightClicked?.Invoke(_mousePosition);
            return;
        }

        _rightButton = ButtonState.IDLE;
    }
}