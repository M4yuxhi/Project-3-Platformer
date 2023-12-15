using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("AudioClips")]
    [SerializeField] protected AudioClip[] audioClips;
    [SerializeField] protected string[] audioNames;

    [Header("Extra Channels")]
    [SerializeField] private AudioSource channel2 = null;
    [SerializeField] private AudioSource channel3 = null;

    private AudioSource audioSource;

    void Start() 
    {
        audioSource = gameObject.GetComponent<AudioSource>();    
    }

    public AudioClip GetAudioClipByName(string name) 
    {
        for (int i = 0; i < audioClips.Length; i++)
            if (name == audioNames[i]) return audioClips[i];

        return null;
    }

    public AudioClip GetCurrentAudioClip(int channel) 
    {
        switch(channel)
        {
            case 1 : return audioSource.clip;
            case 2 : return channel2.clip;
            case 3 : return channel3.clip;
        }

        return null;
    }

    private ref AudioSource GetDesignedChannel(int channel) 
    {
        switch (channel)
        {
            case 1 : return ref audioSource;
            case 2 : return ref channel2;
            case 3 : return ref channel3;
            default:
                // Manejar el caso por defecto, por ejemplo, lanzar una excepción o devolver un valor predeterminado.
                throw new ArgumentException("Can't get designed channel for unknown channel number.", nameof(channel));
        }
    }

    public void PlayAudioClip(string audioName, bool loop, float volume, int channel) 
    {
        AudioClip audio = GetAudioClipByName(audioName);

        ref AudioSource designedChannel = ref GetDesignedChannel(channel);

        if ((designedChannel.isPlaying && designedChannel.clip != audio) || !designedChannel.isPlaying)
        {
            SetVolume(volume, ref designedChannel);

            if (designedChannel.clip != audio)
                designedChannel.clip = audio;

            if (designedChannel.loop != loop)
                designedChannel.loop = loop;

            designedChannel.Play();
        }
    }

    public void SetVolume(float volume, ref AudioSource channel)
    {
        if (volume < 0) volume = 0;
        else if (volume > 1) volume = 1;

        channel.volume = volume;
    }

    public void StopAudioClip(string audioName, int channel)
    {
        if (string.IsNullOrEmpty(audioName))
        {
            throw new System.ArgumentException($"'{nameof(audioName)}' no puede ser nulo ni estar vacío.", nameof(audioName));
        }

        switch (channel)
        {
            case 1 :

                audioSource.loop = false;
                audioSource.pitch = 1;
                audioSource.Stop();

                break;
            case 2:

                channel2.loop = false;
                channel2.pitch = 1;
                channel2.Stop();

                break;
            case 3:

                channel3.loop = false;
                channel3.pitch = 1;
                channel3.Stop();

                break;
            default:
                throw new System.ArgumentException($"Canal invalido.");
        }
    }
}
