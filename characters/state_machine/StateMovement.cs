using Godot;

public class StateMovement(Character character) : State(character)
{
    private const int MaxSpeed = 200;
    private const float Acceleration = 20f;
    
    private Vector2 TargetVelocity { get; set; } = Vector2.Zero;
    
    public override void EnterState()
    {
       Playback.Travel(WalkBlendSpaceName);
    }

    public override void ExitState()
    {
    }

    public override State ProcessUnselected(double delta)
    {
        if (Character.GlobalPosition.DistanceSquaredTo(Character.Other.GlobalPosition) <
            FollowDistance * FollowDistance)
        {
            Character.Velocity = Vector2.Zero;
            return new StateIdle(Character);
        }
        
        Vector2 direction = (Character.Other.GlobalPosition - Character.GlobalPosition).Normalized();
        Character.Velocity = Character.Velocity.Lerp(direction * MaxSpeed, Acceleration * (float)delta);
        // Hack to make follow movement work
        TargetVelocity = Character.Velocity;
        Animate();
        
        return null;
    }

    public override void PhysicsProcessUnselected(double delta)
    {
        Character.MoveAndSlide();
    }

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
            return new StateIdle(Character);
        }
        
        // Fake Lerp for testing
        Character.Velocity = Character.Velocity.Lerp(TargetVelocity, Acceleration * (float)delta);
        Animate();

        return null;
    }

    public override void PhysicsProcessSelected(double delta)
    {
        Character.MoveAndSlide();
    }

    private void Animate()
    {
        Vector2 blendPosition = TargetVelocity;
        //Vector2 blendPosition = Sprout.Velocity.Normalized();
        Character.AnimationTree.Set(WalkBlendPositionPath, blendPosition);
        Character.AnimationTree.Set(IdleBlendPositionPath, blendPosition);
        // GD.Print($"walk pos: {Sprout.AnimationTree.Get(WalkBlendPositionPath)} " +
        //          $"idle pos: {Sprout.AnimationTree.Get(IdleBlendPositionPath)} " +
        //          $"blend pos: {blendPosition} " +
        //          $"target velocity: {TargetVelocity} " +
        //          $"velocity: {Sprout.Velocity}");
    }
}
