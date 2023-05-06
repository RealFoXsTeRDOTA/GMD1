using System.Collections;
using System.Collections.Generic;
using Tiles;
using UnityEngine;

public class MovingTile : MonoBehaviour, IEnterExitTile
{
    [SerializeField]
    private float slideDistance = 5f;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private bool rightFirst = true;

    private Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        float direction = rightFirst ? 1f : -1f;
        float newPosition = Mathf.PingPong(Time.time * speed, slideDistance);
        transform.position = _initialPosition + Vector3.right * (direction * newPosition);
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