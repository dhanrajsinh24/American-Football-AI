using System;
using UnityEngine;

/// <summary>
/// Strategy for NPC defender movement that calculates pursuit angles based on the target's position and speed.
/// </summary>
public class AngledPursuit : IDefenderStrategy
{
    private readonly DefenderMovements _defenderMovements;
    private readonly PlayerInfo _defenderInfo;
    private readonly PlayerInfo _targetInfo;

    public AngledPursuit(DefenderMovements defenderMovements, PlayerInfo defenderInfo, PlayerInfo targetInfo)
    {
        _defenderMovements = defenderMovements != null ? defenderMovements : throw new ArgumentNullException(nameof(defenderMovements));
        _defenderInfo = defenderInfo;
        _targetInfo = targetInfo;
    }

    /// <summary>
    /// Moves the defender NPC towards the calculated pursuit direction.
    /// </summary>
    public void Move()
    {
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
        Vector3 relativePosition = target.playerTransform.position - defender.playerTransform.position;
        float distance = relativePosition.magnitude;

        float relativeSpeed = Mathf.Sqrt(Mathf.Max(0f, defender.playerSpeed * defender.playerSpeed - target.playerSpeed * target.playerSpeed));

        if (relativeSpeed <= 0f || distance <= defender.playerSpeed * Time.deltaTime)
        {
            // Fallback strategy: move directly towards the target's current position
            return relativePosition.normalized;
        }

        float timeToIntercept = distance / relativeSpeed;

        Vector3 predictedTargetPosition = target.playerTransform.position + target.playerSpeed * timeToIntercept * relativePosition.normalized;
        Vector3 pursuitDirection = (predictedTargetPosition - defender.playerTransform.position).normalized;

        return pursuitDirection;
    }
}
