using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BallCarrier : MonoBehaviour
{
    private BallCarrierMovements _ballCarrierMovements;

    /// <summary>
    /// Initializes the ball carrier with the specified speed.
    /// </summary>
    /// <param name="speed">The speed of the ball carrier.</param>
    public void Initialize(float speed)
    {
       _ballCarrierMovements = GetComponent<BallCarrierMovements>();
       _ballCarrierMovements.Speed = speed; 
        FindObjectOfType<CameraFollow>().SetTarget(transform);
    }
}
