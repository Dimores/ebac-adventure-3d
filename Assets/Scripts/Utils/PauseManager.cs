using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public KeyCode pauseKey = KeyCode.Escape;
    public GameObject pauseScreen;

    private bool _isPaused = false;

    private void Awake()
    {
        _isPaused = false;

        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(pauseKey)) Pause();
    }

    public void Pause()
    {
        if (_isPaused)
        {
            UnPause();
        }
        else
        {
            pauseScreen.SetActive(true);

            Time.timeScale = 0f;
            _isPaused = true;
        }
    }

    private void UnPause()
    {
        pauseScreen.SetActive(false);

        Time.timeScale = 1f;
        _isPaused = false;
    }
}
