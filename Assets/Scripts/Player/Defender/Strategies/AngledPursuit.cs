using UnityEngine;

/// <summary>
/// Strategy for NPC defender movement that calculates pursuit angles based on the target's position and speed.
/// </summary>
public class AngledPursuit : IDefenderStrategy
{
    private readonly DefenderMovements _defenderMovements;
    private PlayerInfo _defenderInfo;
    private PlayerInfo _targetInfo;

    public AngledPursuit(DefenderMovements defenderMovements, PlayerInfo defenderInfo, PlayerInfo targetInfo)
    {
        _defenderMovements = defenderMovements;
        _defenderInfo = defenderInfo;
        _targetInfo = targetInfo;
    }

    /// <summary>
    /// Moves the defender NPC towards the calculated pursuit direction.
    /// </summary>
    public void Move()
    {
        if(_defenderMovements == null) return;
        Vector3 direction = CalculatePursuitDirection(_defenderInfo, _targetInfo);
        _defenderMovements.Move(direction);
    }

    /// <summary>
    /// Calculates the optimal pursuit direction based on the Defender and target positions, and their speeds.
    /// </summary>
    /// <param name="defender">Defender information containing Defender position and speed.</param>
    /// <param name="target">Target information containing target position and speed.</param>
    /// <returns>The normalized direction vector for pursuit.</returns>
    private Vector3 CalculatePursuitDirection(PlayerInfo defender, PlayerInfo target)
    {
        Vector3 targetPosition = target.playerTransform.position;
        Vector3 defenderPosition = defender.playerTransform.position;

        Vector3 relativePosition = targetPosition - defenderPosition;
        float distance = relativePosition.magnitude;

        // Calculate relative speed
        float relativeSpeed = Mathf.Sqrt(Mathf.Max(0f, defender.playerSpeed * defender.playerSpeed - target.playerSpeed * target.playerSpeed));

        if (relativeSpeed <= 0f || distance <= defender.playerSpeed * Time.deltaTime)
        {
            // Fallback strategy: move directly towards the target's current position
            return relativePosition.normalized;
        }

        // Calculate the time to intercept using the estimated relative speed
        float timeToIntercept = distance / relativeSpeed;

        // Predict the target's future position based on its current speed
        Vector3 predictedTargetPosition = targetPosition + target.playerSpeed * timeToIntercept * relativePosition.normalized;

        // Calculate the distance to the predicted target position
        Vector3 relativePredictedPosition = predictedTargetPosition - defenderPosition;
        float predictedDistance = relativePredictedPosition.magnitude;

        // Check if the defender can intercept the predicted target position
        if (predictedDistance <= defender.playerSpeed * Time.deltaTime)
        {
            // If the defender can intercept, move towards the predicted target position
            return relativePredictedPosition.normalized;
        }
        else
        {
            // If the defender cannot intercept, use the normal pursuit calculation
            Vector3 tacklePoint = targetPosition + target.playerSpeed * timeToIntercept * relativePosition.normalized;
            return (tacklePoint - defenderPosition).normalized;
        }
    }
}
