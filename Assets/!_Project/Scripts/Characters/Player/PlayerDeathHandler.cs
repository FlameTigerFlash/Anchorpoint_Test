using System;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private Vector3 _knockbackPositionOffset = new Vector3(0, 1, 0);

    [SerializeField] private float _horizontalKnockbackAcceleration = 4f;
    [SerializeField] private float _verticalKnockbackAcceleration = 1f;

    public void HandleDeath()
    {
        _rb.freezeRotation = false;

        Vector2 horizontalAcceleration = UnityEngine.Random.insideUnitCircle * _horizontalKnockbackAcceleration;

        Vector3 knockbackVector = new Vector3(horizontalAcceleration.x, _verticalKnockbackAcceleration, horizontalAcceleration.y);

        _rb.AddForceAtPosition(knockbackVector, _knockbackPositionOffset, ForceMode.VelocityChange);
    }
}
