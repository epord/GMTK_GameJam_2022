using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioClip plantDying;
    public AudioClip rollDice;
    public AudioClip win;
    public AudioClip loose;
    public AudioClip movePlant;
    public AudioClip placePlant;
    public AudioClip mutation;

    private AudioSource source;
    private void Start()
    {
        this.source = GetComponent<AudioSource>();
    }

    public void PlayPlantDying()
    {
        this.source.Stop();
        this.source.clip = plantDying;
        this.source.Play(); 
    }

    public void PlayRollDice()
    {
        this.source.Stop();
        this.source.clip = rollDice;
        this.source.Play();
    }

    public void PlayWin()
    {
        this.source.Stop();
        this.source.clip = win;
        this.source.Play();
    }

    public void PlayLoose()
    {
        this.source.Stop();
        this.source.clip = loose;
        this.source.Play();
    }

    public void PlayMovePlant()
    {
        this.source.Stop();
        this.source.clip = movePlant;
        this.source.Play();
    }

    public void PlayPlacePlant()
    {
        this.source.Stop();
        this.source.clip = placePlant;
        this.source.Play();
    }

    public void PlayMutation()
    {
        this.source.Stop();
        this.source.clip = mutation;
        this.source.Play();
    }
}
