using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public AudioSource source;
    public AudioClip startGameClip;
    public Image[] imagesToFade;
    public float fadeDuration;
    [Button]
    public void FadeOutMenu()
    {
        foreach (var image in imagesToFade)
        {
            StartCoroutine(image.FadeOutSprite(fadeDuration));
        }
        StartCoroutine(FadeOut(source, 3f));
    }
    [Button]
    public void FadeInMenu()
    {
        Time.timeScale = 1;
        foreach (var image in imagesToFade)
        {
            StartCoroutine(image.FadeInSprite(fadeDuration));
        }
    }
    public void StartGame()
    {
        Invoke("MoveToNextScene", fadeDuration);
        FadeOutMenu();
        source.PlayOneShot(startGameClip);
    }
    public void MoveToNextScene() => SceneManager.LoadScene(1);
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
}
