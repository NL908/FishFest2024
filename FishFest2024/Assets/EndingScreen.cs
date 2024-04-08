using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition;

public class EndingScreen : MonoBehaviour
{
    [SerializeField] TransitionSettings transitionSettings;

    private void Start()
    {
        TransitionManager.Instance().Transition("StartingScreen", transitionSettings, 5);
        AudioManager.instance.PlayMenuMusic();
    }
}
