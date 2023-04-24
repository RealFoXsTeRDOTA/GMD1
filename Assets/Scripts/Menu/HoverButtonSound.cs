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
      //audioSource = GetComponent<AudioSource>();

      //// set the audio source to not play on awake, and to play the assigned clip
      //audioSource.playOnAwake = false;
      //audioSource.clip = hoverSound;
      //audioSource.volume = 0.5f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      // if the button is interactable, play the hover sound
      if (button.interactable)
      {
        audioManager.Play(hoverSound);
      }
    }
  }
}