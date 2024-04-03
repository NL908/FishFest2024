using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Collidable
{
    [SerializeField]
    private float healthLost;

    protected override void HandleCollision()
    {
        playerManager.HandleMineCollision(healthLost);
    }

    protected override void HandleDeath()
    {
        // TODO: Play Death particle and sound effect
        Debug.Log("BOOM DESU");
    }
}
