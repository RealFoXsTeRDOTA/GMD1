using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  private const string startScene = "Scenes/Tutorial/Level 0";

  public void NewGame()
  {
    GameSaver.SaveData(startScene, 0);
    SceneManager.LoadScene(startScene);
  }
  
  public void Continue()
  {
    var savedData = GameSaver.LoadData();
    SceneManager.LoadScene(savedData.Level);
    
    var gameController = GameObject.FindGameObjectWithTag("GameController")
      .GetComponent<GameController>();
    gameController.GiveHealth(gameController.MaxPlayerHealth);
    gameController.SetScore(savedData.Collectibles);
  }

  public void QuitGame()
  {
    Debug.Log("Quitting Game...");
    Application.Quit();
  }
}
