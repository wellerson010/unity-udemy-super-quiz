using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundController : MonoBehaviour
{

    private static GameObject SoundController;

    public AudioSource AudioSourceMusic, AudioSourceFX;

    public AudioClip RightSound;
    public AudioClip WrongSound;
    public AudioClip ButtonSound;
    public AudioClip Level3StarsSound;
    public AudioClip[] Musics;

    void Awake()
    {
        if (SoundController == null)
        {
            SoundController = gameObject;
            DontDestroyOnLoad(SoundController);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadPreferences()
    {
        if (PlayerPrefs.GetInt("DefaultValues") == 0)
        {
            PlayerPrefs.SetInt("DefaultValues", 1);
            PlayerPrefs.SetInt("MusicPlaying", 1);
            PlayerPrefs.SetInt("FXPlaying", 1);
            PlayerPrefs.SetFloat("VolumeMusic", 1f);
            PlayerPrefs.SetFloat("VolumeEffect", 1f);
        }

        bool MusicPlaying = (PlayerPrefs.GetInt("MusicPlaying") == 1);
        bool FXPlaying = (PlayerPrefs.GetInt("FXPlaying") == 1);
        float VolumeMusic = PlayerPrefs.GetFloat("VolumeMusic");
        float VolumeEffect = PlayerPrefs.GetFloat("VolumeEffect");

        AudioSourceMusic.mute = !MusicPlaying;
        AudioSourceFX.mute = !FXPlaying;

        AudioSourceMusic.volume = VolumeMusic;
        AudioSourceFX.volume = VolumeEffect;
    }

    // Use this for initialization
    void Start()
    {
        LoadPreferences();
        AudioSourceMusic.clip = Musics[0];
        AudioSourceMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayRightSound()
    {
        AudioSourceFX.PlayOneShot(RightSound);
    }

    public void Play3StarsSound()
    {
        AudioSourceFX.PlayOneShot(Level3StarsSound);
    }

    public void PlayWrongSound()
    {
        AudioSourceFX.PlayOneShot(WrongSound);
    }

    public void PlayButtonSound()
    {
        AudioSourceFX.PlayOneShot(ButtonSound);
    }
}
