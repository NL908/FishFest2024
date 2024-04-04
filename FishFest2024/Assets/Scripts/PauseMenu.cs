using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public GameObject pauseMenuUI;

    public bool isPaused = false;

    private InputAction _resumeInputAction;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _resumeInputAction = InputManager.inputActions.Pause.Resume;
        _resumeInputAction.performed += OnResume;
    }

    private void OnResume(InputAction.CallbackContext obj)
    {
        Resume();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        InputManager.ToggleActionMap(InputManager.inputActions.Pause);
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        InputManager.ToggleActionMap(InputManager.inputActions.Player);
        isPaused = false;
    }
}
