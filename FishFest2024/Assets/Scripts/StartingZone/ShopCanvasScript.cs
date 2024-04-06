using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCanvasScript : MonoBehaviour
{
    public bool isShopOpen;
    public GameObject CurrencyText;

    private PlayerManager playerManager;
    void Start()
    {
        playerManager = PlayerManager.instance;    
    }

    public void UpdateCurrency()
    {
        TextMesh text = CurrencyText.GetComponent<TextMesh>();
        text.text = playerManager.currency.ToString();
    }
    public void OpenShop()
    {
        Debug.Log("Shop Open");
        isShopOpen = true;
        gameObject.SetActive(true);
        InputManager.inputActions.Player.Disable();
        UpdateCurrency();
    }

    public void CloseShop()
    {
        Debug.Log("Shop Close");
        isShopOpen = false;
        gameObject.SetActive(false);
        InputManager.inputActions.Player.Enable();
    }

    public void PurchaseItemHealthBoost()
    {
        float price = 10;
        float amount = 10;
        if (playerManager.currency < price)
        {
            // TODO: show warning?
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
            // TODO: show warning?
        }
        else
        {
            playerManager.ChangeCurrency(playerManager.currency - price);
            playerManager.ReduceJumpCost(amount);
            UpdateCurrency();
        }
    }

    public void PurchaseItemBoost()
    {
        UpdateCurrency();

    }
}
