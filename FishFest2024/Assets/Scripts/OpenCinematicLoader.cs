using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;

public class OpenCinematicLoader : MonoBehaviour
{
    [SerializeField]
    private TransitionSettings transitionSettings;
    public void LoadMainMenu()
    {
        TransitionManager.Instance().Transition("StartingScreen", transitionSettings, 0);
    }
}
