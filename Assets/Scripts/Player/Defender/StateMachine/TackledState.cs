public class TackledState : IDefenderState
{
    private DefenderMovements _defenderMovements;
    public TackledState(Defender defender)
    {
        _defenderMovements = defender.DefenderMovements;
    }

    public void Enter()
    {
        _defenderMovements.StopMove();
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        //Exit
    }
}
