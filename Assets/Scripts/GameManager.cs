using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private float timerMax;
    private float currentTime;

    private void Awake()
    {
        currentTime = timerMax;
        MakeSingleton();
    }

    private void Update()
    {
        StartTimer();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        mainMenuUI.SetActive(false);
    }

    private void MakeSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void StartTimer()
    {
        if(currentTime <= 0)
        {
            currentTime = timerMax;
        }

        currentTime -= Time.deltaTime;

        timerText.text = currentTime.ToString("F0");
    }

}
