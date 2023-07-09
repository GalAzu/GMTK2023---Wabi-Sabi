using System.Security.Claims;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _AudioStuff;
using System;

[System.Serializable]
public class AudioSourcePool
{
    public int numberOfAudioSources = 7;
    public float maxDistanceRolloff;
    public float minDistanceRoloff;

    [Range(-80, 4)]
    public float volume;
    public List<AudioSource> audioSources = new();
    public bool randomPitch;
    public Vector2 randomFactor { get => AudioManager.instance.RandomSfxFactor; }
    public float randomMultiplier = 1;

    public AudioSource GetSource()
    {
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                Debug.Log($"Got Source {source}");
                return source;
            }
        }
        return null;
    }
    public void PlayFromPool(Vector2 position)
    {
        for (int i = 0; i < audioSources.Count; i++)
        {

            if (!audioSources[i].isPlaying)
            {
                if (randomPitch)
                {
                    audioSources[i].pitch = SourceRandomPitch(randomFactor.x, randomFactor.y);
                }
                audioSources[i].volume = volume;
                audioSources[i].transform.position = position;
                audioSources[i].Play();
                return;
            }
        }
    }

    public void PlayFromPool(Vector2 position, float customPitch = 1)
    {
        for (int i = 0; i < audioSources.Count; i++)
        {

            if (!audioSources[i].isPlaying)
            {
                audioSources[i].volume = volume;
                audioSources[i].pitch = customPitch;
                audioSources[i].transform.position = position;
                audioSources[i].Play();
                return;
            }
        }
    }

    public float SourceRandomPitch(float min, float max)
    {
        var newPitch = UnityEngine.Random.Range(min, max) * randomMultiplier;
        return newPitch;

    }
    public int PlaySequenceFromPoolGetIndex(Vector2 position, int index)
    {
        if (!audioSources[index].isPlaying)
        {
            audioSources[index].transform.position = position;
            audioSources[index].Play();
        }
        return index;
    }

    public void PlaySequenceFromPool(Vector2 position, int index)
    {
        if (index < audioSources.Count)
        {
            if (!audioSources[index].isPlaying)
            {
                audioSources[index].transform.position = position;
                audioSources[index].Play();
                return;
            }
        }
    }
    public void PlayStaticSequenceFromPool(int index)
    {
        if (index < audioSources.Count)
        {
            if (!audioSources[index].isPlaying)
            {
                audioSources[index].transform.position = AudioManager.staticSFXpos;
                audioSources[index].Play();
                return;
            }
        }
    }

    public int PlayFromPoolGetIndex(Vector2 position)
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                audioSources[i].transform.position = position;
                audioSources[i].Play();
                return i;
            }
        }


        return -1;
    }

    public void StopAtIndex(int index)
    {
        if (index < audioSources.Count)
        {
            audioSources[index].Stop();
        }
    }
}
public class SFXAudioSourcePool : AudioSourcePool
{
    public SfxToPlay sfxType;
}
public class SeqAudioSourcePool : AudioSourcePool
{
    public SeqToPlay sfxType;
}
public class UIAudioSourcePool : AudioSourcePool
{
    public UISfxToPlay sfxType;
}
