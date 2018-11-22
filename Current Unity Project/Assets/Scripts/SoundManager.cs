using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource interactSound;
	public AudioSource interactSound2;
	public AudioSource interactSound3;
	public AudioSource interactSound4;
	public AudioSource music;
	public AudioSource alt2;
	public AudioSource altSound;
	public AudioSource altSound3;
	public AudioSource altSound4;
	public AudioSource altSound5;
	public AudioSource elevator;
	public static SoundManager soundManager = null;


	public float glowstickVolume = 0.07f;
	public float otherVolume = 0.1f;
	public float scaryObjVolume = 0.04f;
	public float elevatorSound = 0.2f;



	float lowPitchRange = .75f;
	float highPitchRange = 1.1f;

	void Awake()
	{
		// if there is no sound manager, make one
		if (soundManager == null) {
			soundManager = this;
		}  else if (soundManager != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	// play multiple sounds at once over each other
	public void playSingle(AudioClip AudioClip)
	{
		if (interactSound.isPlaying) {
			if (interactSound2.isPlaying) {
				if (interactSound3.isPlaying) {
					interactSound4.clip = AudioClip;
					if (AudioClip.name == "brokenFan") {
						interactSound4.volume = scaryObjVolume + 0.08f;
					} else {
						interactSound4.volume = scaryObjVolume;
					}
					interactSound4.Play ();
				} else {
					interactSound3.clip = AudioClip;
					if (AudioClip.name == "brokenFan") {
						interactSound3.volume = scaryObjVolume + 0.08f;
					} else {
						interactSound3.volume = scaryObjVolume;
					}
					interactSound3.Play ();
				}
			} else {
				interactSound2.clip = AudioClip;
				if (AudioClip.name == "brokenFan") {
					interactSound2.volume = scaryObjVolume + 0.08f;
				} else {
					interactSound2.volume = scaryObjVolume;
				}
				interactSound2.Play ();
			}
		} else {
			interactSound.clip = AudioClip;
			if (AudioClip.name == "brokenFan") {
				interactSound.volume = scaryObjVolume + 0.08f;
			} else {
				interactSound.volume = scaryObjVolume;
			}
			interactSound.Play ();
		}
	}

	public void playElevator(AudioClip audioClip)
	{
		if (interactSound.isPlaying) {
			if (interactSound2.isPlaying) {
				if (interactSound3.isPlaying) {
					interactSound4.clip = audioClip;
					interactSound4.volume = elevatorSound;
					interactSound4.Play ();
				} else {
					interactSound3.clip = audioClip;
					interactSound3.volume = elevatorSound;
					interactSound3.Play ();
				}
			} else {
				interactSound2.clip = audioClip;
				interactSound2.volume = elevatorSound;
				interactSound2.Play ();
			}
		} else {
			interactSound.clip = audioClip;
			interactSound.volume = elevatorSound;
			interactSound.Play ();
		}
	}

	// play a single clip
	public void playSingleSound(AudioClip AudioClip)
	{
		if (!altSound.isPlaying) {
			altSound.clip = AudioClip;
			if (AudioClip.name == "glowstickSound") {
				altSound.volume = glowstickVolume;
			}
			else
			{
				altSound.volume = otherVolume;
			}
			altSound.Play ();
		} else if (!altSound3.isPlaying) {
			altSound3.clip = AudioClip;
			if (AudioClip.name == "glowstickSound") {
				altSound3.volume = glowstickVolume;
			} else {
				altSound3.volume = otherVolume;
			}
			altSound3.Play ();
		} else if (!altSound4.isPlaying) {
			altSound4.clip = AudioClip;
			if (AudioClip.name == "glowstickSound") {
				altSound4.volume = glowstickVolume;
			} else {
				altSound4.volume = otherVolume;
			}
			altSound4.Play ();
		} else if (!altSound5.isPlaying) {
			altSound5.clip = AudioClip;
			if (AudioClip.name == "glowstickSound") {
				altSound5.volume = glowstickVolume;
			} else {
				altSound5.volume = otherVolume;
			}
			altSound5.Play ();
		}
	}

	public void playAlt2(AudioClip AudioClip)
	{
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);
		alt2.clip = AudioClip;
		alt2.pitch = randomPitch;
		alt2.Play ();
	}

	// change the music clip
	public void changeMusic(AudioClip musicSwitch)
	{
		music.clip = musicSwitch;
		music.Play ();
	}

	// stop various single clips
	public void stopSingle(AudioClip AudioClip)
	{
		if (string.Compare(interactSound.clip.name,AudioClip.name) == 0) {
			interactSound.Stop ();
		} else if (string.Compare(interactSound2.clip.name,AudioClip.name) == 0) {
			interactSound2.Stop ();
		}else if (string.Compare(interactSound3.clip.name,AudioClip.name) == 0) {
			interactSound3.Stop ();
		}else if (string.Compare(interactSound4.clip.name,AudioClip.name) == 0) {
			interactSound4.Stop ();
		}
	}

	/*public void randomizeSound(params AudioClip [] clips)
	{
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		interactSound.pitch = randomPitch;

		interactSound.clip = clips [randomIndex];
		interactSound.Play ();
	}*/

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
