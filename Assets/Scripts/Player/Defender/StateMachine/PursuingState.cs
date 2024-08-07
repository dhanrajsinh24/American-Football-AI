public class PursuingState : IDefenderState
{
    private const float distanceRequiredToTackle = 1f;
    private Defender _defender;
    private IDefenderStrategy _strategy;

    public PursuingState(IDefenderStrategy strategy, Defender defender)
    {
        _strategy = strategy;
        _defender = defender;
    }

    public void Enter()
    {
        //Enter
    }

    public void Execute()
    {
        //TODO if player is near then tackled state
        _strategy.Move();
    }

    public void Exit()
    {
        //Exit
    }
}
