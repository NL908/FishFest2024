using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public float bonusHP;
    public float jumpHPReduction;
    public float currency;
    public float jumpDistanceMultiplier;
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
    public void SavePlayerData(float currency, float bonusHP, float jumpHPReduction, float jumpDistanceMultiplier)
    {
        this.currency = currency;
        this.bonusHP = bonusHP;
        this.jumpHPReduction = jumpHPReduction;
        this.jumpDistanceMultiplier = jumpDistanceMultiplier;
    }
}
