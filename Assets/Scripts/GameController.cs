using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance
    {
        get;
        set;
    }

    private int _levelUnlockedFlags = 1;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
    }

    void Start()
    {
        SceneManager.LoadScene(1);
    }
    
    public bool IsLevelUnlocked(int levelNumber)
    {
        return (_levelUnlockedFlags & (1 << (levelNumber - 1))) != 0;
    }

    public void UnlockLevel(int levelNumber)
    {
        _levelUnlockedFlags |= (1 << (levelNumber - 1));
    }
}
