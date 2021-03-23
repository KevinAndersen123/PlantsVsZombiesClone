using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
//Sound variable that will be used in the sound manager
public class Sound {

	public string name; //name of the sound

	public AudioClip clip; //the audio clip of the sound

	[Range(0f, 1f)] //range of the volume
	public float volume = .75f; //volume parameter that can be changed in the inspector
	[Range(0f, 1f)]
	public float volumeVariance = .1f; //volume variance parameter that can be changed in the inspector

    [Range(.1f, 3f)] //range of the pitch
	public float pitch = 1f; //pitch of the sound effect, this can also be changed in the inspector
	[Range(0f, 1f)]
	public float pitchVariance = .1f;

	public bool loop = false; //sets loop to false as its a sound effect rather than a background music

	public AudioMixerGroup mixerGroup;

	[HideInInspector]
	public AudioSource source; 

}
