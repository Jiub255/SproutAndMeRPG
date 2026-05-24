public partial class Me : Character
{
    public override void _Ready()
    {
        base._Ready();
        
        StateMachine = new StateMachine(new StateIdleMe(this));
    }
}
