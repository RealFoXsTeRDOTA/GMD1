using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  [SerializeField]
  private Transform[] spawnLocations;

  void Start()
  {
    var player = GameObject.FindGameObjectWithTag("Player");
    var gameController = GameObject.FindGameObjectWithTag("GameController")
                                   .GetComponent<GameController>();
    player.transform.position = spawnLocations[gameController.SpawnPosition].position;
  }
}
