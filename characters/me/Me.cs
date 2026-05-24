using Godot;
using System.Collections.Generic;

public partial class Me : Character
{
    public Area2D InteractionArea { get; private set; }
    private List<Npc> Npcs { get; } = [];
    private Dialogic Dialogic { get; set; }
    
    public override void _Ready()
    {
        base._Ready();
        
        StateMachine = new StateMachine(new StateIdleMe(this));
        InteractionArea = GetNode<Area2D>("%InteractionArea");

        InteractionArea.BodyEntered += AddInteractable;
        InteractionArea.BodyExited += RemoveInteractable;
        
        Node dialogicGameHandler = GetNode("%DialogicGameHandler");
        Dialogic = new Dialogic(dialogicGameHandler);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        
        InteractionArea.BodyEntered -= AddInteractable;
        InteractionArea.BodyExited -= RemoveInteractable;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed(Inputs.Interact) && Npcs != null)
        {
            if (Npcs.Count == 0) return;
            
            string timelineName = Npcs[0].GetDialog();
            if (Dialogic.TimelineNull)
            {
                Dialogic.Start(timelineName);
            }
        }
    }

    private void AddInteractable(Node2D body)
    {
        if (body is Npc npc)
        {
            Npcs.Add(npc);
        }
    }

    private void RemoveInteractable(Node2D body)
    {
        if (body is Npc npc)
        {
            Npcs.Remove(npc);
        }
    }
}
