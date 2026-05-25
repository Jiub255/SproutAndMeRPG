public partial class Sprout : PlayerCharacter
{
    public override void _Ready()
    {
        base._Ready();
        
        StateMachine = new StateMachine(new StateIdleSprout(this));
    }
}
