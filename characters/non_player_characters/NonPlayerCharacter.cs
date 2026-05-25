using Godot;

namespace SproutandMeRPG.characters.non_player_characters;

public abstract partial class NonPlayerCharacter : CharacterBody2D
{
    public AnimationTree AnimationTree { get; private set; }
    protected StateMachineNpc StateMachine { get; set; }
    
    public override void _Ready()
    {
        base._Ready();
        AnimationTree = GetNode<AnimationTree>("%AnimationTree");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        StateMachine.ProcessState(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        StateMachine.PhysicsProcessState(delta);
    }
}