using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SphereOneShotDetector : MonoBehaviour
{
    [SerializeField] private Transform _center;

    [SerializeField] private LayerMask _layerMask = -1;

    [SerializeField] private float _radius = 5f;

    [SerializeField] private bool _seeThroughWalls = false;

    public UnityEvent<GameObject> ObjectDetectedEvent;

    public void Detect()
    {
        if (_center == null)
        {
            _center = transform;
        }

        Collider[] colliders = Physics.OverlapSphere(_center.position, _radius, _layerMask);

        if (_seeThroughWalls == false)
        {
            colliders = FilterVisibleColliders(colliders).ToArray();
        }

        HashSet<GameObject> gameObjects = new HashSet<GameObject>();
        foreach (Collider collider in colliders)
        {
            gameObjects.Add(collider.gameObject);
        }

        foreach (GameObject obj in gameObjects)
        {
            ObjectDetectedEvent.Invoke(obj);
        }
    }

    private List<Collider> FilterVisibleColliders(Collider[] colliders)
    {
        List<Collider> visibleColliders = new List<Collider>();
        foreach (Collider targetCollider in colliders)
        {
            Vector3 colliderCenter = targetCollider.bounds.center;
            Vector3 direction = (colliderCenter - _center.position).normalized;

            Ray ray = new Ray(_center.position, direction);
            if (Physics.Raycast(ray, out var hit, Vector3.Distance(colliderCenter, _center.position), _layerMask) &&
                hit.collider.gameObject != gameObject &&
                hit.collider.gameObject != targetCollider.gameObject)
            {
                continue;
            }
            visibleColliders.Add(targetCollider);
        }
        return visibleColliders;
    }
}
