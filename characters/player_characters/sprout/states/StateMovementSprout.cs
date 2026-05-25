using Godot;
using SproutandMeRPG.misc;

namespace SproutandMeRPG.characters.player_characters.sprout.states;

public class StateMovementSprout(Sprout sprout) : StateSprout(sprout)
{
    private static string IdleBlendPositionNormal => "parameters/IdleStateMachine/IdleBlendSpace/blend_position";
    private static string IdleBlendPositionSitIdle => "parameters/IdleStateMachine/IdleSitBlendSpace/blend_position";
    private static string IdleBlendPositionSit => "parameters/IdleStateMachine/SitBlendSpace/blend_position";
    private static string IdleBlendPositionUnsit => "parameters/IdleStateMachine/UnsitBlendTree/AnimationNodeBlendSpace2D/blend_position";
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
            Sprout.Velocity = Vector2.Zero;
            return new StateIdleSprout(Sprout);
        }
        
        // Fake Lerp
        Sprout.Velocity = Sprout.Velocity.Lerp(TargetVelocity, Acceleration * (float)delta);
        Animate(TargetVelocity);

        return null;
    }

    public override void PhysicsProcessSelected(double delta)
    {
        Sprout.MoveAndSlide();
    }

    public override State ProcessUnselected(double delta)
    {
        if (!Sprout.Following || Sprout.GlobalPosition.DistanceSquaredTo(Sprout.Other.GlobalPosition) <
            FollowDistance * FollowDistance)
        {
            Sprout.Velocity = Vector2.Zero;
            return new StateIdleSprout(Sprout);
        }

        Vector2 direction = (Sprout.Other.GlobalPosition - Sprout.GlobalPosition).Normalized();
        Sprout.Velocity = Sprout.Velocity.Lerp(direction * MaxSpeed, Acceleration * (float)delta);
        Animate(direction);
        
        return null;
    }

    public override void PhysicsProcessUnselected(double delta)
    {
        Sprout.MoveAndSlide();
    }

    private void Animate(Vector2 blendPosition)
    {
        Sprout.AnimationTree.Set(WalkBlendPositionPath, blendPosition);
        Sprout.AnimationTree.Set(IdleBlendPositionNormal, blendPosition);
        Sprout.AnimationTree.Set(IdleBlendPositionSit, blendPosition);
        Sprout.AnimationTree.Set(IdleBlendPositionSitIdle, blendPosition);
        Sprout.AnimationTree.Set(IdleBlendPositionUnsit, blendPosition);
    }
}