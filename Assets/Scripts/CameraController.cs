using UnityEngine;

public class CameraController : MonoBehaviour
{
  private GameObject objectToFollow;
  private Vector3 offset;

  void Start()
  {
    objectToFollow = GameObject.FindGameObjectWithTag("Player");
    offset = Vector3.back * 10;
  }

  void LateUpdate()
  {
    transform.position = objectToFollow.transform.position + offset;
  }
}
