using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeBarsIntercomunicator : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Scrollbar masterSlider;
    [SerializeField] private Scrollbar musicSlider;
    [SerializeField] private Scrollbar fxSlider;

    private const string MIXER_MASTER = "MasterVol";
    private const string MIXER_MUSIC = "MusicVol";
    private const string MIXER_FX = "FXVol";

    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        fxSlider.onValueChanged.AddListener(SetFXVolume);

        masterSlider.value = Globals.Sound.VolMaster;
        musicSlider.value = Globals.Sound.VolMusic;
        fxSlider.value = Globals.Sound.VolFX;

        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(Globals.Sound.VolMaster) * 20);
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(Globals.Sound.VolMusic) * 20);
        mixer.SetFloat(MIXER_FX, Mathf.Log10(Globals.Sound.VolFX) * 20);
    }

    void Update()
    {
        if (masterSlider.value < 0) masterSlider.value = 0;
        if (musicSlider.value < 0) musicSlider.value = 0;
        if (fxSlider.value < 0) fxSlider.value = 0;
    }

    private void SetMasterVolume(float value)
    {
        Globals.Sound.VolMaster = value;
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(Globals.Sound.VolMaster) * 20);
    }

    private void SetMusicVolume(float value)
    {
        Globals.Sound.VolMusic = value;
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(Globals.Sound.VolMusic) * 20);
    }

    private void SetFXVolume(float value)
    {
        Globals.Sound.VolFX = value;
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(Globals.Sound.VolMaster) * 20);
    }
}
