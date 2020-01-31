using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    private AudioSource _audioSource;
    public GameObject SoundPlay;
    public GameObject SoundMute;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        SoundMute.SetActive(true);
        _audioSource.Stop();
        if(SoundPlay == true)
        {
            SoundPlay.SetActive(false);
        }
    }
    public void MuteMusic()
    {
        _audioSource.Play();
        SoundPlay.SetActive(true);
        if (SoundMute == true)
        {
            SoundMute.SetActive(false);
        }
    }

}
