using UnityEngine;

public class NPCVision : MonoBehaviour
{
    [SerializeField] private Transform _visionPoint;

    [SerializeField] private LayerMask _layerMask = -1;

    [SerializeField] private float _angleOfView = 30f;

    public Transform VisionPoint => _visionPoint;

    private GameObject _target;

    private Collider _targetCollider;

    public void SetTarget(GameObject target)
    {
        _target = target;
        _target.TryGetComponent<Collider>(out _targetCollider);
    }

    public bool GetTargetRayCollision(out Vector3 hitPos, float maxDistance)
    {
        hitPos = _visionPoint.position;

        if (_target == null)
        {
            return false;
        }

        Vector3 targetPoint = (_targetCollider == null) ? _target.transform.position : _targetCollider.bounds.center;
        Vector3 direction = targetPoint - _visionPoint.position;

        if (Vector3.Angle(new Vector3(direction.x, 0, direction.z), _visionPoint.forward) > _angleOfView)
        {
            return false;
        }

        if (Physics.Raycast(_visionPoint.position, direction, out var hit, maxDistance, _layerMask))
        {
            if (hit.collider.gameObject != _target)
            {
                return false;
            }
            hitPos = hit.point;
            return true;
        }

        return false;
    }
}
