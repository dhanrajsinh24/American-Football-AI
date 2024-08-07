using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BallCarrierMovements : PlayerMovements
{
    /// <summary>
    /// Calculates ball carrier's movement direction based on player input.
    /// </summary>
    private void Update()
    {
        // Get input from keyboard
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        var moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the character in the specified direction
        Move(moveDirection);
    }
}
