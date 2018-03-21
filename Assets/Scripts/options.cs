using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class options : MonoBehaviour {
    public GameObject PanelMenu;
    public GameObject PanelSettings;
    private soundController SoundController;
    public Toggle OnOffMusic, OnOffEffect;
    public Slider VolumeM, VolumeE;

    void Start()
    {
        SoundController = FindObjectOfType<soundController>();

        LoadPreferences();

        PanelMenu.SetActive(true);
        PanelSettings.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
		
	}


    public void OpenSettings()
    {
        SoundController.PlayButtonSound();
        if (PanelSettings.activeSelf)
        {
            PanelSettings.SetActive(false);
            PanelMenu.SetActive(true);
        }
        else
        {
            PanelSettings.SetActive(true);
            PanelMenu.SetActive(false);
        }
    }

    public void ClearProgress()
    {
        int MusicPlaying = PlayerPrefs.GetInt("MusicPlaying");
        int FXPlaying = PlayerPrefs.GetInt("FXPlaying");
        float VolumeMusic = PlayerPrefs.GetFloat("VolumeMusic");
        float VolumeEffect = PlayerPrefs.GetFloat("VolumeEffect");

        SoundController.PlayButtonSound();
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("DefaultValues", 1);
        PlayerPrefs.SetInt("MusicPlaying", MusicPlaying);
        PlayerPrefs.SetInt("FXPlaying", FXPlaying);
        PlayerPrefs.SetFloat("VolumeMusic", VolumeMusic);
        PlayerPrefs.SetFloat("VolumeEffect", VolumeEffect);
    }

    public void MuteMusic() {
        SoundController.AudioSourceMusic.mute = !OnOffMusic.isOn;

        PlayerPrefs.SetInt("MusicPlaying", (OnOffMusic.isOn) ? 1 : 0);
    }

    public void MuteEffect()
    {
        SoundController.AudioSourceFX.mute = !OnOffEffect.isOn;

        PlayerPrefs.SetInt("FXPlaying", (OnOffEffect.isOn) ? 1 : 0);
    }

    public void VolumeMusic()
    {
        SoundController.AudioSourceMusic.volume = VolumeM.value;

        PlayerPrefs.SetFloat("VolumeMusic", VolumeM.value);
    }

    public void VolumeEffect()
    {
        SoundController.AudioSourceFX.volume = VolumeE.value;
        PlayerPrefs.SetFloat("VolumeEffect", VolumeE.value);
    }

    void LoadPreferences()
    {
        bool MusicPlaying = (PlayerPrefs.GetInt("MusicPlaying") == 1);
        bool FXPlaying = (PlayerPrefs.GetInt("FXPlaying") == 1);
        float VolumeMusic = PlayerPrefs.GetFloat("VolumeMusic");
        float VolumeEffect = PlayerPrefs.GetFloat("VolumeEffect");

        OnOffMusic.isOn = MusicPlaying;
        OnOffEffect.isOn = FXPlaying;
        VolumeM.value = VolumeMusic;
        VolumeE.value = VolumeEffect;
    }
}
