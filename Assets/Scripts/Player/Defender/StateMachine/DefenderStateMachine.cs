using System;

[Serializable]
public class DefenderStateMachine
{
    public IDefenderState CurrentState {get; private set;}
    
    // All states needed for this state machine
    private PursuingState _pursuingState;
    private TackledState _tackledState;

    /// <summary>
    /// Initializes all the states required.
    /// </summary>
    /// <param name="strategy">The strategy for the defender.</param>
    /// <param name="defender">The defender NPC.</param>
    public DefenderStateMachine(IDefenderStrategy strategy, Defender defender)
    {
        _pursuingState = new PursuingState(strategy, defender);
        
        //TODO
        _tackledState = new TackledState(defender);
    }
    
    /// <summary>
    /// Initializes the state machine by setting the initial state.
    /// </summary>
    public void Initialize()  
    {
        TransitionTo(_pursuingState);
    }

    /// <summary>
    /// Transitions to the next state.
    /// </summary>
    /// <param name="nextState">The next state.</param>
    public void TransitionTo(IDefenderState nextState)
    {
        CurrentState?.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    public void Execute() 
    {
        CurrentState?.Execute();
    }
}
