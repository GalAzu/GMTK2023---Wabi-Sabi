using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _AudioStuff;
using UnityEngine.Audio;
using Sirenix.OdinInspector;
using System;
[DefaultExecutionOrder(-1000)]
public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [FoldoutGroup("Audio Sources")]
    [Required]
    public AudioSource staticSFX;
    [FoldoutGroup("Audio Sources")]
    [Required]
    public AudioSource staticUISFX;
    public static Vector3 staticSFXpos;
    public static Vector3 staticUISFXpos;
    [Required]
    public AudioSourceHandler sourceHandler;
    public float defaultSFXVolume = 0;
    public float defaultUISFXVolume = 0;
    public float defaultBGMVolume = 0;
    private bgmHandler bgmHandler;

    [SerializeField, FoldoutGroup("Mixer")] public AudioMixerGroup masterBus;
    [SerializeField, FoldoutGroup("Mixer")] public AudioMixerGroup bgmBus;
    [SerializeField, FoldoutGroup("Mixer")] public AudioMixerGroup sfxBus;
    [SerializeField, FoldoutGroup("Mixer")] public AudioMixerGroup UISfx;
    [SerializeField, FoldoutGroup("Mixer")] public AudioMixer mixer;

    [Range(0.0001f, 1), FoldoutGroup("Mixer")] public float masterVolume = 0;

    [Range(0.0001f, 1), FoldoutGroup("Mixer")] public float musicVolume = 0;

    [Range(0.0001f, 1), FoldoutGroup("Mixer")] public float ambientVolume = 0;

    [Range(0.0001f, 1), FoldoutGroup("Mixer")] public float fxVolume = 0;
    public AudioManagerData data;
    public static AudioManager instance;
    public bool playBgmOnStart;
    public float volumeUnit = 0.099f;
    public Vector2 RandomSfxFactor;

    [Button, FoldoutGroup("Mixer")]
    private void SetMixerVolume()
    {
        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSfxVolume(fxVolume);
    }
    void Update()
    {
        staticSFXpos = transform.position;
        staticUISFXpos = transform.position;
    }

    private void Awake()
    {
        sourceHandler = GetComponentInChildren<AudioSourceHandler>();
        bgmHandler = GetComponentInChildren<bgmHandler>();
        instance = this;
    }
    #region bgmHandler
    // public void StartMusic() => bgmHandler.PlayReleventBGM();
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = startVolume;
    }
    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.volume = 0f;
        audioSource.Play();

        while (audioSource.volume < 1f)
        {
            audioSource.volume += Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = 1f;
    }

    #endregion

    #region sfxHandler
    public void PlayUISFXFromPool(UISfxToPlay sfx, Vector3 position)
    {
        if (!sourceHandler.UIPoolHolder.ContainsKey(sfx))
        {
            Debug.LogWarning($"AUDIO IS MISSING : NO ENUM {sfx} HAS BEEN FOUND IN THE UI SFX POOL");
            return;
        }
        sourceHandler.UIPoolHolder[sfx].PlayFromPool(position);
    }
    public void PlaySFXFromPool(SfxToPlay sfx, Vector2 position)
    {
        if (!sourceHandler.sfxPoolsHolder.ContainsKey(sfx))
        {
            Debug.LogWarning($"AUDIO IS MISSING : NO ENUM {sfx} HAS BEEN FOUND IN THE SFX POOL");
            return;
        }
        sourceHandler.sfxPoolsHolder[sfx].PlayFromPool(position);
    }
    public void PlaySFXFromPool(SfxToPlay sfx, Vector2 position, float pitch)
    {
        if (!sourceHandler.sfxPoolsHolder.ContainsKey(sfx))
        {
            Debug.LogWarning($"AUDIO IS MISSING : NO ENUM {sfx} HAS BEEN FOUND IN THE SFX POOL");
            return;
        }
        sourceHandler.sfxPoolsHolder[sfx].PlayFromPool(position, pitch);
    }

    public AudioUnitUI GetUIsfx(UISfxToPlay sfxToPlay)
    {
        Debug.LogWarning($"AUDIO IS MISSING : NO ENUM {sfxToPlay} HAS BEEN FOUND IN THE UI SFX POOL");
        var sfx = data.uiSfxList.Find(num => num.uiSfxToPlay == sfxToPlay);
        return sfx;
    }
    public AudioUnitSFX GetUIsfx(SfxToPlay sfxToPlay)
    {
        Debug.LogWarning($"AUDIO IS MISSING : NO ENUM {sfxToPlay} HAS BEEN FOUND IN THE UI SFX POOL");
        var sfx = data.sfxList.Find(num => num.sfxToPlay == sfxToPlay);
        return sfx;
    }
    public void PlayStaticOneShot(UISfxToPlay sfxToPlay)
    {
        var sfx = GetUIsfx(sfxToPlay);
        staticSFX.PlayOneShot(sfx.clip);
    }
    public void PlayStaticOneShot(SfxToPlay sfxToPlay)
    {
        var sfx = GetUIsfx(sfxToPlay);
        staticSFX.PlayOneShot(sfx.clip);
    }
    public void PlaySeqFromPool(SeqToPlay sfx, Vector2 position, int index)
    {
        if (!sourceHandler.SequencePoolHolder.ContainsKey(sfx))
        {
            Debug.LogWarning($"AUDIO IS MISSING : NO ENUM {sfx} HAS BEEN FOUND IN THE SEQUENCE POOL");
            return;
        }
        sourceHandler.SequencePoolHolder[sfx].PlaySequenceFromPool(position, index);
    }
    public void PlayStaticSeqFromPool(SeqToPlay sfx, int index)
    {
        if (!sourceHandler.SequencePoolHolder.ContainsKey(sfx))
        {
            Debug.LogWarning($"AUDIO IS MISSING : NO ENUM {sfx} HAS BEEN FOUND IN THE SEQUENCE POOL");
            return;
        }
        sourceHandler.SequencePoolHolder[sfx].PlayStaticSequenceFromPool(index);
    }


    public int PlaySFXFromPoolAndGetIndex(SfxToPlay sfx, Vector2 position)
    {
        if (!sourceHandler.sfxPoolsHolder.ContainsKey(sfx))
        {
            Debug.LogWarning($"AUDIO IS MISSING : NO ENUM {sfx} HAS BEEN FOUND IN THE SFX POOL");
            return 0;
        }

        return sourceHandler.sfxPoolsHolder[sfx].PlayFromPoolGetIndex(position);
    }

    public int PlaySeqFromPoolAndGetIndex(SeqToPlay sfx, Vector2 position, Dictionary<SeqToPlay, AudioSourcePool> dict, int index)
    {
        if (!dict.ContainsKey(sfx))
        {
            Debug.LogWarning($"AUDIO IS MISSING : NO ENUM {sfx} HAS BEEN FOUND IN THE SEQUENCE POOL");
            return 0;
        }

        return dict[sfx].PlayFromPoolGetIndex(position);
    }

    public void StopSFXWithIndex(SfxToPlay sfx, int index)
    {
        if (!sourceHandler.sfxPoolsHolder.ContainsKey(sfx))

        {
            Debug.LogWarning($"AUDIO IS MISSING : NO ENUM {sfx} HAS BEEN FOUND IN THE SFX POOL");
            return;
        }

        sourceHandler.sfxPoolsHolder[sfx].StopAtIndex(index);
    }
    public void StopSeqWithIndex(SeqToPlay sfx, int index)
    {
        if (!sourceHandler.SequencePoolHolder.ContainsKey(sfx))
        {
            Debug.LogWarning($"AUDIO IS MISSING : NO ENUM {sfx} HAS BEEN FOUND IN THE SEQUENCE POOL");
            return;
        }

        sourceHandler.SequencePoolHolder[sfx].StopAtIndex(index);
    }



    #endregion
    #region HandleMixer
    [Button]
    public void MuteMaster() => mixer.SetFloat("masterBus", -80);
    [Button]
    public void MuteSFX() => mixer.SetFloat("sfxBus", -80);
    [Button]
    public void MuteMusic() => mixer.SetFloat("bgmBus", -80);

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        mixer.SetFloat("masterBus", Mathf.Log10(masterVolume) * 20);
    }
    public void SetSfxVolume(float volume)
    {
        fxVolume = volume;
        mixer.SetFloat("sfxBus", Mathf.Log10(fxVolume) * 20);

    }
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        mixer.SetFloat("bgmBus", Mathf.Log10(musicVolume) * 20);
    }
    public void LowerMasterVolume()
    {
        masterVolume -= volumeUnit;
        if (masterVolume <= 0)
            masterVolume = 0.0001f;
        mixer.SetFloat("masterBus", Mathf.Log10(masterVolume) * 20);
    }
    public void RaiseMasterVolume()
    {
        masterVolume += volumeUnit;
        if (masterVolume >= 1)
            masterVolume = 1;
        mixer.SetFloat("masterBus", Mathf.Log10(masterVolume) * 20);
    }
    public void LowerSFXVolume()
    {
        fxVolume -= volumeUnit;
        if (fxVolume <= 0)
            fxVolume = 0.0001f;
        mixer.SetFloat("sfxBus", Mathf.Log10(fxVolume) * 20);
    }
    public void RaiseSFXVolume()
    {
        fxVolume += volumeUnit;
        if (fxVolume >= 1)
            fxVolume = 1;
        mixer.SetFloat("sfxBus", Mathf.Log10(fxVolume) * 20);
    }
    public void LowerBGMVolume()
    {
        musicVolume -= volumeUnit;
        if (musicVolume <= 0)
            musicVolume = 0.0001f;
        mixer.SetFloat("bgmBus", Mathf.Log10(musicVolume) * 20);
    }
    public void RaiseBGMVolume()
    {
        musicVolume += volumeUnit;
        if (musicVolume >= 1)
            musicVolume = 1;
        mixer.SetFloat("bgmBus", Mathf.Log10(musicVolume) * 20);

    }

    #endregion

}


