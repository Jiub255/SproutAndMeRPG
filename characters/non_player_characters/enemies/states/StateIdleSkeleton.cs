namespace SproutandMeRPG.characters.non_player_characters.enemies.states;

public class StateIdleSkeleton(NonPlayerCharacter npc) : StateNpc(npc)
{
    private static string IdleBlendSpaceName = "IdleBlendSpace";
    
    public override void EnterState()
    {
        Playback.Travel(IdleBlendSpaceName);
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