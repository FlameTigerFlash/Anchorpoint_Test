using UnityEngine;

public class PlayerLookRotate : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private float _mouseSensitivity = 1f;
    [SerializeField] private float _maxVerticalAngle = 80f;

    private Rigidbody _rb;

    private Vector2 _lookInput = Vector2.zero;

    private float _verticalRotation = 45;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _camera.tag = "MainCamera";
        HandleVerticalLook();
    }

    private void Update()
    {
        RotateTowardsCursor();
    }

    private void RotateTowardsCursor()
    {
        HandleHorizontalLook();
        HandleVerticalLook();
        _lookInput = Vector2.zero;
    }

    private void HandleHorizontalLook()
    {
        float horizontal = _lookInput.x * _mouseSensitivity;
        transform.rotation *= Quaternion.Euler(0f, horizontal, 0f);
    }

    private void HandleVerticalLook()
    {
        float vertical = -_lookInput.y * _mouseSensitivity;
        _verticalRotation += vertical;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_maxVerticalAngle, _maxVerticalAngle);
        _camera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }

    public void SetLookInput(Vector3 direction)
    {
        _lookInput = direction;
    }
}
