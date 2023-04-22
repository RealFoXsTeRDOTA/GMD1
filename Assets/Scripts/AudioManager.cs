using UnityEngine;

public class AudioManager : MonoBehaviour
{
  private AudioSource backgroundMusic;
  private AudioSource soundEffects;

  private void Awake()
  {
    var audioSources = GetComponents<AudioSource>();
    backgroundMusic = audioSources[0];
    soundEffects = audioSources[1];
  }

  public void Play(AudioClip audio)
  {
    soundEffects.PlayOneShot(audio);
  }
}
