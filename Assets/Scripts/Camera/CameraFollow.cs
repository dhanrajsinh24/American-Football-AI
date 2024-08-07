using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    private Transform _target;
    private Vector3 _offset = new (0, 3.5f, -5f);

    // Smoothing variables
    [SerializeField] private float smoothSpeed = 0.125f; //controls the follow position smoothing
    [SerializeField] private float rotationSmoothSpeed = 5f; //controls follow rotation smoothing

    private Vector3 _velocity = Vector3.zero;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        if (!_target) return;

        // Calculate smoothed position using Smooth Damping
        var desiredPosition = _target.position + _offset;
        var smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, smoothSpeed);

        // Apply smoothed position
        transform.position = smoothedPosition;

        // Calculate Smooth rotation
        var desiredRotation = Quaternion.LookRotation(_target.position - transform.position);
        
        // Apply smoothed rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
    }
}