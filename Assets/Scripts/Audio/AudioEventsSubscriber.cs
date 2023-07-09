using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _AudioStuff;
using System;

public class AudioEventsSubscriber : MonoBehaviour
{
    //On Win - LevelManager

    void OnEnable()
    {
        SubscribeToEvents();
    }
    void OnDisable()
    {
        UnsubscribeToEvents();
    }
    public void SubscribeToEvents()
    {

        MainMenu.instance.onStartButtonPressed += PlayStartGame;
        MainMenu.instance.onStartButtonPressed += PlayGameplayMusic;
    }


    public void UnsubscribeToEvents()
    {
        MainMenu.instance.onStartButtonPressed -= PlayStartGame;
        MainMenu.instance.onStartButtonPressed -= PlayGameplayMusic;
    }

    private void PlayStartGame() => AudioManager.instance.PlayStaticOneShot(UISfxToPlay.StartGame);
    public void PlayOrbAudio() => AudioManager.instance.PlaySFXFromPool(SfxToPlay.gainOrb, homeObject.instance.transform.position);
    public void PlayGameplayMusic() => AudioManager.instance.PlayGameplayMusic();

}
