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
    public double fadeOutDuration;

    public int RandomGameplayMusic()
    {
        var i = Random.Range(0, data.bgmList.Length);
        return i;
    }
    public void StopAudioIfPlaying(AudioSource source)
    {
        if (source.clip != null && source.isPlaying)
        {
            source.Stop();
        }
    }
    public double GetDuration(AudioClip clip)
    {
        double duration = clip.samples / clip.frequency;
        return duration;

    }
    [Button]
    public void PlayReleventBGM()
    {
        if (GameManager.Instance.activeScreen == GameManager.ActiveScreen.MainMenu)
        {
            bgmSource.clip = data.bgmList[0].clips[0];
            bgmSource.Play();
        }
        else if (GameManager.Instance.activeScreen == GameManager.ActiveScreen.GameSession)
        {
            if (bgmSource.isPlaying)
            {
                AudioManager.FadeOut(bgmSource, fadeOutDuration);
                bgmSource_2.clip = data.bgmList[0].clips[1];
                bgmSource_2.PlayScheduled(fadeOutDuration);
            }
        }
    }
}
