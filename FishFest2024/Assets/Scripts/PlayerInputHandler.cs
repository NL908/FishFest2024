using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 movement;
    public Vector2 mouseMovement;

    private InputAction _moveInputAction;
    private Vector2 startMousePos;
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
        //TODO: See if we can use this instead of Input,mousePosition
        movement = _moveInputAction.ReadValue<Vector2>();
    }

    private void OnJumpPress(InputAction.CallbackContext obj)
    {
        // Record mouse position on press
        startMousePos = Input.mousePosition;
    }

    private void OnJumpRelease(InputAction.CallbackContext obj)
    {
        // Compute the mouse movement delta
        Vector2 endMousePos = Input.mousePosition;
        mouseMovement = endMousePos - startMousePos;
    }

    public void ResetMouseMovement()
    {
        mouseMovement = Vector2.zero;
    }
}
