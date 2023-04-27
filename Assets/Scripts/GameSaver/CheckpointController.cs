
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    [SerializeField]
    private AudioClip saveSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        var gameController = GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<GameController>();
        gameController.GiveHealth(gameController.MaxPlayerHealth);
        GameSaver.SaveData(SceneManager.GetActiveScene().name, gameController.Score);
        FindFirstObjectByType<AudioManager>().Play(saveSound);
    }
}
