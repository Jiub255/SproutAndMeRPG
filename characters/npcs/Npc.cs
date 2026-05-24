using Godot;

public partial class Npc : CharacterBody2D
{
    [Export]
    private string Dialog { get; set; }
    
    public string GetDialog()
    {
        return Dialog;
    }
}
