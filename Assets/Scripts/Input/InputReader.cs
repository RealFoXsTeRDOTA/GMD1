using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameInput;
using static UnityEngine.InputSystem.InputAction;

public class InputReader : MonoBehaviour, IGameplayActions, IUIActions
{
  private GameInput gameInput;
  private PauseMenu pauseMenu;
  private GameController gameController;

  public Action<Vector2> MoveEvent;
  public Action AttackEvent;
  public Action JumpEvent;
  public Action PauseEvent;
  public Action ResumeEvent;
  public Action DashEvent;
  public Action RangedAttackEvent;

  private void Awake()
  {
    gameInput = new GameInput();
    gameInput.Gameplay.SetCallbacks(this);
    gameInput.UI.SetCallbacks(this);

    gameController = GameObject.FindGameObjectWithTag("GameController")
                               .GetComponent<GameController>();
    pauseMenu = FindFirstObjectByType<PauseMenu>();

    pauseMenu.ResumeClickedEvent += StartGameplay;
    gameController.PlayerDeathEvent += PauseGameplay;

    StartGameplay();
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

  public void OnAttack(CallbackContext context)
  {
    if (context.action.phase == InputActionPhase.Performed)
    {
      AttackEvent?.Invoke();
    }
  }

  public void OnJump(CallbackContext context)
  {
    if (context.action.phase == InputActionPhase.Performed)
    {
      JumpEvent?.Invoke();
    }
  }

  public void OnMove(CallbackContext context)
  {
    MoveEvent?.Invoke(context.ReadValue<Vector2>());
  }

  public void OnPause(CallbackContext context)
  {
    if (context.action.phase == InputActionPhase.Performed)
    {
      PauseEvent?.Invoke();
      PauseGameplay();
    }
  }

  public void OnResume(CallbackContext context)
  {
    if (context.action.phase == InputActionPhase.Performed)
    {
      ResumeEvent?.Invoke();
      StartGameplay();
    }
  }

  public void OnDash(CallbackContext context)
  {
    if (context.action.phase == InputActionPhase.Performed)
    {
      DashEvent?.Invoke();
    }
  }

  public void OnRangedAttack(CallbackContext context)
  {
    if (context.action.phase == InputActionPhase.Performed)
    {
      RangedAttackEvent?.Invoke();
    }
  }

  private void OnDestroy()
  {
    pauseMenu.ResumeClickedEvent -= StartGameplay;
    gameController.PlayerDeathEvent -= PauseGameplay;
  }
}
