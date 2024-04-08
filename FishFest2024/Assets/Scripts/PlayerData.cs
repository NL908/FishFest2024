using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public float bonusHP;
    public float jumpHPReduction;
    public float currency;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePlayerData(float currency, float bonusHP, float jumpHPReduction)
    {
        this.currency = currency;
        this.bonusHP = bonusHP;
        this.jumpHPReduction = jumpHPReduction;
    }
}
