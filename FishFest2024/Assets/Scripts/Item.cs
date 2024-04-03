using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : Collidable
{
    protected override void HandleDeath()
    {
        // TODO: Play Death particle and sound effect
        Debug.Log("DEATH DESU");
    }
}
