using Godot;

public class StateIdleMe(Character character) : State(character)
{
    private static string IdleBlendSpaceName => "IdleBlendSpace";
    
    public override void EnterState()
    {
       Playback.Travel(IdleBlendSpaceName);
    }

    public override void ExitState() {}

    public override State ProcessSelected(double delta)
    {
        Vector2 input = Input.GetVector(
            Inputs.Left,
            Inputs.Right,
            Inputs.Up,
            Inputs.Down);
        return input.IsZeroApprox() ? null : new StateMovementMe(Character);
    }

    public override void PhysicsProcessSelected(double delta) {}

    public override State ProcessUnselected(double delta)
    {
        if (!Character.Following) return null;
        
        bool needToStartFollowing = Character.GlobalPosition.DistanceSquaredTo(Character.Other.GlobalPosition) >
                            FollowDistance * FollowDistance * FollowRadiusOverlapMultiplier;
        return needToStartFollowing ? new StateMovementMe(Character) : null;
    }

    public override void PhysicsProcessUnselected(double delta) {}
}
