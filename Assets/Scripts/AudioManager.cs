using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    [Range(0f, 1f)]
    public float volume = .6f;
    [Range(.1f, 3f)]
    public float pitch = 1f;

    void Awake() {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = this.volume;
            s.source.pitch = this.pitch;
        }
    }
    void Start() {
        PlayLoop("BGM");
    }
    // Start is called before the first frame update
    public void Play (string name) {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s != null) {
            s.source.Play();
        }
    }
    public void PlayLoop (string name) {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.loop = true;
        if (s != null) {
            s.source.Play();
        }
    }

    public void SetVolumeBGM(float volume) {
        Sound bgm = Array.Find(sounds, sounds => sounds.name == "BGM");
        bgm.source.volume = volume;
    }

    public void SetVolumeSE(float volume) {
        Sound se = Array.Find(sounds, sounds => sounds.name == "SE");
        se.source.volume = volume;
    }
}
