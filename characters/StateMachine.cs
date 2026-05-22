using Godot;

public class StateMachine
{
    private State CurrentState { get; set; }
    
    
	public StateMachine(Sprout sprout)
	{
		ChangeState(new StateIdle(sprout));
	}

	public void ProcessStateUnselected(double delta)
	{
		State potentialNextState = CurrentState?.ProcessUnselected(delta);
		ChangeState(potentialNextState);
	}

	public void PhysicsProcessStateUnselected(double delta)
	{
		CurrentState?.PhysicsProcessUnselected(delta);
	}

	public void ProcessStateSelected(double delta)
	{
		State potentialNextState = CurrentState?.ProcessSelected(delta);
		ChangeState(potentialNextState);
	}

	public void PhysicsProcessStateSelected(double delta)
	{
		CurrentState?.PhysicsProcessSelected(delta);
	}

	private void ChangeState(State state)
	{
		if (state == null) return;
		GD.Print($"Changing from {CurrentState?.GetType()} to {state.GetType()}");
		CurrentState?.ExitState();
		CurrentState = state;
		CurrentState?.EnterState();
	}
}
