using System.Collections;
using System.Collections.Generic;
using Tiles;
using UnityEngine;

public class MovingTile : MonoBehaviour, IEnterExitTile
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float moveDistance = 5f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float newPosition = Mathf.PingPong(Time.time * speed, moveDistance);
        transform.position = startPosition + Vector3.right * newPosition;
    }
    
    public void OnEnter(PlayerController playerController)
    {
        playerController.transform.SetParent(transform);
    }

    public void OnExit(PlayerController playerController)
    {
        playerController.transform.SetParent(null);
    }
}
