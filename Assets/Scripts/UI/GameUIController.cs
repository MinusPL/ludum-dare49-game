using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
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
}
