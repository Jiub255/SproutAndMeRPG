using Godot;
using System;

public partial class Skeleton : NonPlayerCharacter
{
    public override void _Ready()
    {
        base._Ready();
        
        StateMachine = new StateMachineNpc(new StateBlankNpc(this));
    }
}
