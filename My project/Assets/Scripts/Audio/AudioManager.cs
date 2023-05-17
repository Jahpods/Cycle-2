using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	[SerializeField]
	private Sound[] sounds;

	public static AudioManager instance;
	private float globalVolume = 1f;

	void Awake(){
		Cursor.visible = false;
		if(instance == null){
			instance = this;
		}else{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);

		
		foreach(Sound s in sounds){
			s.source = gameObject.AddComponent<AudioSource>();

			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	public float GetGlobalVolume(){
		return globalVolume;
	}

	public void SetGlobalVolume(float value){
		globalVolume = value;
		foreach(Sound s in sounds){
			s.source.volume = s.volume * value;
		}		
	}

	void Update(){
		if (Input.GetKey(KeyCode.Asterisk)){
            Application.Quit();
        }
	}

	void Start(){
		foreach(Sound s in sounds){
			Pause(s.name);
		}
		Play("Menu");
		if(SceneManager.GetActiveScene().name == "FirstLevel"){
			//Play("background", 0.04f);
		}
	}

	public void Play(string name, float vol = -1, float pit = -1f){
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if(s == null){
			Debug.LogWarning("Sound " + name + " was not found");
			return;
		}
		if(s.source.isPlaying == true) return;
		if(vol == -1) vol = s.volume;
		if(pit == -1) pit = s.pitch;
		s.source.volume = vol * globalVolume;
		s.source.pitch = pit;
		s.source.Play();
	}

	public void Pause(string name){
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if(s == null){
			Debug.LogWarning("Sound " + name + " was not found");
			return;
		}
		s.source.Pause();
	}
}
