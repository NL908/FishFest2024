using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    PlayerInputHandler playerInputHandler;
    PlayerLocomotion playerLocomotion;

    //[Header("Player Flags")]

    private void Awake()
    {
        instance = this;
        playerInputHandler = GetComponent<PlayerInputHandler>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        playerInputHandler.TickInput();
    }

    private void FixedUpdate()
    {
        Locomotions();
    }

    private void Locomotions()
    {
        
    }
}
