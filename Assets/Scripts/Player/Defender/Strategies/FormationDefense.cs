using System;
using UnityEngine;

/// <summary>
/// Strategy for NPC defender movement that maintains a defensive formation to stop the ball carrier.
/// </summary>
public class FormationDefense : IDefenderStrategy
{
    private readonly DefenderMovements _defenderMovements;
    private readonly PlayerInfo _defenderInfo;
    private readonly PlayerInfo _targetInfo;
    private readonly int _defenderIndex;
    private readonly Vector3 _formationOffset;

    /// <summary>
    /// Initializes a new instance of the <see cref="FormationDefense"/> class.
    /// </summary>
    /// <param name="defenderMovements">The defender movements handler.</param>
    /// <param name="defenderInfo">Information about the defender.</param>
    /// <param name="targetInfo">Information about the target (ball carrier).</param>
    /// <param name="defenderIndex">Index of the defender in the formation.</param>
    /// <param name="formationOffset">The offset to maintain the formation.</param>
    public FormationDefense(DefenderMovements defenderMovements, PlayerInfo defenderInfo, PlayerInfo targetInfo, int defenderIndex, Vector3 formationOffset)
    {
        _defenderMovements = defenderMovements != null ? defenderMovements : throw new ArgumentNullException(nameof(defenderMovements));
        _defenderInfo = defenderInfo;
        _targetInfo = targetInfo;
        _defenderIndex = defenderIndex;
        _formationOffset = formationOffset;
    }

    /// <summary>
    /// Moves the defender NPC towards the calculated pursuit direction while maintaining formation.
    /// </summary>
    public void Move()
    {
        Vector3 direction = CalculatePursuitDirection(_defenderInfo, _targetInfo);
        _defenderMovements.Move(direction);
    }

    /// <summary>
    /// Calculates the optimal pursuit direction based on the defender and target positions,
    /// and their speeds, while maintaining the defensive formation.
    /// </summary>
    /// <param name="defender">Defender information containing defender position and speed.</param>
    /// <param name="target">Target information containing target position and speed.</param>
    /// <returns>The normalized direction vector for pursuit.</returns>
    private Vector3 CalculatePursuitDirection(PlayerInfo defender, PlayerInfo target)
    {
        Vector3 formationPosition = GetFormationPosition(defender);
        Vector3 relativePosition = formationPosition - defender.playerTransform.position;

        float distance = relativePosition.magnitude;
        float relativeSpeed = Mathf.Sqrt(Mathf.Max(0f, defender.playerSpeed * defender.playerSpeed - target.playerSpeed * target.playerSpeed));

        if (relativeSpeed <= 0f || distance <= defender.playerSpeed * Time.deltaTime)
        {
            // If relative speed is zero or the defender is very close, move directly towards the formation position
            return relativePosition.normalized;
        }

        float timeToTackle = distance / relativeSpeed;
        Vector3 predictedTargetPosition = target.playerTransform.position + target.playerSpeed * timeToTackle * relativePosition.normalized;
        Vector3 pursuitDirection = (predictedTargetPosition - defender.playerTransform.position).normalized;

        return pursuitDirection;
    }

    /// <summary>
    /// Calculates the desired formation position for the defender based on the formation offset and defender index.
    /// </summary>
    /// <param name="defender">Defender information.</param>
    /// <returns>The calculated formation position for the defender.</returns>
    private Vector3 GetFormationPosition(PlayerInfo defender)
    {
        Vector3 formationCenter = _targetInfo.playerTransform.position;

        // Calculate the offset for each defender's position based on formation and index
        Vector3 offset = new(_formationOffset.x * (_defenderIndex % 5 - 2), 0, _formationOffset.z * (_defenderIndex / 5));

        return formationCenter + offset;
    }
}
