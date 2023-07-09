using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private float timerMax;
    private float currentTime;
    public enum ActiveScreen { MainMenu, GameSession }
    public ActiveScreen activeScreen;
    public PlayerStats[] players;
    public float score;

    private void Awake()
    {
        currentTime = timerMax;
        MakeSingleton();
        players = FindObjectsOfType<PlayerStats>();
    }
    void Start()
    {
        SetGameScreen(ActiveScreen.MainMenu);
        Time.timeScale = 0;
    }

    private void Update()
    {
        StartTimer();
    }
    public void SetGameScreen(ActiveScreen screen) => activeScreen = screen;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SetGameScreen(ActiveScreen.GameSession);
        Time.timeScale = 1;
        UIManager.Instance.mainMenu.FadeOutMenu();
        AudioManager.instance.StartMusic();
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
        if (activeScreen == ActiveScreen.GameSession)
        {
            UIManager.Instance.TogglePauseMenu();
        }
    }

    public void BackToMainMenu()
    {
        UIManager.Instance.mainMenu.gameObject.SetActive(true);
    }
    [Button]
    public void Lose()
    {
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
    }

}
