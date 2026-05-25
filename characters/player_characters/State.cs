using Godot;

namespace SproutandMeRPG.characters.player_characters;

public abstract class State(PlayerCharacter playerCharacter)
{
    protected AnimationNodeStateMachinePlayback Playback { get; } = playerCharacter
        .GetNode<AnimationTree>("%AnimationTree")
        .Get("parameters/playback")
        .As<AnimationNodeStateMachinePlayback>();

    protected static float FollowDistance => 70f;
    protected static float FollowRadiusOverlapMultiplier => 4f;
    protected static string WalkBlendPositionPath => "parameters/WalkBlendSpace/blend_position";
    protected static string WalkBlendSpaceName => "WalkBlendSpace";
    
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract State ProcessSelected(double delta);
    public abstract void PhysicsProcessSelected(double delta);
    public abstract State ProcessUnselected(double delta);
    public abstract void PhysicsProcessUnselected(double delta);
}