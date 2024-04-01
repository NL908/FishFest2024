using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        InputManager.ToggleActionMap(InputManager.inputActions.Player);

        // TODO: Add cursor lock and cursor focus
    }
}
