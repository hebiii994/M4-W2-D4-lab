using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    [SerializeField] private int _numberOfCubes = 5;
    [SerializeField] private float _spacing = 2.5f;
    [SerializeField] private Vector3 _startPosition;

    private void Awake()
    {
        if (_cubePrefab == null)
        {
            Debug.LogError("Assegna il Prefab del cubo allo script Instantiator!");
            return;
        }
    }
    void Start()
    {
        GenerateCubes();
    }

    private void GenerateCubes()
    {
        for (int i = 0; i < _numberOfCubes; i++)
        {
            Vector3 position = _startPosition + new Vector3(i * _spacing, 0, 0);
            Instantiate(_cubePrefab, position, Quaternion.identity);
        }
    }
}
