using Godot;

namespace SproutandMeRPG.characters.non_player_characters.friends;

public partial class FriendSingleDialog : Friend, ITalkable
{
    [Export]
    private string DialogPath { get; set; }

    public string GetDialog()
    {
        return DialogPath;
    }
}