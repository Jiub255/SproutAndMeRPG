public class StateBlankNpc(NonPlayerCharacter npc) : StateNpc(npc)
{
    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override StateNpc Process(double delta)
    {
        return null;
    }

    public override void PhysicsProcess(double delta)
    {
    }
}
