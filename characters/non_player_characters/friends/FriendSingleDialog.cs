using Godot;

public partial class FriendSingleDialog : Friend, ITalkable
{
    [Export]
    private string DialogPath { get; set; }

    public string GetDialog()
    {
        return DialogPath;
    }
}
