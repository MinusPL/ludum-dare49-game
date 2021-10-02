using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public int levelNumber;
    public int targetSceneId;

    private Button _button;
    private GameController _gameController;
    public void OnLevelSelected()
    {
        if(GameController.Instance.IsLevelUnlocked(levelNumber))
            SceneManager.LoadScene(targetSceneId);
    }

    private void Start()
    {
        _button = GetComponent<Button>();
        _gameController = GameController.Instance;
    }

    private void Update()
    {
        _button.interactable = _gameController.IsLevelUnlocked(levelNumber);
    }
}
