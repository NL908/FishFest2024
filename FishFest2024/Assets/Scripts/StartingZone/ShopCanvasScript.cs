using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCanvasScript : MonoBehaviour
{
    public bool isShopOpen;
    public void OpenShop()
    {
        Debug.Log("Shop Open");
        isShopOpen = true;
        gameObject.SetActive(true);
        InputManager.inputActions.Player.Disable();
    }

    public void CloseShop()
    {
        Debug.Log("Shop Close");
        isShopOpen = false;
        gameObject.SetActive(false);
        InputManager.inputActions.Player.Enable();
    }
}
