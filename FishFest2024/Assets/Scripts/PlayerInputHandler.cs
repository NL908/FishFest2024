using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public float movement;

    private InputAction _moveInputAction;

    public bool jumpFlag;
    public bool isJumpPerforming;

    private void OnEnable()
    {
        _moveInputAction = InputManager.inputActions.Player.Movement;

        InputManager.inputActions.Player.JumpPress.performed += OnJumpPress;

        InputManager.inputActions.Player.JumpRelease.performed += OnJumpRelease;
    }

    private void OnDisable()
    {
        InputManager.inputActions.Player.JumpPress.performed -= OnJumpPress;

        InputManager.inputActions.Player.JumpRelease.performed -= OnJumpRelease;

        InputManager.inputActions.Player.Disable();
    }

    public void TickInput()
    {
        MoveInput();
    }

    private void MoveInput()
    {
        movement = _moveInputAction.ReadValue<float>();
    }

    private void OnJumpPress(InputAction.CallbackContext obj)
    {
        // set flag here
        throw new NotImplementedException();
    }

    private void OnJumpRelease(InputAction.CallbackContext obj)
    {
        // set flag here
        throw new NotImplementedException();
    }
}
