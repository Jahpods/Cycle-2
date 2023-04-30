using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	[SerializeField]
	private Sound[] sounds;

	public static AudioManager instance;

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

	void Update(){
		if (Input.GetKey(KeyCode.Asterisk)){
            Application.Quit();
        }
	}

	void Start(){
		Play("Menu");
	}

	public void Play(string name){
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if(s == null){
			Debug.LogWarning("Sound " + name + " was not found");
			return;
		}
		if(name == "Hit" || name == "Damage"){
			s.source.pitch = s.pitch + UnityEngine.Random.Range(-0.3f, 0.3f);
		}
		s.source.Play();
	}
}
