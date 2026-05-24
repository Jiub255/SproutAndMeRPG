public partial class Sprout : Character
{
    public override void _Ready()
    {
        base._Ready();
        
        StateMachine = new StateMachine(new StateIdleSprout(this));
    }
}
