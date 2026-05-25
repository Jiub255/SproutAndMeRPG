using Godot;

public abstract partial class Friend : NonPlayerCharacter
{
    public override void _Ready()
    {
        base._Ready();
        
        StateMachine = new StateMachineNpc(new StateBlankNpc(this));
    }
}
