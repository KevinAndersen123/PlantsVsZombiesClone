using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	//public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	void Awake()
	{
        //makes sure there is only one audio manager
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

        //makes a new sound object and sets its varables
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = s.mixerGroup;
		}
	}
	void Start()
    {
		Play("MainBG");
		Play("Crow");
		StartCoroutine(Crow());
    }

    /// <summary>
    /// plays the sound based on the string that is passed in, it searches for the sound name with the same name as the string
    /// </summary>
    /// <param name="sound"></param> name of the sound
	public void Play(string sound)
	{
		
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));

		s.source.Play();
	}
	public void Stop(string sound)
    {
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.Stop();
	}

	public IEnumerator Crow()
    {
		yield return new WaitForSeconds(15.0f);
		if (Data.m_currentScene == "MainMenu")
		{
			Play("Crow");
			StartCoroutine(Crow());
		}
    }
}
