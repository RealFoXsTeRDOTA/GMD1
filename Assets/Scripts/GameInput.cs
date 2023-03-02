using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
  //public event EventHandler OnAttack;
  //public event EventHandler OnInteract;
  public event EventHandler OnJump;
  //public event EventHandler OnTypeChange;

  private PlayerInputActions playerInputActions;

  private void Awake()
  {
    playerInputActions = new PlayerInputActions();
    playerInputActions.Player.Enable();

    //playerInputActions.Player.Attack.performed += AttackPerformed;
    //playerInputActions.Player.Interact.performed += InteractPerformed;
    playerInputActions.Player.Jump.performed += JumpPerformed;
    //playerInputActions.Player.TypeChange.performed += TypeChangePerformed;
  }

  private void JumpPerformed(InputAction.CallbackContext obj)
  {
    OnJump?.Invoke(this, EventArgs.Empty);
  }

  public Vector2 GetMovementVector()
  {
    var inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
    return inputVector;
  }
}
