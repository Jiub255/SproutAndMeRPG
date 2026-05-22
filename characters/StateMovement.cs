using Godot;

public class StateMovement(Sprout sprout) : State(sprout)
{
    private const int Speed = 200;
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
        return null;
    }

    public override void PhysicsProcessUnselected(double delta)
    {
    }

    public override State ProcessSelected(double delta)
    {
        Vector2 input = Input.GetVector("left", "right", "up", "down");
        TargetVelocity = input.Normalized() * Speed;

        if (TargetVelocity.IsZeroApprox())
        {
            Sprout.Velocity = Vector2.Zero;
            return new StateIdle(Sprout);
        }
        
        //Sprout.Velocity.MoveToward(TargetVelocity, Acceleration * (float)delta);
        // Fake Lerp for testing
        Sprout.Velocity = Sprout.Velocity.Lerp(TargetVelocity, Acceleration * (float)delta);
        Animate();

        return null;
    }

    public override void PhysicsProcessSelected(double delta)
    {
        Sprout.MoveAndSlide();
    }

    private void Animate()
    {
        Vector2 blendPosition = TargetVelocity;
        //Vector2 blendPosition = Sprout.Velocity.Normalized();
        Sprout.AnimationTree.Set(WalkBlendPositionPath, blendPosition);
        Sprout.AnimationTree.Set(IdleBlendPositionPath, blendPosition);
        // GD.Print($"walk pos: {Sprout.AnimationTree.Get(WalkBlendPositionPath)} " +
        //          $"idle pos: {Sprout.AnimationTree.Get(IdleBlendPositionPath)} " +
        //          $"blend pos: {blendPosition} " +
        //          $"target velocity: {TargetVelocity} " +
        //          $"velocity: {Sprout.Velocity}");
    }
}
