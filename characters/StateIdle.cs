using Godot;

public class StateIdle(Sprout sprout) : State(sprout)
{
    public override void EnterState()
    {
       Playback.Travel(IdleBlendSpaceName);
    }

    public override void ExitState()
    {
    }

    public override State ProcessUnselected(double delta)
    {
        return null;
    }

    public override void PhysicsProcessUnselected(double delta)
    {
    }

    public override State ProcessSelected(double delta)
    {
        Vector2 input = Input.GetVector("left", "right", "up", "down") ;
        return input.IsZeroApprox() ? null : new StateMovement(Sprout);
    }

    public override void PhysicsProcessSelected(double delta)
    {
    }
}
