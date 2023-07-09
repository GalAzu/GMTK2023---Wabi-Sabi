
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _AudioStuff;
using UnityEngine.Audio;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "AudioManagerData", menuName = "ScriptableObjects/AudioManagerData", order = 0)]
public class AudioManagerData : ScriptableObject
{


    [FoldoutGroup("SFX Database")]
    public List<AudioUnitSFX> sfxList = new List<AudioUnitSFX>();
    [FoldoutGroup("SFX Database")]
    public List<AudioUnitSFXSequence> sfxSequencesList = new List<AudioUnitSFXSequence>();
    [FoldoutGroup("SFX Database")]
    public List<AudioUnitUI> uiSfxList = new List<AudioUnitUI>();
    [FoldoutGroup("BGM Stuff")]
    public AudioUnitBGM[] bgmList;
    [FoldoutGroup("BGM Stuff")]
    public AudioUnitBGM IntroToMainMenu;

    private void OnValidate()
    {

        foreach (var obj in sfxList)
        {
            obj.name = obj?.sfxToPlay.ToString();
        }
        foreach (var obj in uiSfxList)
        {
            obj.name = obj?.uiSfxToPlay.ToString();
        }
        foreach (var obj in sfxSequencesList)
        {
            obj.sequenceName = obj.SequenceToPlay.ToString();
        }
    }
}