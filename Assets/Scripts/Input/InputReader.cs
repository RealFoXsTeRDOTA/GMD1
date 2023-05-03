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
  public event Action<Vector2> MoveEvent;
  public event Action AttackEvent;
  public event Action JumpEvent;
  public event Action PauseEvent;
  public event Action ResumeEvent;
  public event Action DashEvent;
  public event Action RangedAttackEvent;

  private void Awake()
  {
    gameInput = new GameInput();
    gameInput.Gameplay.SetCallbacks(this);
    gameInput.UI.SetCallbacks(this);

    pauseMenu = FindFirstObjectByType<PauseMenu>();
    gameController = FindFirstObjectByType<GameController>();
    pauseMenu.ResumeClickedEvent += StartGameplay;
    gameController.PlayerDeathEvent += PauseGameplay;
    gameController.PlayerRespawnEvent += StartGameplay;

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
    gameController.PlayerRespawnEvent -= StartGameplay;
  }
}
