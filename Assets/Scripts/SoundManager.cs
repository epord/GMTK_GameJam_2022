using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip shopMusic;
    public AudioClip battleMusic;

    private AudioSource source;

    private void Start()
    {
        this.source = GetComponent<AudioSource>();
    }
    public void PlayShopMusic()
    {
        StartCoroutine(this.SwitchMusic(source, shopMusic, 1f));
    }
    public void PlayBattleMusic()
    {
        StartCoroutine(this.SwitchMusic(source, battleMusic, 1f));
    }

    public IEnumerator SwitchMusic(AudioSource audioSource, AudioClip nextClip, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = nextClip;
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        audioSource.volume = startVolume;
    }
}
