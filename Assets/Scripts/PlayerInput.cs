using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions _actMap;

    public event Action<bool> OnInputAttack;
    public event Action<Vector2> OnInputMove;

    public Vector2 MousePosition { get; private set; }

    private void Awake()
    {
        _actMap = new InputSystem_Actions();
        _actMap.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        _actMap.Enable();
    }

    private void OnDisable()
    {
        _actMap.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnInputAttack?.Invoke(true);
        else if (context.canceled)
            OnInputAttack?.Invoke(false);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnInputMove?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }
}
