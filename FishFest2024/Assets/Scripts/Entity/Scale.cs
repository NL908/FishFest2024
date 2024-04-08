using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : Item
{
    [SerializeField] float minCurrencyGained = 20;
    [SerializeField] float maxCurrencyGained = 50;
    protected override void HandleCollision(Collider2D collision)
    {
        float currencyGained = Mathf.Round(Random.Range(minCurrencyGained, maxCurrencyGained));
        playerManager.IncreaseCurrency(currencyGained);
        if (AudioManager.instance) AudioManager.instance.PlaySound("item_get");
    }

    protected override void CalculateVelocity()
    {

    }
}
