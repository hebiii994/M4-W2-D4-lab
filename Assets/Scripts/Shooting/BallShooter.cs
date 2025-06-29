using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _bulletForce = 2000f;
    [SerializeField] private float _sphereCastRadius = 0.5f;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;

        if (_firePoint == null)
        {
            _firePoint = transform;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.SphereCast(ray, _sphereCastRadius, out hit, 100f))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                Debug.Log("Linea di tiro bloccata da un muro!");
                return;
            }
        }
        GameObject projectile = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(ray.direction * _bulletForce);
    }
}
