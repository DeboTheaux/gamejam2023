using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup sfxMixerGroup;
    [SerializeField] private string sfxMixerVolumeName = "SFX_Volume";
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private string musicMixerVolumeName = "Music_Volume";
    [Space]
    [SerializeField] private Sound[] effects;
    [SerializeField] private Sound[] musics;

    private AudioSource musicSource;
    private ISoundClip currentMusicClip;
    private List<AudioSource> pooledSources = new List<AudioSource>();


    public static float LinearToDB(float linearValue) => (linearValue > float.Epsilon) ? Mathf.Log10(linearValue) * 20 : -80f;
    public static float DBToLinear(float dbValue) => (dbValue > -80f) ? Mathf.Pow(10f, dbValue / 20f) : 0f;


    private static void PlayClip(AudioSource source, ISoundClip sound, System.Func<float> rng)
    {
        float randomPitch = (rng() - 0.5f) * 2f;
        randomPitch *= sound.RandomPitch;
        source.pitch = sound.Pitch + randomPitch;

        source.loop = sound.Loop;
        source.clip = sound.Clip;
        source.volume = sound.Volume;
        source.Play();
    }

    private static float RandomValue() => Random.value;


    void Awake()
    {
        musicSource = CreateSource("Music_Source", musicMixerGroup);

        foreach (Sound music in musics)
            music.SetSource(CreateSource($"Music_{music.soundId}", musicMixerGroup));

        foreach (Sound effect in effects)
            effect.SetSource(CreateSource($"SFX_{effect.soundId}", sfxMixerGroup));
    }

    private AudioSource GetPooledSoundSource()
    {
        foreach (var source in pooledSources)
        {
            if (!source.isPlaying)
                return source;
        }

        var newSource = CreateSource($"Sound_Source_{pooledSources.Count}", sfxMixerGroup);
        pooledSources.Add(newSource);
        return newSource;
    }

    private AudioSource CreateSource(string name, AudioMixerGroup audioMixerGroup)
    {
        var go = new GameObject(name);
        go.transform.parent = transform;
        var source = go.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = audioMixerGroup;
        return source;
    }


    public void PlayMusic(ISoundClip sound)
    {
        if (musicSource.isPlaying && currentMusicClip == sound)
            return;

        StopMusic();
        PlayClip(musicSource, sound, RandomValue);
        currentMusicClip = sound;
    }

    public void PlaySound(ISoundClip sound)
    {
        var source = GetPooledSoundSource();
        PlayClip(source, sound, RandomValue);
    }


    public void StopMusic()
    {
        currentMusicClip = null;
        musicSource.Stop();
        foreach (var music in musics)
            music.Stop();
    }


    public void PlayMusic(string soundId)
    {
        var sound = FindMusic(soundId);
        if (sound.IsPlaying())
            return;

        StopMusic();
        PlaySound(soundId, false);
    }

    public void PlaySound(string soundId, bool canPlayAgain = true, bool playOneShoot = false)
    {
        var sound = FindSound(soundId);
        if (sound != null)
        {
            sound.Play(canPlayAgain, playOneShoot);
        }
        else
        {
            Debug.LogWarning($"Sound {soundId} not found");
        }
    }

    public void SetEffectsVolume(float volume)
    {
        DoAtNextFrame(() =>
        {
            if (sfxMixerGroup == null)
            {
                foreach (var sound in effects)
                    sound.SetVolume(volume);
            }
            else sfxMixerGroup.audioMixer.SetFloat(sfxMixerVolumeName, LinearToDB(volume));
        });
    }

    public void ResetEffectsVolume()
    {
        DoAtNextFrame(() =>
        {
            if (sfxMixerGroup == null)
            {
                foreach (var sound in effects)
                    sound.ResetVolume();
            }
            else sfxMixerGroup.audioMixer.SetFloat(sfxMixerVolumeName, 0f);
        });
    }

    public void SetMusicVolume(float volume)
    {
        DoAtNextFrame(() =>
        {
            if (musicMixerGroup == null)
            {
                foreach (var music in musics)
                    music.SetVolume(volume);
            }
            else musicMixerGroup.audioMixer.SetFloat(musicMixerVolumeName, LinearToDB(volume));
        });
    }

    public void ResetMusicVolume()
    {
        DoAtNextFrame(() =>
        {
            if (musicMixerGroup == null)
            {
                foreach (var music in musics)
                    music.ResetVolume();
            }
            else musicMixerGroup.audioMixer.SetFloat(musicMixerVolumeName, 0f);
        });
    }

    private Coroutine DoAtNextFrame(System.Action action)
    {
        return StartCoroutine(Routine());
        System.Collections.IEnumerator Routine()
        {
            yield return null;
            action();
        }
    }


    public void StopSound(string soundId)
    {
        Sound sound = FindSound(soundId);
        if (sound != null)
        {
            sound.Stop();
        }
        else
        {
            Debug.LogWarning($"Sound {soundId} not found");
        }
    }

    public void StopAllSounds()
    {
        StopMusic();
        foreach (var sound in GetAllSounds())
            sound.Stop();
    }

    private IEnumerable<Sound> GetAllSounds() =>
        effects.Concat(musics);

    private Sound FindMusic(string soundId) =>
        musics.FirstOrDefault(sound => sound.soundId.Equals(soundId));

    private Sound FindSound(string soundId)
    {
        return GetAllSounds()
            .FirstOrDefault(sound => sound.soundId.Equals(soundId));
    }
}


[System.Serializable]
public class Sound
{
    public string soundId;
    public AudioClip audioClip;
    public bool loop;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomPitch = 0f;

    private AudioSource audioSource;

    public void SetSource(AudioSource source)
    {
        audioSource = source;
        audioSource.loop = loop;
        audioSource.clip = audioClip;
        audioSource.playOnAwake = false;
    }

    public void Play(bool canPlayAgain, bool playOneShoot)
    {
        if (!canPlayAgain && IsPlaying())
            return;

        audioSource.volume = volume;
        audioSource.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));

        if (playOneShoot)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
        else
        {
            audioSource.Play();
        }
    }

    public void Stop()
    {
        if (IsPlaying())
        {
            audioSource.Stop();
        }
    }

    public void SetVolume(float newVolume)
    {
        audioSource.volume = newVolume;
    }

    public void ResetVolume()
    {
        audioSource.volume = volume;
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}

public interface ISoundClip
{
    AudioClip Clip { get; }
    bool Loop { get; }

    float Volume { get; }

    float Pitch { get; }
    float RandomPitch { get; }
}
