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
    public MainMenu mainMenu;
    public GameObject darkBG;
    public GameObject PauseMenu;
    public WinScreen winScreen;


    private void Awake()
    {
        MakeSingleton();
    }
    public void UpdateWinScreen() => winScreen.UpdateScoreText();
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
}
