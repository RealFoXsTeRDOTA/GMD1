using UnityEngine;

public class AnimatedCollectible : MonoBehaviour
{
  private float speed = .5f;
  private Vector3 pointA;
  private Vector3 pointB;

  private void Start()
  {
    pointA = transform.position + new Vector3(0, -.1f, 0);
    pointB = transform.position + new Vector3(0, .1f, 0);
  }

  private void Update()
  {
    float time = Mathf.PingPong(Time.time * speed, 1);
    transform.position = Vector3.Lerp(pointA, pointB, time);
  }
}
