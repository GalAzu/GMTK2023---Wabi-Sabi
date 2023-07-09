using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using TMPro;

public class InfoPanel : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public TextMeshProUGUI text;
    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        FadeOutPanel();
    }
    [Button]
    public void FadeOutPanel()
    {
        StartCoroutine(image.FadeOutSprite(1.5f));
        Invoke("InvokeActiveFalse", 1.5f);
    }
    public void InvokeActiveFalse()
    {
        text.gameObject.SetActive(false);
    }
}
