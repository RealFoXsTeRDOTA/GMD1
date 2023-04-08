using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField]
  private Transform objectToFollow;
  private Vector3 offset;

  void Start()
  {
    offset = transform.position - objectToFollow.position;
    DontDestroyOnLoad(gameObject);
  }

  void LateUpdate()
  {
    transform.position = objectToFollow.position + offset;
  }
}
