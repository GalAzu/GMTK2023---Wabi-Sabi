using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SpriteExtensions
{
    public static IEnumerator FadeOutSprite(this Image sprite, float fadeDuration)
    {
        Color startColor = sprite.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float counter = 0f;

        while (counter < fadeDuration)
        {
            float t = counter / fadeDuration;
            Color lerpedColor = Color.Lerp(startColor, targetColor, t);
            sprite.color = lerpedColor;

            counter += Time.deltaTime;
            yield return null;
        }

        sprite.color = targetColor;
        sprite.gameObject.SetActive(false);
    }
    public static IEnumerator FadeInSprite(this Image sprite, float fadeDuration)
    {
        sprite.gameObject.SetActive(true);
        Color startColor = sprite.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        float counter = 0f;

        while (counter < fadeDuration)
        {
            float t = counter / fadeDuration;
            Color lerpedColor = Color.Lerp(startColor, targetColor, t);
            sprite.color = lerpedColor;

            counter += Time.deltaTime;
            yield return null;
        }

        sprite.color = targetColor;
    }
}
