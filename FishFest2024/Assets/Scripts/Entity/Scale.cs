using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : Item
{
    [SerializeField]
    private float minCurrencyGained = 5;
    private float maxCurrencyGained = 10;
    protected override void HandleCollision(Collider2D collision)
    {
        float currencyGained = Mathf.Round(Random.Range(minCurrencyGained, maxCurrencyGained));
        playerManager.IncreaseMaxHP(currencyGained);
        if (AudioManager.instance) AudioManager.instance.PlaySound("item_get");
    }

    protected override void CalculateVelocity()
    {

    }
}
