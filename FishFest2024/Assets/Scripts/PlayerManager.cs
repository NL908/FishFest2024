using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    PlayerInputHandler playerInputHandler;
    PlayerLocomotion playerLocomotion;

    AimCircleController aimCircleController;

    //[Header("Player Flags")]
    public bool isAiming;
    public bool isAimTriggered;

    private void Awake()
    {
        instance = this;
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
        aimCircleController = GetComponentInChildren<AimCircleController>();
    }

    private void Update()
    {
        playerInputHandler.TickInput();
    }

    private void FixedUpdate()
    {
        Locomotions();
        playerInputHandler.ResetMouseMovement();

        HandleAimCircle();

        ResetFlags();
    }

    private void Locomotions()
    {
        playerLocomotion.HandleMovement(playerInputHandler.mouseMovement);
    }

    private void HandleAimCircle()
    {
        if (isAimTriggered)
        {
            aimCircleController.EnableAim();
        }
        if (isAiming)
        {
            aimCircleController.updateAimDirection(playerInputHandler.aimDirection);
        }
        else
        {
            aimCircleController.DisableAim();
        }
    }

    private void ResetFlags()
    {
        isAimTriggered = false;
    }
}
