using Godot;

public abstract class State
{
    protected State(Sprout sprout)
    {
        Sprout = sprout;
        Playback = sprout
            .GetNode<AnimationTree>("%AnimationTree")
            .Get("parameters/playback")
            .As<AnimationNodeStateMachinePlayback>();
    }

    protected Sprout Sprout { get; init; }
    protected AnimationNodeStateMachinePlayback Playback { get; init; }
    
    protected static string IdleBlendPositionPath => "parameters/IdleBlendSpace/blend_position";
    protected static string WalkBlendPositionPath => "parameters/WalkBlendSpace/blend_position";
    protected static string WalkBlendSpaceName => "WalkBlendSpace";
    protected static string IdleBlendSpaceName => "IdleBlendSpace";
    
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract State ProcessUnselected(double delta);
    public abstract void PhysicsProcessUnselected(double delta);
    public abstract State ProcessSelected(double delta);
    public abstract void PhysicsProcessSelected(double delta);
}
