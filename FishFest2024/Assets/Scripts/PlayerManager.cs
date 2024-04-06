using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private const float DefaultHP = 20;
    [SerializeField]
    private float hp;
    [SerializeField]
    private float maxHP = DefaultHP;
    [SerializeField]
    private float hpDeleptionRate = 0.1f;
    [SerializeField]
    private float bonusHP = 0;
    [SerializeField]
    private float jumpHPCost = 1;
    [SerializeField]
    private float jumpHPReduction = 0;
    public float currency = 0;

    // HUD object
    public TMP_Text HPText;
    public GameObject HPBar;
    public TMP_Text currencyText;

    [Header("Player Flags")]
    public bool isAiming;
    public bool isAimTriggered;
    public bool isJumpPerformed; // True when jump is performed
    public bool isControllable = false;
    public bool isHealthDepleting = false;

    private void Awake()
    {
        instance = this;
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
        aimCircleController = GetComponentInChildren<AimCircleController>();
        isControllable = true;
        IntializePlayerStats();
    }

    private void Update()
    {
        // Gather Inputs
        playerInputHandler.TickInput();
        HandleAimCircle();
        HandleHPDepletion();
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
        if (isJumpPerformed)
        {
            // Call Locomotion to perform jump locomotion
            playerLocomotion.HandleJump(playerInputHandler.aimDirection);

            // Decrese HP when jump
            if (isHealthDepleting)
            {
                float cost = jumpHPCost - jumpHPReduction;
                ChangeHP(hp - cost);
            }
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
            if (!PauseMenu.instance.isPaused)
            {
                Time.timeScale = 1f;
            }
        }
    }

    private void ResetInputs()
    {
        isAimTriggered = false;
        isJumpPerformed = false;
    }
    public void ChangeHP(float newHP)
    {
        hp = Mathf.Clamp(newHP, 0, maxHP);
        // Update the HP UI
        HPText.text = System.Math.Round(hp, 1).ToString() + "/" + System.Math.Round(maxHP, 1).ToString();
        RectTransform[] HPBarChildren = HPBar.GetComponentsInChildren<RectTransform>();
        RectTransform border = HPBarChildren[0];
        RectTransform fill = HPBarChildren[1];
        fill.sizeDelta = new Vector2(hp / maxHP * border.sizeDelta.x, fill.sizeDelta.y);
        // HP detection
        if (hp <= 0f)
        {
            InputManager.inputActions.Player.Disable();
            isControllable = false;
            isHealthDepleting = false;
        }
        else
        {
            if (GameManager.instance.isGameActive) {
                InputManager.inputActions.Player.Enable();
                isControllable = true;
                isHealthDepleting = true;
            }
        }
    }

    private void OnBecameInvisible()
    {
        GameOver("Game Over due to fall off screen");
    }

    public void HandleFishCollision(float healthGained, float currencyGained)
    {
        ChangeHP(hp + healthGained);
        ChangeCurrency(currency + currencyGained);

    }
    public void HandleMineCollision(float healthLost)
    {
        ChangeHP(hp - healthLost);
    }
    public void HandleHPDepletion()
    {
        if (isHealthDepleting)
        {
            ChangeHP(hp - Time.deltaTime * hpDeleptionRate);
        }
    }

    public void ChangeVelocity(Vector2 velocity)
    {
        playerLocomotion.ChangeVelocity(velocity);
    }


    public void GameOver(string reason)
    {
        Debug.Log(reason);
        isControllable = false;
        isHealthDepleting = false;
        InputManager.inputActions.Player.Disable();
        Time.timeScale = 0;
        // TODO: enter transition animation, and save any persistent stats
        GameManager.instance.GameOver();
    }

    public void GameStart()
    {
        isControllable = true;
        isHealthDepleting = true;
    }

    public void IntializePlayerStats()
    {
        ChangeHP(maxHP);
        ChangeCurrency(currency);
    }

    public void PauseGame()
    {
        isAiming = false;
        PauseMenu.instance.Pause();
    }

    // Player Attributes Related
    public void IncreaseMaxHP(float maxHPIncreased)
    {
        maxHP += maxHPIncreased;
        ChangeHP(hp + maxHPIncreased);
    }
    public void IncreaseCurrency(float currencyGained)
    {
        currency += currencyGained;
        ChangeCurrency(currency);
    }
    public void ReduceJumpCost(float amount)
    {
        jumpHPReduction += amount;
    }
    public void IncreaseBonusHP(float amount)
    {
        bonusHP += amount;
        maxHP += amount;
        ChangeHP(hp + amount);
    }
    public void ChangeCurrency(float newAmount)
    {
        currency = Mathf.Clamp(newAmount, 0f, 999f);
        currencyText.text = currency.ToString();
    }
}
