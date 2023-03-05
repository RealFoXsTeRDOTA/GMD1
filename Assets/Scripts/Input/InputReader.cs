using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions
{
  private GameInput gameInput;

  public Action<Vector2> MoveEvent;
  public Action AttackEvent;
  public Action DescendEvent;
  public Action JumpEvent;
  public Action PauseEvent;
  public Action ResumeEvent;

  private void OnEnable()
  {
    if (gameInput == null)
    {
      gameInput = new GameInput();
      gameInput.Gameplay.SetCallbacks(this);
      gameInput.UI.SetCallbacks(this);

      StartGameplay();
    }
  }

  private void StartGameplay()
  {
    gameInput.Gameplay.Enable();
    gameInput.UI.Disable();
  }

  private void PauseGameplay()
  {
    gameInput.Gameplay.Disable();
    gameInput.UI.Enable();
  }

  public void OnAttack(InputAction.CallbackContext context)
  {
    if (context.action.phase == InputActionPhase.Performed)
    {
      AttackEvent?.Invoke();
    }
  }

  public void OnDescend(InputAction.CallbackContext context)
  {
    if (context.action.phase == InputActionPhase.Performed)
    {
      DescendEvent?.Invoke();
    }
  }

  public void OnJump(InputAction.CallbackContext context)
  {
    if (context.action.phase == InputActionPhase.Performed)
    {
      JumpEvent?.Invoke();
    }
  }

  public void OnMove(InputAction.CallbackContext context)
  {
    MoveEvent?.Invoke(context.ReadValue<Vector2>());
  }

  public void OnPause(InputAction.CallbackContext context)
  {
    if (context.action.phase == InputActionPhase.Performed)
    {
      PauseEvent?.Invoke();
      PauseGameplay();
    }
  }

  public void OnResume(InputAction.CallbackContext context)
  {
    if (context.action.phase == InputActionPhase.Performed)
    {
      ResumeEvent?.Invoke();
      StartGameplay();
    }
  }
}
