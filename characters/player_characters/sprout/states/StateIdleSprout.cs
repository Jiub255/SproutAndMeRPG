using Godot;
using System;

public class StateIdleSprout(Sprout sprout) : StateSprout(sprout)
{
    private static string IdleStateMachineName => "IdleStateMachine";
    private static string IdleSitName => "SitBlendSpace";
    private static string IdleUnsitName => "UnsitBlendTree";
    private static string IdlePlaybackPath => "parameters/IdleStateMachine/playback";
    private static string IdleSitTimeseekPath => "parameters/IdleStateMachine/IdleSitBlendTree/TimeSeek/seek_request";
    private static string UnsitTimeseekPath => "parameters/IdleStateMachine/UnsitBlendTree/TimeSeek/seek_request";
    private static float AnimationLength => 0.65f;
    private static float MinRandomTime => 3f;
    private static float MaxRandomTime => 10f;
    private float Timer { get; set; }
    private Random Random { get; } = new();
    private AnimationNodeStateMachinePlayback IdlePlayback { get; } = sprout
        .GetNode<AnimationTree>("%AnimationTree")
        .Get(IdlePlaybackPath)
        .As<AnimationNodeStateMachinePlayback>();

    private enum IdlePosition
    {
        Normal,
        Sitting,
        Laying
    }
    private IdlePosition IdleVariation { get; set; } = IdlePosition.Normal;
    
    
    public override void EnterState()
    {
        Playback.Travel(IdleStateMachineName);
        ResetTimerRandom();
    }

    public override void ExitState() {}

    public override State ProcessSelected(double delta)
    {
        Vector2 input = Input.GetVector(
            Inputs.Left,
            Inputs.Right,
            Inputs.Up,
            Inputs.Down);
        
        return !input.IsZeroApprox() ? new StateMovementSprout(Sprout) : ProcessShared(delta);
    }

    public override void PhysicsProcessSelected(double delta) {}

    public override State ProcessUnselected(double delta)
    {
        if (!Sprout.Following) return ProcessShared(delta);
        
        bool needToStartFollowing = Sprout.GlobalPosition.DistanceSquaredTo(Sprout.Other.GlobalPosition) >
                                    FollowDistance * FollowDistance * FollowRadiusOverlapMultiplier;
        
        return needToStartFollowing ? new StateMovementSprout(Sprout) : ProcessShared(delta);
    }

    public override void PhysicsProcessUnselected(double delta) {}

    private State ProcessShared(double delta)
    {
        Timer -= (float)delta;
        if (Timer > 0) return null;
        
        ResetTimerRandom();
        ChooseIdlePositionRandom();

        return null;
    }

    private void ResetTimerRandom()
    {
        Timer = Random.RandomFRange(MinRandomTime, MaxRandomTime);
    }

    private void ChooseIdlePositionRandom()
    {
        switch (IdleVariation)
        {
            case IdlePosition.Normal:
            {
                int random = Random.Next(1);
                //int random = Random.Next(5);
                switch (random)
                {
                    case 0:
                        // TODO: Shift to sitting animation
                        GD.Print("Switching to sitting animation");
                        Sprout.AnimationTree.Set(IdleSitTimeseekPath, AnimationLength);
                        IdleVariation = IdlePosition.Sitting;
                        IdlePlayback.Travel(IdleSitName);
                        break;
                    // case 1:
                    //     // TODO: Shift to laying animation
                    //     IdleVariation = IdlePosition.Laying;
                    //     IdlePlayback.Travel(IdleLayName);
                    //     break;
                    default:
                        // TODO: Stay in regular idle animation
                        GD.Print("Staying in idle animation");
                        IdleVariation = IdlePosition.Normal;
                        break;
                }
                break;
            }
            case IdlePosition.Sitting:
            {
                if (Random.XinYOdds(1, 1))
                    //if (Random.XinYOdds(1, 4))
                {
                    // TODO: Switch to unsitting blend tree, which should automatically go to regular idle after
                    GD.Print("Switching to unsitting animation");
                    Sprout.AnimationTree.Set(UnsitTimeseekPath, AnimationLength);
                    IdleVariation = IdlePosition.Normal;
                    IdlePlayback.Travel(IdleUnsitName);
                }
                break;
            }
            case IdlePosition.Laying:
            {
                if (Random.XinYOdds(1, 7))
                {
                    // TODO: Switch to unlaying blend tree, which should automatically go to regular idle after
                    IdleVariation = IdlePosition.Normal;
                }
                break;
            }
        }
    }
}
