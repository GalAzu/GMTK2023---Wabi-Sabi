using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class WinScreen : MonoBehaviour
{
    private float score { get => GameManager.Instance.score; }
    public TextMeshProUGUI scoreText;

    [Button]
    public void UpdateScoreText()
    {
        scoreText.text = $"Total Score: {score}";
    }
}


