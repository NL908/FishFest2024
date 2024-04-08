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

    private float protectionStartTime = 10;
    private float protectionTimer = 0;

    // HUD object
    public TMP_Text HPText;
    public TMP_Text currencyText;
    public GameObject HPBar;

    [Header("Player Flags")]
    public bool isAiming;
    public bool isAimTriggered;
    public bool isJumpPerformed; // True when jump is performed
    public bool isControllable = false;
    public bool isHealthDepleting = false;
    public bool isProtectionBubble = false;


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
        HandleProtectionItem();
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
            if (isHealthDepleting && !isProtectionBubble)
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

    public void HandleProtectionItem()
    {
        if (isProtectionBubble)
        {
            if (protectionTimer > 0)
            {
                protectionTimer -= Time.deltaTime;
            }
            if (protectionTimer <= 0)
            {
                isProtectionBubble = false;
                protectionTimer = 0;
                ToggleHPBubble(false);
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
        RectTransform[] hpRects = HPBar.GetComponentsInChildren<RectTransform>();
        RectTransform border = hpRects[0];
        RectTransform fill = hpRects[1];
        HPText.text = System.Math.Round(hp, 1).ToString() + "/" + System.Math.Round(maxHP, 1).ToString();
        fill.sizeDelta = new Vector2(hp / maxHP * border.sizeDelta.x, fill.sizeDelta.y);
        // HP detection
        if (hp <= 0f)
        {
            isControllable = false;
            isHealthDepleting = false;
        }
        else
        {
            if (GameManager.instance.isGameActive)
            {
                isControllable = true;
                isHealthDepleting = true;
            }
        }
    }

    private void OnBecameInvisible()
    {
        GameOver("Game Over due to fall off screen");
    }

    private void ToggleHPBubble(bool status)
    {
        GameObject hpBubble = HPBar.transform.GetChild(2).gameObject;
        hpBubble.SetActive(status);
    }

    public void HandleFishCollision(float healthGained, float currencyGained)
    {
        ChangeHP(hp + healthGained);
        ChangeCurrency(currency + currencyGained);

    }
    public void HandleMineCollision(float healthLost)
    {
        if (!isProtectionBubble)
        {
            ChangeHP(hp - healthLost);
        }
    }
    public void HandleHPDepletion()
    {
        if (isHealthDepleting && !isProtectionBubble)
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

    public void ActivateProtectionBubble()
    {
        isProtectionBubble = true;
        protectionTimer = protectionStartTime; 
        ToggleHPBubble(true);
    }
}
