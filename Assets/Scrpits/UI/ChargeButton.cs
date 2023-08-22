using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private Obelisk _currentObelisk;

    public void Charge()
    {
        _currentObelisk.Charged();
        _button.gameObject.SetActive(false);
    }

    private void OnObeliskEnter(Obelisk obelisk, bool isEnter)
    {
        if (_currentObelisk.IsCharge == false)
            _button.gameObject.SetActive(isEnter);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Obelisk obelisk))
        {
            _currentObelisk = obelisk;
            _currentObelisk.ObeliskEnter += OnObeliskEnter;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Obelisk obelisk))
        {
            _currentObelisk.ObeliskEnter -= OnObeliskEnter;
            _currentObelisk = null;
        }
    }
}
