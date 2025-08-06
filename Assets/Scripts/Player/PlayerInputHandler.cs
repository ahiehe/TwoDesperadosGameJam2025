using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    public static PlayerInputHandler instance;
    private PlayerInputs inputActions;

    public event Action<Vector2> OnMovePerformed;
    public event Action OnJumpPressed;
    public event Action OnAttackPressed;


    private void Awake()
    {
        instance = this;
        inputActions = new PlayerInputs();

        inputActions.Player.Move.performed += ctx => OnMovePerformed?.Invoke(ctx.ReadValue<Vector2>());
        inputActions.Player.Move.canceled += ctx => OnMovePerformed?.Invoke(Vector2.zero);
        inputActions.Player.Jump.performed += ctx => OnJumpPressed?.Invoke();
        inputActions.Player.Attack.performed += ctx => OnAttackPressed?.Invoke();
    }



    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }


}
