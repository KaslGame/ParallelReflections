using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ghost : MonoBehaviour
{
    public event UnityAction<Ghost> GhostSpawned;

    private void OnEnable()
    {
        GhostSpawned?.Invoke(this);
    }
}
