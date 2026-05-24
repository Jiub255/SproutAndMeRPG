using Godot;

public class StateMovementMe(Character character) : State(character)
{
    private static string IdleBlendPositionPath => "parameters/IdleBlendSpace/blend_position";
    private int MaxSpeed { get; set; } = 200;
    private float Acceleration { get; set; } = 20f;
    
    private Vector2 TargetVelocity { get; set; } = Vector2.Zero;
    
    public override void EnterState()
    {
       Playback.Travel(WalkBlendSpaceName);
    }

    public override void ExitState() {}

    public override State ProcessSelected(double delta)
    {
        Vector2 input = Input.GetVector(
            Inputs.Left,
            Inputs.Right,
            Inputs.Up,
            Inputs.Down);
        TargetVelocity = input.Normalized() * MaxSpeed;

        if (TargetVelocity.IsZeroApprox())
        {
            Character.Velocity = Vector2.Zero;
            return new StateIdleMe(Character);
        }
        
        // Fake Lerp
        Character.Velocity = Character.Velocity.Lerp(TargetVelocity, Acceleration * (float)delta);
        Animate(TargetVelocity);

        return null;
    }

    public override void PhysicsProcessSelected(double delta)
    {
        Character.MoveAndSlide();
    }

    public override State ProcessUnselected(double delta)
    {
        if (!Character.Following || Character.GlobalPosition.DistanceSquaredTo(Character.Other.GlobalPosition) <
            FollowDistance * FollowDistance)
        {
            Character.Velocity = Vector2.Zero;
            return new StateIdleMe(Character);
        }

        Vector2 direction = (Character.Other.GlobalPosition - Character.GlobalPosition).Normalized();
        Character.Velocity = Character.Velocity.Lerp(direction * MaxSpeed, Acceleration * (float)delta);
        Animate(Character.Velocity);
        
        return null;
    }

    public override void PhysicsProcessUnselected(double delta)
    {
        Character.MoveAndSlide();
    }

    private void Animate(Vector2 blendPosition)
    {
        Character.AnimationTree.Set(WalkBlendPositionPath, blendPosition);
        Character.AnimationTree.Set(IdleBlendPositionPath, blendPosition);
    }
}
