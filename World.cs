using Godot;
using SproutandMeRPG.characters.player_characters;
using SproutandMeRPG.misc;

namespace SproutandMeRPG;

public partial class World : Node2D
{
    private Party Party { get; set; }

    public override void _Ready()
    {
        base._Ready();
        
        characters.player_characters.PlayerCharacter sprout = GetNode<characters.player_characters.PlayerCharacter>("%Sprout");
        characters.player_characters.PlayerCharacter me =  GetNode<characters.player_characters.PlayerCharacter>("%Me");
        Camera camera = GetNode<Camera>("%Camera");

        sprout.Other = me;
        me.Other = sprout;

        Party = new Party(sprout, me, camera);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed(Inputs.Switch))
        {
            Party.Switch();
        }

        if (Input.IsActionJustPressed(Inputs.ToggleFollow))
        {
            Party.ToggleFollower();
        }
    }
}