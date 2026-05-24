using Godot;

public abstract class State
{
    protected State(Character character)
    {
        Character = character;
        Playback = character
            .GetNode<AnimationTree>("%AnimationTree")
            .Get("parameters/playback")
            .As<AnimationNodeStateMachinePlayback>();
    }

    protected Character Character { get; }
    protected AnimationNodeStateMachinePlayback Playback { get; }
    
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
