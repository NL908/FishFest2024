using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingLineScript : MonoBehaviour
{
    public GameObject startingZone;
    protected PlayerManager playerManager;

    public bool isActive = false;

    void Start()
    {
        playerManager = PlayerManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.gameObject.layer == 3)
        {
            isActive = false;
            GameManager.instance.GameStart();
        }
    }
}
