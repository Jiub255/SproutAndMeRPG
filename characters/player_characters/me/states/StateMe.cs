namespace SproutandMeRPG.characters.player_characters.me.states;

public abstract class StateMe(Me me) : State(me)
{
    protected Me Me { get; } = me;
}