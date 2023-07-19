using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public Sound[] sounds;

    private AudioSource bgmSource;
    public Sound[] bgmSounds;

    private float bgmVolume = 1.0f; // 배경음 볼륨 설정 변수
    private float sfxVolume = 1.0f; // 효과음 볼륨 설정 변수

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        // 각 사운드에 AudioSource 컴포넌트 추가 및 초기화
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
        }

        bgmSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        Sound sound = GetSound(name);
        if (sound != null)
        {
            sound.source.volume = sfxVolume;
            sound.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound with name " + name + " not found!");
        }
    }

    public void StopSound(string name)
    {
        Sound sound = GetSound(name);
        if (sound != null)
        {
            sound.source.Stop();
        }
        else
        {
            Debug.LogWarning("Sound with name " + name + " not found!");
        }
    }

    public void PlayBGM(string name)
    {
        Sound sound = GetBGMSound(name);
        if (sound != null)
        {
            bgmSource.Stop();
            bgmSource.volume = bgmVolume;
            bgmSource.clip = sound.clip;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning("Sound with name " + name + " not found!");
        }
    }

    private Sound GetSound(string name)
    {
        Sound sound = System.Array.Find(sounds, s => s.name == name);
        return sound;
    }

    private Sound GetBGMSound(string name)
    {
        Sound sound = System.Array.Find(bgmSounds, s => s.name == name);
        return sound;
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        bgmSource.volume = bgmVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
    }
}
