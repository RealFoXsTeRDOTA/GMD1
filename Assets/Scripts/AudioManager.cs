using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private static AudioManager instance;
	private AudioSource backgroundMusic;
	private AudioSource soundEffects;
	[SerializeField]
	private AudioClip backgroundAudio;
	[SerializeField]
	private AudioClip bossAudio;

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
			backgroundMusic = audioSources[0];
			soundEffects = audioSources[1];
		}

		DontDestroyOnLoad(gameObject);
		backgroundMusic.clip = backgroundAudio;
		backgroundMusic.Play();
	}

	public void Play(AudioClip audio)
	{
		soundEffects.PlayOneShot(audio);
	}

	public void ToggleBossMusic()
	{
		if (backgroundMusic.clip == backgroundAudio)
		{
			backgroundMusic.clip = bossAudio;
		}
		else
		{
			backgroundMusic.clip = backgroundAudio;
		}

		backgroundMusic.Play();
	}
}
