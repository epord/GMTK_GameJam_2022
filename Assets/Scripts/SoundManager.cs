using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip shopMusic;
    public AudioClip battleMusic;
    public AudioClip winSound;

    private AudioSource source;

    private void Start()
    {
        this.source = GetComponent<AudioSource>();
    }
    public void PlayShopMusic()
    {
        StartCoroutine(this.SwitchMusic(source, shopMusic, 0.5f));
    }

    public void PlayWin()
    {
        StartCoroutine(this.PlayWin(source, 0.5f));
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

    public IEnumerator PlayWin(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        audioSource.Stop();
        audioSource.clip = winSound;
        audioSource.Play();

        yield return new WaitForSeconds(0.5f);

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        audioSource.volume = startVolume;
    }
}
