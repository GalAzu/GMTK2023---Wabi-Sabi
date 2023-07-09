using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _AudioStuff;
using Sirenix.OdinInspector;

public class bgmHandler : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource bgmSource_2;
    public AudioManagerData data;
    private double startTime;
    public float fadeDuration;

    void Start()
    {
        PlayReleventBGM();
    }
    [Button]
    public void PlayReleventBGM()
    {
        switch (GameManager.Instance.activeScreen)
        {
            case (GameManager.ActiveScreen.MainMenu):
                bgmSource.clip = data.bgmList[0].clips[0];
                bgmSource.Play();
                break;
            case (GameManager.ActiveScreen.GameSession):
                bgmSource_2.clip = data.bgmList[0].clips[1];
                if (bgmSource.isPlaying)
                {
                    StartCoroutine(AudioManager.FadeOut(bgmSource, fadeDuration));
                    bgmSource_2.clip = data.bgmList[0].clips[1];
                    StartCoroutine(AudioManager.FadeIn(bgmSource_2, fadeDuration));
                }
                else
                {
                    bgmSource_2.Play();
                }
                break;
        }
    }

}
