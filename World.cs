using Godot;

public partial class World : Node2D
{
    private Party Party { get; set; }

    public override void _Ready()
    {
        base._Ready();
        
        Character sprout = GetNode<Character>("%Sprout");
        Character me =  GetNode<Character>("%Me");
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
    }
}
