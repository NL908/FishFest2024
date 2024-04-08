using Cinemachine;
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
    PlayerData playerData;

    AimCircleController aimCircleController;

    [SerializeField]
    private ParticleSystem _jumpParticle;

    // Player Attributes
    [SerializeField]
    private float DefaultHP = 100;
    [SerializeField]
    private float hp;
    [SerializeField]
    private float maxHP;
    [SerializeField]
    private float hpDeleptionRate = 1f;
    [SerializeField]
    private float bonusHP = 0;
    [SerializeField]
    private float jumpHPCost = 5;
    [SerializeField]
    private float jumpHPReduction = 0;
    public float currency = 0;
    [SerializeField] float jumpDistanceMultiplier = 1.0f;

    private float protectionStartTime = 10;
    private float protectionTimer = 0;

    // Multplier
    [SerializeField] float startHPDepletionMul = 2.0f;
    [SerializeField] float endHPDepletionMult = 1.0f;

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
    public bool isGameOverIfFallOffScreen = false;
    public bool isShaking = false;


    private void Awake()
    {
        instance = this;
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerLocomotion = GetComponentInChildren<PlayerLocomotion>();
        aimCircleController = GetComponentInChildren<AimCircleController>();
        isControllable = true;
    }

    private void Start()
    {
        playerData = PlayerData.instance;
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
            playerLocomotion.HandleJump(playerInputHandler.aimDirection, jumpDistanceMultiplier);

            // Play jump audio
            if (AudioManager.instance) AudioManager.instance.PlaySound("jump");

            _jumpParticle.Play();

            // Decrese HP when jump
            if (isHealthDepleting && !isProtectionBubble)
            {
                float cost = jumpHPCost - jumpHPReduction;
                ChangeHP(hp - Mathf.Max(0.1f, cost));
            }

            // Play bubble particle
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
            if (!PauseMenu.instance.isPaused && !GameManager.instance.isPlayWin)
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
        Image[] hpImages = HPBar.GetComponentsInChildren<Image>();
        HPText.text = System.Math.Round(hp, 1).ToString() + "/" + System.Math.Round(maxHP, 1).ToString();
        Image fill = hpImages[1];
        fill.fillAmount = hp / maxHP;
        // HP detection
        if (hp <= 0f)
        {
            isAiming = false;
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
        if (isGameOverIfFallOffScreen)
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
        //GameManager.instance.TriggerScreenShake();
        StartCoroutine(GameManager.instance.TriggerVignette());
    }

    public void HandleHPDepletion()
    {
        if (isHealthDepleting && !isProtectionBubble)
        {
            float factor = GetDepthFactor();
            float mult = startHPDepletionMul * (1 - factor) + endHPDepletionMult * factor;
            ChangeHP(hp - Time.deltaTime * hpDeleptionRate * mult);
        }
    }

    public void ChangeVelocity(Vector2 velocity)
    {
        playerLocomotion.ChangeVelocity(velocity);
    }


    public void GameOver(string reason)
    {
        AudioManager.instance.PlaySound("player_fall");
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
        bonusHP = playerData.bonusHP;
        jumpHPReduction = playerData.jumpHPReduction;
        currency = playerData.currency;
        maxHP = DefaultHP + bonusHP;
        jumpDistanceMultiplier = playerData.jumpDistanceMultiplier;
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
        jumpHPReduction = Mathf.Clamp(jumpHPReduction + amount, 0, jumpHPCost - 0.1f);
    }
    public void IncreaseBonusHP(float amount)
    {
        if (bonusHP < DefaultHP)
        {
            bonusHP += amount;
            maxHP += amount;
            ChangeHP(hp + amount);
        }
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

    private void OnDestroy()
    {
        playerData.SavePlayerData(currency, bonusHP, jumpHPReduction, jumpDistanceMultiplier);
    }

    public float GetDepthFactor()
    {
        // 0 at bottom, 1 at top
        return Mathf.Max(0.0f, transform.position.y) / GameManager.instance.oceanDepth;
    }

    public void IncreaseJumpDistance(float additionalPercent)
    {
        jumpDistanceMultiplier = Mathf.Clamp(jumpDistanceMultiplier + additionalPercent, 1f, 2f);
    }
}
