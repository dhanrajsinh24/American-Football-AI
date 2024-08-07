public interface IDefenderState
{
    /// <summary>
    /// Code that runs when we first enter the state.
    /// </summary>
    void Enter();
    
    /// <summary>
    /// per-frame logic, include condition to transition to a new state
    /// </summary>
    void Execute();
    
    /// <summary>
    /// code that runs when we exit the state
    /// </summary>
    void Exit();
}