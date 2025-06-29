using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAtPoint : MonoBehaviour
{

    [SerializeField] private float _pushForce = 50f;
    [SerializeField] private GameObject _bulletHolePrefab;
    [SerializeField] private float decalSize = 0.1f;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Rigidbody targetRb = hit.collider.GetComponent<Rigidbody>();
                if (targetRb != null)
                {
                    Vector3 forceDirection = ray.direction;
                    targetRb.AddForceAtPosition(forceDirection * _pushForce, hit.point, ForceMode.Impulse);
                    if (_bulletHolePrefab != null)
                    {
                        Vector3 clampedPosition = CalculateClampedPosition(hit, decalSize);
                        Quaternion rotation = Quaternion.LookRotation(-hit.normal);
                        Instantiate(_bulletHolePrefab, clampedPosition, rotation, hit.transform);
                    }
                        Debug.Log($"Colpito il cubo nel punto {hit.point}!");
                }
            }
        }
    }


    //quì mi sono arreso (recupererò la lezione di pratica)
    private Vector3 CalculateClampedPosition(RaycastHit hit, float size)
    {
        float decalRadius = size / 2f;
        Vector3 cubeExtents = hit.transform.GetComponent<MeshFilter>().mesh.bounds.extents;
        Vector3 localHitPoint = hit.transform.InverseTransformPoint(hit.point);
        Vector3 offset = Vector3.zero;

        float minX = -cubeExtents.x + decalRadius;
        float maxX = cubeExtents.x - decalRadius;
        if (localHitPoint.x < minX)
            offset.x = minX - localHitPoint.x;
        else if (localHitPoint.x > maxX)
            offset.x = maxX - localHitPoint.x;

        float minY = -cubeExtents.y + decalRadius;
        float maxY = cubeExtents.y - decalRadius;
        if (localHitPoint.y < minY)
            offset.y = minY - localHitPoint.y;
        else if (localHitPoint.y > maxY)
            offset.y = maxY - localHitPoint.y;

        float minZ = -cubeExtents.z + decalRadius;
        float maxZ = cubeExtents.z - decalRadius;
        if (localHitPoint.z < minZ)
            offset.z = minZ - localHitPoint.z;
        else if (localHitPoint.z > maxZ)
            offset.z = maxZ - localHitPoint.z;

        
        Vector3 localNormal = hit.transform.InverseTransformDirection(hit.normal);
        if (Mathf.Abs(localNormal.x) > 0.9f) offset.x = 0;
        if (Mathf.Abs(localNormal.y) > 0.9f) offset.y = 0;
        if (Mathf.Abs(localNormal.z) > 0.9f) offset.z = 0;

        
        Vector3 finalLocalPosition = localHitPoint + offset;
        Vector3 finalWorldPosition = hit.transform.TransformPoint(finalLocalPosition);
        finalWorldPosition += hit.normal * 0.001f; 

        return finalWorldPosition;
    }
}
