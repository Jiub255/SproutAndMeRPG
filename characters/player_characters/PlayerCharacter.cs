using Godot;

public abstract partial class PlayerCharacter : CharacterBody2D
{
    public AnimationTree AnimationTree { get; private set; }
    public bool Selected { get; set; }
    public bool Following { get; set; } = true;
    public PlayerCharacter Other { get; set; }
    
    protected StateMachine StateMachine { get; set; }

    
    public override void _Ready()
    {
        base._Ready();
        AnimationTree = GetNode<AnimationTree>("%AnimationTree");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Selected)
        {
            StateMachine.ProcessStateSelected(delta);
        }
        else
        {
            StateMachine.ProcessStateUnselected(delta);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (Selected)
        {
            StateMachine.PhysicsProcessStateSelected(delta);
        }
        else
        {
            StateMachine.PhysicsProcessStateUnselected(delta);
        }
    }
}
