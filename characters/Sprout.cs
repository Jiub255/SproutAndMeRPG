using Godot;

public partial class Sprout : CharacterBody2D
{
    public AnimationTree AnimationTree { get; private set; }
    private StateMachine StateMachine { get; set; }

    public override void _Ready()
    {
        base._Ready();
        AnimationTree = GetNode<AnimationTree>("%AnimationTree");
        StateMachine = new StateMachine(this);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        StateMachine.ProcessStateSelected(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        
        StateMachine.PhysicsProcessStateSelected(delta);
    }
}
