using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform _base;

    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float _length = 0.1f;

    public bool CheckGround()
    {
        bool hit = Physics.Raycast(_base.position, -_base.up, _length, _layerMask);
        return hit;
    }
}
