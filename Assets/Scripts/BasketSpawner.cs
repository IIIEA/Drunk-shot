using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketSpawner : MonoBehaviour
{
    private Camera _mainCamera;
    private float _xBorderCoordinates;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }
}
