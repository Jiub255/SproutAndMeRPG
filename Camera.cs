using System;
using Godot;

namespace SproutandMeRPG;

public partial class Camera : Camera2D
{
    public characters.player_characters.PlayerCharacter FollowTarget { get; set; }

    [Export(PropertyHint.Range, "0f, 0.25f, 0.01f")]
    private float Smoothing { get; set; } = 0.025f;

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        if (FollowTarget == null) return;
        
        GlobalPosition = GlobalPosition.Lerp(
            FollowTarget.GlobalPosition,
            1.0f - MathF.Pow(Smoothing, (float)delta));
    }
}