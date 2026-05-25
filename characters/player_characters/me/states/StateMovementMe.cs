using Godot;
using SproutandMeRPG.misc;

namespace SproutandMeRPG.characters.player_characters.me.states;

public class StateMovementMe(Me me) : StateMe(me)
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
            Me.Velocity = Vector2.Zero;
            return new StateIdleMe(Me);
        }
        
        // Fake Lerp
        Me.Velocity = Me.Velocity.Lerp(TargetVelocity, Acceleration * (float)delta);
        Animate(TargetVelocity);

        return null;
    }

    public override void PhysicsProcessSelected(double delta)
    {
        Me.MoveAndSlide();
    }

    public override State ProcessUnselected(double delta)
    {
        if (!Me.Following || Me.GlobalPosition.DistanceSquaredTo(Me.Other.GlobalPosition) <
            FollowDistance * FollowDistance)
        {
            Me.Velocity = Vector2.Zero;
            return new StateIdleMe(Me);
        }

        Vector2 direction = (Me.Other.GlobalPosition - Me.GlobalPosition).Normalized();
        Me.Velocity = Me.Velocity.Lerp(direction * MaxSpeed, Acceleration * (float)delta);
        Animate(direction);
        
        return null;
    }

    public override void PhysicsProcessUnselected(double delta)
    {
        Me.MoveAndSlide();
    }

    private void Animate(Vector2 blendPosition)
    {
        Me.InteractionArea.Rotation = blendPosition.Angle();

        Me.AnimationTree.Set(WalkBlendPositionPath, blendPosition);
        Me.AnimationTree.Set(IdleBlendPositionPath, blendPosition);
    }
}