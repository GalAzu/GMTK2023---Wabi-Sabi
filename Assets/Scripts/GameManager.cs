using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private float timerMax;
    private float currentTime;
    public PlayerStats[] players;
    public bool isGamePause;
    public float score;

    private void Awake()
    {
        currentTime = timerMax;
        MakeSingleton();
        players = FindObjectsOfType<PlayerStats>();
    }

    public void StartSceneWithIndex(int i)
    {
        SceneManager.LoadScene(i);
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
        StartSceneWithIndex(1);
        AudioManager.instance.PlaySFXFromPool(_AudioStuff.SfxToPlay.StartGame, AudioManager.staticSFXpos);
    }
    [Button]
    public void Win()
    {
        AudioManager.instance.PlaySFXFromPool(_AudioStuff.SfxToPlay.Lose, AudioManager.instance.staticSFX.transform.position);
        UIManager.Instance.ToggleWinScreen();
        GetEndScore();
        Time.timeScale = 0;
    }
    public void GetEndScore()
    {
        foreach (var player in players)
        {
            score += player.curHealth;
        }
    }
    [Button]
    public void Pause()
    {
        UIManager.Instance.TogglePauseMenu();
        isGamePause = true;
    }
    public void BackToGame()
    {
        if (isGamePause)
        {
            Time.timeScale = 1;
            UIManager.Instance.TogglePauseMenu();
        }
    }

    public void BackToMainMenu()
    {
        StartSceneWithIndex(0);
    }
    [Button]
    public void Lose()
    {
        UIManager.Instance.ToggleLoseScreen();
        AudioManager.instance.PlaySFXFromPool(_AudioStuff.SfxToPlay.Lose, AudioManager.instance.staticSFX.transform.position);
        Time.timeScale = 0;

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
        if (currentTime <= 0)
        {
            currentTime = timerMax;
        }

        currentTime -= Time.deltaTime;

        timerText.text = currentTime.ToString("F0");
        if (currentTime == 0)
        {
            Win();
        }
    }
    public void PlayGenericUIClick()
    {
        AudioManager.instance.PlaySFXFromPool(_AudioStuff.SfxToPlay.GenericClick, AudioManager.staticSFXpos);
    }
}
