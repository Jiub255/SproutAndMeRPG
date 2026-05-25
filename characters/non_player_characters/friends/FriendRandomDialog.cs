using Godot;
using System;
using Godot.Collections;

public partial class FriendRandomDialog : Friend, ITalkable
{
    [Export] private Array<string> DialogPaths { get; set; } = [];

    public string GetDialog()
    {
        if (DialogPaths.Count == 0)
        {
            GD.PushError($"No dialogs found on {Name}");
            return null;
        }
        Random rng = new();
        int index =  rng.Next(DialogPaths.Count);
        return DialogPaths[index];
    }
}
