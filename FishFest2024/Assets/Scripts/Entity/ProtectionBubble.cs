using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionBubble : Item
{
    protected override void HandleCollision(Collider2D collision)
    {
        playerManager.ActivateProtectionBubble();
        if (AudioManager.instance) AudioManager.instance.PlaySound("item_get");
    }

    protected override void CalculateVelocity()
    {
        
    }
}
