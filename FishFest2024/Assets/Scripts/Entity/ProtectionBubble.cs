using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionBubble : Item
{
    [SerializeField]
    private float maxHPIncreased = 5;
    protected override void HandleCollision(Collider2D collision)
    {
        playerManager.ActivateProtectionBubble();
        if (AudioManager.instance) AudioManager.instance.PlaySound("item_get");
    }

    protected override void CalculateVelocity()
    {
        
    }
}
