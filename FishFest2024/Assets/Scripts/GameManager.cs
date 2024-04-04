using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    protected PlayerManager playerManager;
    public GameObject startingZone;
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        InputManager.ToggleActionMap(InputManager.inputActions.Player);
        playerManager = PlayerManager.instance;
        // TODO: Add cursor lock and cursor focus
    }

    public void GameStart() {
        playerManager.GameStart();
        startingZone.SetActive(false);
    }
}
