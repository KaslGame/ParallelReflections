using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button _shopButton;
    [SerializeField] private GameObject _UIShop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            _shopButton.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _shopButton.gameObject.SetActive(false);

            if (_UIShop.activeSelf)
                _UIShop.gameObject.SetActive(false);
        }
    }
}
