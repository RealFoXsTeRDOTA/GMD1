using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI collectiblesText;
  private int maxScore = 39;

  private void Awake()
  {
    var score = FindFirstObjectByType<GameController>().Score;
    collectiblesText.text = $"{score}/{maxScore}";
  }

  public void ReturnToMainMenu()
  {
    SceneManager.LoadSceneAsync(0);
  }
}
