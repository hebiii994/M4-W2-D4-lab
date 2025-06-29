using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    [SerializeField] private float _lifetime = 5f;
    private void Awake()
    {
        Destroy(gameObject, _lifetime);
    }
}
