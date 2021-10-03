using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject levelWon;
    public GameObject nextLevelButton;
    public TextMeshProUGUI levelText;
    private int nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        levelWon.SetActive(false);
    }

    public void SetPauseMenu(bool pause, bool menu)
	{
        if (menu)
            pauseMenu.SetActive(pause);
	}

    public void ExitToMenu()
	{
        SceneManager.LoadScene(1);
	}

    public void ReloadLevel()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    public void LoadNextLevel()
	{
        SceneManager.LoadScene(nextLevel+1);
	}

    public void LevelWon(int nLevel)
	{
        nextLevel = nLevel;
        if (nLevel == -1)
        {
            levelText.text = "That was last level!";
            nextLevelButton.GetComponent<Button>().interactable = false;
        }
        levelWon.SetActive(true);
	}
}
