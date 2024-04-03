using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Collidable
{
    [SerializeField]
    private float healthGained;

    protected override void HandleCollision()
    {
        playerManager.HandleFishCollision();
    }

    protected override void PlayDeathAnimation()
    {
        Debug.Log("Play Animation");
        // TODO: add death animation for fish
    }
}
