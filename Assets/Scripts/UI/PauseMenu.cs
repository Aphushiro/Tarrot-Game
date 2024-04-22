using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool pauseMenuOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuOpen = !pauseMenuOpen;
            pauseMenu.SetActive(pauseMenuOpen);

            if (pauseMenuOpen)
            {
                Time.timeScale = 0f;
            } else
            {
                Time.timeScale = 1f;
            }
        }
        /*else if (pauseMenu.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }*/
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
