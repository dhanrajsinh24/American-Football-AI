using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the player initialization and positions of ball carrier and NPC defenders.
/// </summary>
public class PlayerInitialization : MonoBehaviour
{
    [SerializeField] private GameObject ballCarrierPrefab;
    [SerializeField] private GameObject npcDefenderPrefab;
    private GameObject _ballCarrier;
    private List<GameObject> _npcDefenders = new ();
    private List<Vector3> _defensivePositions;
    private const float YardsToMeters = 0.9144f; // Conversion factor from yards to meters

    private void Start() 
    {
        AddDefensivePositions();
        PositionPlayers();
    }

    private void PositionPlayers() 
    {
        // Position ball carrier at the starting position
        var ballCarrierPosition = new Vector3(0, ballCarrierPrefab.transform.position.y, -20f * YardsToMeters);
        _ballCarrier = Instantiate(ballCarrierPrefab, ballCarrierPosition, Quaternion.identity);
        _ballCarrier.transform.SetParent(transform);
        
        // Initialize ball carrier with speed
        var ballCarrier = _ballCarrier.GetComponent<BallCarrier>();
        var ballCarrierSpeed = 5f;
        ballCarrier.Initialize(ballCarrierSpeed);

        // Initialize all defenders
        for (int i = 0; i < _defensivePositions.Count; i++)
        {
            Vector3 position = _defensivePositions[i];
            PlayerInfo targetInfo = new(ballCarrier.transform, ballCarrierSpeed);

            // Defender index to use it for formulation if required by strategy 
            var defenderIndex = i+1;
            
            // Spawn defender
            var defender = DefenderFactory.Create(npcDefenderPrefab, defenderIndex, targetInfo, position);
            _npcDefenders.Add(defender);
        }
    }

    //Total 11 hardcoded defender positions
    private void AddDefensivePositions() 
    {
        // Get the y position for all defenders
        var yPos = npcDefenderPrefab.transform.position.y;

        // Defensive Positions in American Football
        _defensivePositions = new List<Vector3> 
        {
            // Defensive Line (DL)
            new(0f, yPos, 5f * YardsToMeters),                     // Nose Tackle (NT)
            new(2f * YardsToMeters, yPos, 5f * YardsToMeters),     // Defensive Tackle (DT)
            new(-2f * YardsToMeters, yPos, 5f * YardsToMeters),    // Defensive End (DE)
            
            // Linebackers (LB)
            new(0f, yPos, 10f * YardsToMeters),                    // Middle Linebacker
            new(-3f * YardsToMeters, yPos, 12f * YardsToMeters),    // Outside Linebacker (Left)
            new(3f * YardsToMeters, yPos, 12f * YardsToMeters),     // Outside Linebacker (Right)
            
            // Cornerbacks (CB)
            new(-5f * YardsToMeters, yPos, 20f * YardsToMeters),    // Left Cornerback
            new(5f * YardsToMeters, yPos, 20f * YardsToMeters),     // Right Cornerback
            
            // Safeties (S)
            new(0f, yPos, 30f * YardsToMeters),                     // Free Safety
            new(0f, yPos, 35f * YardsToMeters),                     // Strong Safety

            new(-1f * YardsToMeters, yPos, 25f * YardsToMeters)     // Extra Defender
        };
    }

}
