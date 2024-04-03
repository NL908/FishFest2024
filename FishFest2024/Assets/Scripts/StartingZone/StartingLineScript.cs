using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingLineScript : MonoBehaviour
{
    public GameObject startingZone;
    public GameObject shopCanvas;
    protected PlayerManager playerManager;

    void Start()
    {
        playerManager = PlayerManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            playerManager.GameStart();
            startingZone.SetActive(false);
            shopCanvas.SetActive(false);
        }
    }
}
