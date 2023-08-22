using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopButton : MonoBehaviour
{
    [SerializeField] private GameObject _shop;
    [SerializeField] private Button _shopButton;

    public void OpenShop()
    {
        _shopButton.gameObject.SetActive(false);
        _shop.SetActive(true);
    }

    public void CloseShop()
    {
        _shopButton.gameObject.SetActive(true);
        _shop.SetActive(false);
    }
}
