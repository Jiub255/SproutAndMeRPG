using SproutandMeRPG.characters.player_characters.sprout.states;

namespace SproutandMeRPG.characters.player_characters.sprout;

public partial class Sprout : PlayerCharacter
{
    public override void _Ready()
    {
        base._Ready();
        
        StateMachine = new StateMachine(new StateIdleSprout(this));
    }
}