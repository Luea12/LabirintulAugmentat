using System;
using UnityEngine.Audio;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Use this for initialization
    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) 
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Play("BGM");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if(s != null)
        {
            s.source.Play();
        }
    }

    public void UpdatePitch(float scale)
    {
        foreach(Sound s in sounds)
        {
            s.source.pitch *= scale;
        }
    }

    public void UpdateVolume(float value)
    {
        foreach(Sound s in sounds)
        {
            s.source.volume = value;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
