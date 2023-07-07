using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject mainMenuUI;

    private void Awake()
    {
        MakeSingleton();
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

}
