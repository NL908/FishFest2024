using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCinematic : MonoBehaviour
{
    private OpenCinematicLoader _loader;
    private void Start()
    {
        _loader = GetComponentInChildren<OpenCinematicLoader>();
        AudioManager.instance.PlayMenuMusic();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _loader.LoadMainMenu();
        }
    }
}
