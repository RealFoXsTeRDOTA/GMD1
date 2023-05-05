using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI collectiblesText;

  private void Awake()
  {
    var score = FindFirstObjectByType<GameController>().Score;
    collectiblesText.text = score.ToString();
  }

  public void ReturnToMainMenu()
  {
    SceneManager.LoadSceneAsync(0);
  }
}
