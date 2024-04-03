using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField]
    private float hpDeleptionRate = 0.1f;

    // HUD object
    public Text HPText;
    public GameObject HPBar;

    [Header("Player Flags")]
    public bool isAiming;
    public bool isAimTriggered;
    public bool isJumpPerformed; // True when jump is performed
    public bool isActive = false;

    public void InitailizePlayer()
    {
        ChangeHP(maxHP);
        isActive = true;
    }

    private void Awake()
    {
        instance = this;
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
        aimCircleController = GetComponentInChildren<AimCircleController>();
        InitailizePlayer();
    }

    private void Update()
    {
        // Gather Inputs
        playerInputHandler.TickInput();
        // Handle Aim Circle
        HandleAimCircle();

        // Reduce HP with time
        if (isActive)
        {
            HandleHPDepletion();
        }
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
            playerLocomotion.HandleJump(playerInputHandler.aimDirection);

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
    public void ChangeHP(float newHP)
    {
        hp  = Mathf.Clamp(newHP, 0, maxHP);
        // Update the HP UI
        HPText.text = System.Math.Round(hp, 1).ToString() + "/" + System.Math.Round(maxHP, 1).ToString();
        RectTransform[] HPBarChildren = HPBar.GetComponentsInChildren<RectTransform>();
        RectTransform border = HPBarChildren[0];
        RectTransform fill = HPBarChildren[1];
        fill.sizeDelta = new Vector2(hp / maxHP * border.sizeDelta.x, fill.sizeDelta.y);
        // HP detection
        if (hp <= 0f)
        {
            // Game Over
            Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnBecameInvisible()
    {
        Debug.Log("Game Over due to fall off screen");
        // TODO: Add a black screen fade out animation before reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HandleFishCollision(float healthGained)
    {
        ChangeHP(hp + healthGained);
    }
    public void HandleMineCollision(float healthLost)
    {
        ChangeHP(hp - healthLost);
    }
    public void HandleHPDepletion()
    {
        ChangeHP(hp - Time.deltaTime * hpDeleptionRate);
    }

    public void ChangeVelocity(Vector2 velocity)
    {
        playerLocomotion.ChangeVelocity(velocity);
    }
}
