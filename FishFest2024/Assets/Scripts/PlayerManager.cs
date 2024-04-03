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
    [SerializeField]
    private float hp;
    [SerializeField]
    private float maxHP = 20;

    // HUD object
    public Text HPText;
    public GameObject HPBar;

    [Header("Player Flags")]
    public bool isAiming;
    public bool isAimTriggered;
    public bool isJumpPerformed; // True when jump is performed

    private void Awake()
    {
        instance = this;
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
        aimCircleController = GetComponentInChildren<AimCircleController>();

        ChangeHP(maxHP);
    }

    private void Update()
    {
        // Gather Inputs
        playerInputHandler.TickInput();
        // Handle Aim Circle
        HandleAimCircle();
    }

    private void FixedUpdate()
    {
        // Perform movements
        HandleJump();
        
        // Reset
        ResetInputs();
    }

    private void HandleJump()
    {
        if(isJumpPerformed)
        {
            // Call Locomotion to perform jump locomotion
            playerLocomotion.HandleMovement(playerInputHandler.mouseMovement);

            // Decrese HP when jump
            ChangeHP(hp - 1f);
        }
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

    private void ResetInputs()
    {
        isAimTriggered = false;
        isJumpPerformed = false;
    }
    private void ChangeHP(float newHP)
    {
        hp  = Mathf.Clamp(newHP, 0, maxHP);
        // Update the HP UI
        HPText.text = hp.ToString() + "/" + maxHP.ToString();
        RectTransform[] HPBarChildren = HPBar.GetComponentsInChildren<RectTransform>();
        RectTransform border = HPBarChildren[0];
        RectTransform fill = HPBarChildren[1];
        fill.sizeDelta = new Vector2(hp / maxHP * border.sizeDelta.x, fill.sizeDelta.y);
        // HP detection
        if (hp <= 0f)
        {
            // Game Over
            Debug.Log("Game Over");
        }
    }

    public void HandleFishCollision()
    {
        ChangeHP(hp + 1f);
    }
}
