namespace SproutandMeRPG.characters.player_characters.sprout.states;

public abstract class StateSprout(Sprout sprout) : State(sprout)
{
    protected Sprout Sprout { get; } = sprout;
}