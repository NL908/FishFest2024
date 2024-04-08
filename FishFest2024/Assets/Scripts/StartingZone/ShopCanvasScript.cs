using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopCanvasScript : MonoBehaviour
{
    public bool isShopOpen;
    public TMP_Text currencyText;
    public GameObject warningPanel;
    public SpriteRenderer shopSprite;
    public Sprite shopOpenSprite;
    public Sprite shopCloseSprite;

    private PlayerManager playerManager;
    void Awake()
    {
        playerManager = PlayerManager.instance;
    }

    public void UpdateCurrency()
    {
        currencyText.text = playerManager.currency.ToString();
    }
    public void OpenShop()
    {
        Debug.Log("Shop Open");
        isShopOpen = true;
        gameObject.SetActive(true);
        shopSprite.sprite = shopOpenSprite;
        playerManager.isAiming = false;
        InputManager.inputActions.Player.Disable();
        UpdateCurrency();
    }

    public void CloseShop()
    {
        Debug.Log("Shop Close");
        isShopOpen = false;
        gameObject.SetActive(false);
        warningPanel.SetActive(false);
        shopSprite.sprite = shopCloseSprite;
        InputManager.inputActions.Player.Enable();
    }

    public void ShowWarning()
    {
        if (!warningPanel.activeSelf) {
            warningPanel.SetActive(true);
            StartCoroutine(HideWarningAfterDelay(3f));
        }
    }

    IEnumerator HideWarningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        warningPanel.SetActive(false);
    }

    public void PurchaseItemHealthBoost()
    {
        float price = 10;
        float amount = 10;
        if (playerManager.currency < price)
        {
            ShowWarning();
        }
        else
        {
            playerManager.ChangeCurrency(playerManager.currency - price);
            playerManager.IncreaseBonusHP(amount);
            UpdateCurrency();
        }
    }

    public void PurchaseItemJumpCostReduction()
    {
        float price = 10;
        float amount = 0.1f;
        if (playerManager.currency < price)
        {
            ShowWarning();
        }
        else
        {
            playerManager.ChangeCurrency(playerManager.currency - price);
            playerManager.ReduceJumpCost(amount);
            UpdateCurrency();
        }
    }
}
