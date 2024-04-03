using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject _shopCanvas;

    private Collider2D _collider;
    private ShopCanvasScript shopCanvasScript;

    private bool isShopOpened = false;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        shopCanvasScript = _shopCanvas.GetComponent<ShopCanvasScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!shopCanvasScript.isShopOpen && collision.gameObject.layer == 3)
        {
            shopCanvasScript.OpenShop();
        }
    }
}
