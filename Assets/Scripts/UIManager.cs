using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public HealthBar playerOneHealthBar;
    public DefenceBar playerOneDefenceBar;

    public HealthBar playerTwoHealthBar;
    public DefenceBar playerTwoDefenceBar;

    private void Awake()
    {
        MakeSingleton();
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
