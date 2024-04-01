using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerControl inputActions;
    public static event Action<InputActionMap> actionMapChange;
    private void Awake()
    {
        inputActions = new PlayerControl();
        Debug.Log("Init");
    }

    public static void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled)
            return;

        inputActions.Disable();
        actionMapChange?.Invoke(actionMap);
        actionMap.Enable();
    }
}
