using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _AudioStuff;
using Sirenix.OdinInspector;
[DefaultExecutionOrder(-500)]
public class AudioSourceHandler : MonoBehaviour
{

    [ShowInInspector]
    public Dictionary<SfxToPlay, SFXAudioSourcePool> sfxPoolsHolder = new();
    [ShowInInspector]
    public Dictionary<SeqToPlay, SeqAudioSourcePool> SequencePoolHolder = new();
    [ShowInInspector]
    public Dictionary<UISfxToPlay, UIAudioSourcePool> UIPoolHolder = new();
    void Awake()
    {
        InitAudioSources();
    }
    [Button]
    public void InitAudioSources()
    {
        InitPools();
    }
    public void InitPools()
    {
        foreach (AudioUnitSFX sfx in AudioManager.instance.data.sfxList)
        {
            var newPool = CreateSfxPool(sfx, sfx.randomPitch);
            newPool.maxDistanceRolloff = sfx.maxDistance;
            newPool.minDistanceRoloff = sfx.minDistance;
            sfxPoolsHolder.Add(sfx.sfxToPlay, newPool);
            newPool.volume = sfx.volume;
            UpdateSFXPoolData(sfx.sfxToPlay, newPool, sfx.clip);
        }
        foreach (AudioUnitSFXSequence seq in AudioManager.instance.data.sfxSequencesList)
        {
            var newPool = CreateSeqPool(seq);
            newPool.maxDistanceRolloff = seq.maxDistance;
            newPool.minDistanceRoloff = seq.minDistance;
            SequencePoolHolder.Add(seq.SequenceToPlay, newPool);
            newPool.volume = seq.volume;
            var newGameObj = new GameObject($"Audio Pool -{seq.sequenceName}");
            newGameObj.transform.parent = this.transform;

            for (int i = 0; i < seq.sfxSequence.Length; i++)
            {
                UpdateSeqPoolData(seq.SequenceToPlay, newPool, seq.sfxSequence[i].clip, newGameObj.transform);
            }
        }
        foreach (AudioUnitUI sfx in AudioManager.instance.data.uiSfxList)
        {
            if (sfx.isPooled)
            {
                var newPool = CreateUIPool(sfx);
                newPool.maxDistanceRolloff = sfx.maxDistance;
                newPool.volume = sfx.volume;
                UIPoolHolder.Add(sfx.uiSfxToPlay, newPool);
                var obj = new GameObject($"Audio Pool -{sfx.name}");
                obj.transform.parent = this.transform;
                UpdateUIPoolData(sfx.uiSfxToPlay, newPool, sfx.clip, obj.transform);
            }
        }
    }
    private SFXAudioSourcePool CreateSfxPool(AudioUnitSFX sfx, bool isRandom)
    {
        var newPool = new SFXAudioSourcePool();
        newPool.numberOfAudioSources = sfx.maxInstances;
        newPool.randomPitch = isRandom;
        return newPool;
    }
    private SeqAudioSourcePool CreateSeqPool(AudioUnitSFXSequence sfx)
    {
        var newPool = new SeqAudioSourcePool();
        newPool.numberOfAudioSources = sfx.maxInstances;
        return newPool;
    }
    private UIAudioSourcePool CreateUIPool(AudioUnitUI sfx)
    {
        var newPool = new UIAudioSourcePool();
        newPool.numberOfAudioSources = sfx.maxInstances;
        return newPool;
    }
    public void UpdateSeqPoolData(SeqToPlay sfxEnum, SeqAudioSourcePool pool, AudioClip clip, Transform parent)
    {
        pool.sfxType = sfxEnum;

        for (int i = 0; i < pool.numberOfAudioSources; i++)
        {
            var sourceObject = new GameObject($"AudioPool - {pool.sfxType}");
            sourceObject.transform.parent = parent;
            AudioSource source = sourceObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = AudioManager.instance.sfxBus;
            source.spatialBlend = 1;
            source.clip = clip;
            source.rolloffMode = AudioRolloffMode.Linear;
            source.maxDistance = pool.maxDistanceRolloff;
            source.minDistance = pool.minDistanceRoloff;
            source.spread = 150;
            source.volume = pool.volume;
            pool.audioSources.Add(source);
        }
    }
    public void UpdateSFXPoolData(SfxToPlay sfxEnum, SFXAudioSourcePool pool, AudioClip clip)
    {
        pool.sfxType = sfxEnum;
        var parentObj = new GameObject($"PoolHolder - {pool.sfxType}");
        for (int i = 0; i < pool.numberOfAudioSources; i++)
        {
            var sourceObject = new GameObject($"AudioPool - {pool.sfxType}");

            sourceObject.transform.parent = parentObj.transform;
            parentObj.transform.parent = this.transform;
            AudioSource source = sourceObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.outputAudioMixerGroup = AudioManager.instance.sfxBus;
            source.spatialBlend = 1;
            source.clip = clip;
            source.rolloffMode = AudioRolloffMode.Linear;
            source.minDistance = pool.minDistanceRoloff;
            source.spread = 150;
            source.maxDistance = pool.maxDistanceRolloff;
            source.volume = pool.volume;
            pool.audioSources.Add(source);
        }
    }
    public void UpdateUIPoolData(UISfxToPlay sfxEnum, UIAudioSourcePool pool, AudioClip clip, Transform parent)
    {
        pool.sfxType = sfxEnum;

        for (int i = 0; i < pool.numberOfAudioSources; i++)
        {
            var sourceObject = new GameObject($"AudioPool - {pool.sfxType}");

            sourceObject.transform.parent = parent;


            AudioSource source = sourceObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = AudioManager.instance.UISfx;
            source.spatialBlend = 1;
            source.clip = clip;

            source.volume = pool.volume;
            pool.audioSources.Add(source);
        }
    }
    [Button]
    public void ClearAllPools()
    {
        foreach (KeyValuePair<SfxToPlay, SFXAudioSourcePool> kv in sfxPoolsHolder)
        {
            SFXAudioSourcePool pool;
            var poolValue = sfxPoolsHolder.TryGetValue(kv.Key, out pool);
            if (poolValue)
            {
                foreach (var source in pool.audioSources)
                {
                    Destroy(source.transform.parent.gameObject);
                }
            }
            pool.audioSources.Clear();
        }
        sfxPoolsHolder.Clear();
        foreach (KeyValuePair<SeqToPlay, SeqAudioSourcePool> kv in SequencePoolHolder)
        {
            SeqAudioSourcePool pool;
            var poolValue = SequencePoolHolder.TryGetValue(kv.Key, out pool);
            if (poolValue)
            {
                foreach (var source in pool.audioSources)
                {
                    Destroy(source.transform.parent.gameObject);
                }
            }
            pool.audioSources.Clear();
        }
        SequencePoolHolder.Clear();
        sfxPoolsHolder.Clear();
        foreach (KeyValuePair<UISfxToPlay, UIAudioSourcePool> kv in UIPoolHolder)
        {
            UIAudioSourcePool pool;
            var poolValue = UIPoolHolder.TryGetValue(kv.Key, out pool);
            if (poolValue)
            {
                foreach (var source in pool.audioSources)
                {
                    Destroy(source.transform.parent.gameObject);
                }
            }
            pool.audioSources.Clear();
        }
        UIPoolHolder.Clear();
    }

}





