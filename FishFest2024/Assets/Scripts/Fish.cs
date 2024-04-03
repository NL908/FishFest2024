using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Collidable
{
    [SerializeField]
    private float healthGained;

    protected override void HandleCollision(Collider2D collision)
    {
        playerManager.HandleFishCollision(healthGained);
    }

    protected override void HandleDeath()
    {
        // TODO: Play Death particle and sound effect
        Debug.Log("DEATH DESU");
    }
}
