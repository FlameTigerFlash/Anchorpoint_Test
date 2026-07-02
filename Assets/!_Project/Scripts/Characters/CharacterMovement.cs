using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    private Rigidbody _rb;

    private Vector2 _direction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 up = transform.up;

        Vector3 newVelocity = right * _direction.x * _speed + up * _rb.linearVelocity.y + forward * _direction.y * _speed;
        _rb.linearVelocity = newVelocity;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
    }
}
