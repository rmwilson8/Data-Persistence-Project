using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private MainManager _mainManager;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        _mainManager.GameOver();
    }
}
