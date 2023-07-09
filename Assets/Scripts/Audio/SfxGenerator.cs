// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using _AudioStuff;

// public class SfxGenerator : MonoBehaviour
// {
//     public enum GenerationState { Start, Collision, Interaction }
//     public GenerationState sfxGeneratorEvent;
//     public AudioUnitSFX.SfxToPlay sfxToPlay;
//     [Space]
//     public bool randomPitch;
//     public bool randomVol;
//     public bool playDelayTime;
//     public float delayTime;
//     [Space]
//     public AudioSource source;

//     private void Start()
//     {

//         if (sfxGeneratorEvent == GenerationState.Start)
//         {
//             if (!playDelayTime)
//                 AudioManager.instance.AttachAndPlaySFX(transform, sfxToPlay, source, randomPitch, randomVol);
//             else
//                 AudioManager.instance.AttachAndPlaySFX(transform, sfxToPlay, source, randomPitch, randomVol, delayTime);
//         }
//     }
//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (sfxGeneratorEvent == GenerationState.Collision)
//         {
//             if (!playDelayTime)
//                 AudioManager.instance.AttachAndPlaySFX(transform, sfxToPlay, source, randomPitch, randomVol);
//             else
//                 AudioManager.instance.AttachAndPlaySFX(transform, sfxToPlay, source, randomPitch, randomVol, delayTime);
//         }
//     }
//     private void OnMouseDown()
//     {

//         if (sfxGeneratorEvent == GenerationState.Interaction)
//         {
//             if (!playDelayTime)
//                 AudioManager.instance.AttachAndPlaySFX(transform, sfxToPlay, source, randomPitch, randomVol);
//             else
//                 AudioManager.instance.AttachAndPlaySFX(transform, sfxToPlay, source, randomPitch, randomVol, delayTime);
//         }
//     }
// }
