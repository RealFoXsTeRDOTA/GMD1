using UnityEngine;
using UnityEngine.UI;

public class HealthUserInterface : MonoBehaviour
{
  private Image[] images;
  private GameController gameController;

  private void Awake()
  {
    images = GetComponentsInChildren<Image>();
    gameController = GameObject.FindGameObjectWithTag("GameController")
                               .GetComponent<GameController>();
    UpdateHealth(gameController.CurrentPlayerHealth);
    gameController.HealthChangedEvent += UpdateHealth;
  }

  private void UpdateHealth(int currentHealth)
  {
    for (var i = 0; i < gameController.MaxPlayerHealth; i++)
    {
      images[i].enabled = i < currentHealth;
    }
  }

  private void OnDestroy()
  {
    gameController.HealthChangedEvent -= UpdateHealth;
  }
}
