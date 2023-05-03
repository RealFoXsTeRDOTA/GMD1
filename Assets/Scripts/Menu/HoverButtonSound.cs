using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu
{
  [RequireComponent(typeof(Button))]
  public class HoverButtonSound : MonoBehaviour, IPointerEnterHandler
  {
    [SerializeField]
    private AudioClip hoverSound;
    private Button button;
    private AudioManager audioManager;

    private void Start()
    {
      button = GetComponent<Button>();
      audioManager = FindFirstObjectByType<AudioManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if (button.interactable)
      {
        audioManager.Play(hoverSound);
      }
    }
  }
}