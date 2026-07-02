using System.Collections;
using UnityEngine;

public class ShootAbility : BaseDiscreteAbility
{
    [SerializeField] private GameObject _projectilePrefab;

    [SerializeField] private Transform _shootingPoint;

    [SerializeField] private float _shootingAcceleration = 10f;

    protected override void Perform()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _shootingPoint.position, _shootingPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(_shootingPoint.forward * _shootingAcceleration, ForceMode.VelocityChange);
        }
        else
        {
            Debug.LogWarning("Projectile object should have a Rigid Body component.");
        }
    }
}
