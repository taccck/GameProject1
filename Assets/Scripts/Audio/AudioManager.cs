using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Curr;

    public Sound[] sounds;
    
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    
        [Range(0f,1f)] public float volume;
        [Range(.1f,3f)] public float pitch;
        public bool loop;

        [HideInInspector] public AudioSource source;
    }


    private void Awake()
    {
        if (Curr == null)
            Curr = this;
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

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string inputName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == inputName);
        if (s == null)
        {
            Debug.LogError($"You made a typo stupid : {inputName}");
            return;
        }
        s.source.Play();
        
    }
}