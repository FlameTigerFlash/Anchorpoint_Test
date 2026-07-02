using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpAbility : BaseDiscreteAbility
{
    [SerializeField] private GroundChecker _groundChecker;

    [SerializeField] private float _jumpAcceleration = 5f;

    private Rigidbody _rb;


    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody>();
    }

    public override bool TryPerform()
    {
        if (!_groundChecker.CheckGround())
        {
            return false;
        }
        return base.TryPerform();
    }

    protected override void Perform()
    {
        _rb.AddForce(transform.up * _jumpAcceleration, ForceMode.VelocityChange);
    }
}
