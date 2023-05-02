using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private static AudioManager instance;
	private AudioSource soundEffects;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			instance = this;
			var audioSources = GetComponents<AudioSource>();
			soundEffects = audioSources[1];
		}

		DontDestroyOnLoad(gameObject);
	}

	public void Play(AudioClip audio)
	{
		soundEffects.PlayOneShot(audio);
	}
}
