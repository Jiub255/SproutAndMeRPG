using Godot;

namespace SproutandMeRPG.characters.non_player_characters;

public abstract class StateNpc(NonPlayerCharacter npc)
{
    protected AnimationNodeStateMachinePlayback Playback { get; } = npc
        .GetNode<AnimationTree>("%AnimationTree")
        .Get("parameters/playback")
        .As<AnimationNodeStateMachinePlayback>();

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract StateNpc Process(double delta);
    public abstract void PhysicsProcess(double delta);
}