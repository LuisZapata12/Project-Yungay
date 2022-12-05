using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sounds[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public Slider sliderMaster;
    public float sliderValueMaster;
    public Image muteImageMaster;

    public Slider sliderMusic;
    public float sliderValueMusic;
    public Image muteImageMusic;

    public Slider sliderSfx;
    public float sliderValueSfx;
    public Image muteImageSfx;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        sliderMaster.value = PlayerPrefs.GetFloat("volumenMaster", 0.5f);
        AudioListener.volume = sliderMaster.value;
        CheckMuteMaster();

        sliderMusic.value = PlayerPrefs.GetFloat("volumenMusic", 0.5f);
        musicSource.volume = sliderMusic.value;
        CheckMuteMusic();

        sliderSfx.value = PlayerPrefs.GetFloat("volumenSfx", 0.5f);
        sfxSource.volume = sliderSfx.value;
        CheckMuteSfx();
    }

    public void Start()
    {
        
    }

    #region "Menu Opciones"
    public void ChangeSliderMaster(float value)
    {
        sliderValueMaster = value;
        PlayerPrefs.SetFloat("volumenMaster", sliderValueMaster);
        AudioListener.volume = sliderValueMaster;
        CheckMuteMaster();
    }

    public void ChangeSliderMusic(float value)
    {
        sliderValueMusic = value;
        PlayerPrefs.SetFloat("volumenMusic", sliderValueMusic);
        musicSource.volume = sliderValueMusic;
        CheckMuteMusic();
    }

    public void ChangeSliderSfx(float value)
    {
        sliderValueSfx = value;
        PlayerPrefs.SetFloat("volumenSfx", sliderValueSfx);
        sfxSource.volume = sliderValueSfx;
        CheckMuteSfx();
    }

    public void CheckMuteMaster()
    {
        if (sliderValueMaster == 0)
        {
            muteImageMaster.enabled = true;
        }
        else
        {
            muteImageMaster.enabled = false;
        }
    }

    public void CheckMuteMusic()
    {
        if (sliderValueMusic == 0)
        {
            muteImageMusic.enabled = true;
        }
        else
        {
            muteImageMusic.enabled = false;
        }
    }
    public void CheckMuteSfx()
    {
        if (sliderValueSfx == 0)
        {
            muteImageSfx.enabled = true;
        }
        else
        {
            muteImageSfx.enabled = false;
        }
    }
    #endregion
    public void PlayMusic(string name)
    {
        Sounds s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sounds s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
}
