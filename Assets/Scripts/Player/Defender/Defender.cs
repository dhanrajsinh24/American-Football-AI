using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] private DefenderStateMachine stateMachine;
    public DefenderMovements DefenderMovements;

    /// <summary>
    /// Initializes the NPC with a strategy, and speed.
    /// </summary>
    /// <param name="strategy">The strategy for Defender movement.</param>
    /// <param name="speed">The speed of the Defender.</param>
    public void Initialize(IDefenderStrategy strategy, float speed)
    {
        DefenderMovements.Speed = speed;
        stateMachine = new DefenderStateMachine(strategy, this);
        stateMachine.Initialize();
    }

    private void Update()
    {
        stateMachine.Execute();
    }
}
