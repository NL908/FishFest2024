using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : Item
{
    [SerializeField]
    private float currencyGained = 1;
    protected override void HandleCollision(Collider2D collision)
    {
        playerManager.IncreaseMaxHP(currencyGained);
        if (AudioManager.instance) AudioManager.instance.PlaySound("item_get");
    }

    protected override void CalculateVelocity()
    {

    }
}
