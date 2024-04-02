using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    PlayerInputHandler playerInputHandler;
    PlayerLocomotion playerLocomotion;

    AimCircleController aimCircleController;

    // Player Attributes
    public int hp = 10;
    public int maxHP = 10;

    // HUD object
    public Text HPText;
    public GameObject HPBar;

    //[Header("Player Flags")]
    public bool isAiming;
    public bool isAimTriggered;

    private void Awake()
    {
        instance = this;
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
        aimCircleController = GetComponentInChildren<AimCircleController>();

        HPText.text = hp.ToString() + "/" + maxHP.ToString();
    }

    private void Update()
    {
        playerInputHandler.TickInput();
        // Handle Aim Circle
        HandleAimCircle();
    }

    private void FixedUpdate()
    {
        // Perform movements
        Locomotions();
        
        // Reset
        playerInputHandler.ResetMouseMovement();
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
            // Aim is triggered
            aimCircleController.EnableAim();
            Time.timeScale = 0.2f;
        }
        if (isAiming)
        {
            aimCircleController.updateAimDirection(playerInputHandler.aimDirection);
        }
        else
        {
            aimCircleController.DisableAim();
            Time.timeScale = 1f;
        }
    }

    private void ResetFlags()
    {
        isAimTriggered = false;
    }

    public void ChangeHPForMove()
    {
        ChangeHP(hp - 1);
    }

    private void ChangeHP(int newHP)
    {
        hp  = newHP;
        // Update the HP UI
        HPText.text = hp.ToString() + "/" + maxHP.ToString();
        RectTransform[] HPBarChildren = HPBar.GetComponentsInChildren<RectTransform>();
        RectTransform border = HPBarChildren[0];
        RectTransform fill = HPBarChildren[1];
        fill.sizeDelta = new Vector2(hp * 1f / maxHP * border.sizeDelta.x, fill.sizeDelta.y);
        // HP detection
        if (hp <= 0)
        {
            // Game Over
            Debug.Log("Game Over");
        }
    }
}
