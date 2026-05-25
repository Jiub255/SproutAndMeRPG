using Godot;
using SproutandMeRPG.misc;

namespace SproutandMeRPG.characters.player_characters.me.states;

public class StateIdleMe(Me me) : StateMe(me)
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
        return input.IsZeroApprox() ? null : new StateMovementMe(Me);
    }

    public override void PhysicsProcessSelected(double delta) {}

    public override State ProcessUnselected(double delta)
    {
        if (!Me.Following) return null;
        
        bool needToStartFollowing = Me.GlobalPosition.DistanceSquaredTo(Me.Other.GlobalPosition) >
                                    FollowDistance * FollowDistance * FollowRadiusOverlapMultiplier;
        return needToStartFollowing ? new StateMovementMe(Me) : null;
    }

    public override void PhysicsProcessUnselected(double delta) {}
}