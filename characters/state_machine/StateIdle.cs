using Godot;

public class StateIdle(Character character) : State(character)
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
        if (!Character.Following) return null;
        
        bool needToStartFollowing = Character.GlobalPosition.DistanceSquaredTo(Character.Other.GlobalPosition) >
                            FollowDistance * FollowDistance * 4f;
        return needToStartFollowing ? new StateMovement(Character) : null;
    }

    public override void PhysicsProcessUnselected(double delta)
    {
    }

    public override State ProcessSelected(double delta)
    {
        Vector2 input = Input.GetVector(
            Inputs.Left,
            Inputs.Right,
            Inputs.Up,
            Inputs.Down);
        return input.IsZeroApprox() ? null : new StateMovement(Character);
    }

    public override void PhysicsProcessSelected(double delta)
    {
    }
}
