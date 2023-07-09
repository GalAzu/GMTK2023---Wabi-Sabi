using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public HealthBar playerOneHealthBar;
    public DefenceBar playerOneDefenceBar;

    public HealthBar playerTwoHealthBar;
    public DefenceBar playerTwoDefenceBar;
    public GameObject darkBG;
    public GameObject PauseMenu;
    public WinScreen winScreen;

    public GameObject LoseScreen;

    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        MakeSingleton();
        LoseScreen.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(false);
        darkBG.gameObject.SetActive(false);
    }
    public void ToggleWinScreen()
    {
        bool isSwitch = winScreen.gameObject.activeSelf;
        isSwitch = !isSwitch;
        winScreen.gameObject.SetActive(isSwitch);
        darkBG.gameObject.SetActive(isSwitch);
        winScreen.UpdateScoreText();
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
    public void TogglePauseMenu()
    {
        bool isSwitch = PauseMenu.gameObject.activeSelf;
        isSwitch = !isSwitch;
        PauseMenu.gameObject.SetActive(isSwitch);
        darkBG.gameObject.SetActive(isSwitch);
    }
    public void ToggleLoseScreen()
    {
        bool isSwitch = LoseScreen.gameObject.activeSelf;
        isSwitch = !isSwitch;
        LoseScreen.gameObject.SetActive(isSwitch);
        darkBG.gameObject.SetActive(isSwitch);
    }
}
