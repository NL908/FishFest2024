using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingLineScript : MonoBehaviour
{
    public GameObject startingZone;
    protected PlayerManager playerManager;

    void Start()
    {
        playerManager = PlayerManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            GameManager.instance.GameStart();
        }
    }
}
