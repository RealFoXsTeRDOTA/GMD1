using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Menu
{
    [RequireComponent(typeof(Button))]
        public class HoverButtonSound: MonoBehaviour, IPointerEnterHandler
        {
            [SerializeField]
            private AudioClip hoverSound;
            private Button button;
            private AudioSource audioSource;

            private void Start()
            {
                button = GetComponent<Button>();
                audioSource = GetComponent<AudioSource>();

                // if audio source is not attached to the button, add one
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }

                // set the audio source to not play on awake, and to play the assigned clip
                audioSource.playOnAwake = false;
                audioSource.clip = hoverSound;
                audioSource.volume = 0.5f;
            }

            public void OnPointerEnter(PointerEventData eventData)
            {
                // if the button is interactable, play the hover sound
                if (button.interactable)
                {
                    audioSource.PlayOneShot(hoverSound);
                }
            }
        }
    }