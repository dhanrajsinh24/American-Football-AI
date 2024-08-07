using UnityEngine;

public class PlayerMovements : MonoBehaviour, IMovable
{
    [SerializeField] private float speed; // Movement speed
    public float Speed { get => speed; set => speed = value; }
    private CharacterController _controller;
    private Vector3 _currentDirection;

    [SerializeField] private float acceleration = 10f; // Acceleration rate
    [SerializeField] private float deceleration = 10f; // Deceleration rate
    private Vector3 _currentVelocity; // Current movement velocity

    protected virtual void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void LateUpdate() 
    {
        if (_currentDirection != Vector3.zero)
        {
            // Accelerate towards the input direction
            _currentVelocity += _currentDirection * acceleration * Time.deltaTime;
            _currentVelocity = Vector3.ClampMagnitude(_currentVelocity, speed);
        }
        else
        {
            // Decelerate when no input is detected
            _currentVelocity = Vector3.Lerp(_currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        // Move the character controller based on current velocity
        _controller.Move(_currentVelocity * Time.deltaTime);
    }

    public void Move(Vector3 direction)
    {
        // If there's input, move the character
        if (direction != Vector3.zero)
        {
            _currentDirection = direction;

            // Rotate the character to face the movement direction
            // Avoiding rotations other than y
            Vector3 newDirection = new (direction.x, 0, direction.z);
            transform.forward = newDirection;
        }
        else StopMove();
    }

    public void StopMove()
    {
        _currentDirection = Vector3.zero;
    }
}