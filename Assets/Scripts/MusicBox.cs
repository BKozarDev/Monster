using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    public static MusicBox instance;

    [Range(0, 1)]
    public float maxMusicVolume;
    [Range(0, 1)]
    public float maxEffectVolume;
    public bool musicOn = true;
    public bool soundsOn = true;
    public AudioClip[] ambients;
    public AudioClip[] effects;
    public AudioSource current;
    public AudioSource last;
    public AudioSource effectSource;
    private int currentAmbient;

    public void ToggleMusic(bool musicOn)
    {
        if (musicOn)
        {
            current.volume = maxMusicVolume;
        }
        else
        {
            last.Stop();
            last.volume = 0;
            current.volume = 0;
        }
        this.musicOn = musicOn;
    }

    public void ToggleSounds(bool soundsOn)
    {
        if (soundsOn)
            effectSource.volume = maxEffectVolume;
        else
            effectSource.volume = 0;
        this.soundsOn = soundsOn;
    }

    void Start()
    {
        instance = this;
        current.clip = ambients[0];
        current.Play();
        PlayEffects();
    }
    
    private void Update()
    {
        current.volume = maxMusicVolume;
        effectSource.volume = maxEffectVolume;
        if (last.volume <= 0.1f)
            last.Stop();
    }

    private void PlayEffects()
    {
        int q = 0;
        if (!effectSource.isPlaying)
        {
            effectSource.clip = effects[q];
            q++;
            effectSource.Play();
        }
        if(q >= effects.Length)
        {
            q = 0;
        }
    }

    public void PlayEffect(int effect)
    {
        effectSource.PlayOneShot(effects[effect]);
    }

    public void PlayEffect(AudioClip effect)
    {
        effectSource.PlayOneShot(effect);
    }

    public void ChangeMusic()
    {
        StopAllCoroutines();

        currentAmbient++;
        if (currentAmbient >= ambients.Length)
        {
            currentAmbient = 0;
        }

        last.clip = ambients[currentAmbient];
        last.Play();

        AudioSource source = current;
        current = last;
        last = source;
        if (musicOn)
            StartCoroutine("Fade");
    }

    public void ChangeMusic(int ambient)
    {
        if (ambient == currentAmbient)
            return;
        if (ambient >= ambients.Length)
            return;

        StopAllCoroutines();

        currentAmbient = ambient;

        last.clip = ambients[currentAmbient];
        last.Play();

        AudioSource source = current;
        current = last;
        last = source;
        if (musicOn)
            StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(0.3f);

        for (float i = 0f; i <= 1; i += 0.02f)
        {
            if (!musicOn)
            {
                last.Stop();
                last.volume = 0;
                current.volume = 0;
                yield break;
            }
            current.volume = i;
            if (last != null)
                last.volume = 1 - i;
            yield return null;
        }

        last.Stop();
    }
}
