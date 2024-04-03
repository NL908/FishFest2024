using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPill : Item
{
    [SerializeField]
    private float maxHPIncreased;
    protected override void HandleCollision(Collider2D collision)
    {
        playerManager.IncreaseMaxHP(maxHPIncreased);
    }
}
