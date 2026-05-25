namespace SproutandMeRPG.characters.non_player_characters;

public class StateMachineNpc
{
    private StateNpc CurrentState { get; set; }
    
    
    public StateMachineNpc(StateNpc initialState)
    {
        ChangeState(initialState);
    }

    public void ProcessState(double delta)
    {
        StateNpc potentialNextState = CurrentState?.Process(delta);
        ChangeState(potentialNextState);
    }

    public void PhysicsProcessState(double delta)
    {
        CurrentState?.PhysicsProcess(delta);
    }

    private void ChangeState(StateNpc state)
    {
        if (state == null) return;
		
        //GD.Print($"Changing from {CurrentState?.GetType()} to {state.GetType()}");
        CurrentState?.ExitState();
        CurrentState = state;
        CurrentState?.EnterState();
    }
}