using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour {

	List<AudioSource> musics;
	List<AudioSource> sounds;

	// Use this for initialization
	void Start () {
		musics = new List<AudioSource>();
		sounds = new List<AudioSource>();

		AudioSource[] audioSources = GameObject.FindObjectsOfType<AudioSource>();
		foreach (var audio in audioSources)
		{
			if(audio.CompareTag("Sound")) {
				sounds.Add(audio);
			}else{
				musics.Add(audio);
			}
		}
	}
	
	private bool muteSounds = false; 
	private bool muteMusic = false; 

	public void Mute(bool mute = true) {
		MuteSounds(mute);
		MuteMusic(mute);
	}

	public void MuteSounds(bool mute = true) {
		muteSounds = mute;
		foreach (var audio in sounds)
		{
			audio.mute = mute;
		}
	}

	public void MuteMusic(bool mute = true) {
		muteMusic = mute;
		foreach (var audio in musics)
		{
			audio.mute = mute;
		}
	}

	public void ToggleMusic(Button b){
		MuteMusic(!muteMusic);
		b.GetComponentInChildren<Text>().text = muteMusic? "Poner música" : "Quitar música";
	}

	public void ToggleSounds(Button b) {
		MuteSounds(!muteSounds);
		b.GetComponentInChildren<Text>().text = muteSounds? "Activar sonidos" : "Quitar sonidos";
	}

}
