using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Image[] imagesToFade;
    public float fadeDuration;
    [Button]
    public void FadeOutMenu()
    {
        foreach (var image in imagesToFade)
        {
            StartCoroutine(image.FadeOutSprite(fadeDuration));
        }
    }
}
