using System.Collections.Generic;
using Godot;
using SproutandMeRPG.characters.non_player_characters;
using SproutandMeRPG.characters.player_characters.me.states;
using SproutandMeRPG.misc;

namespace SproutandMeRPG.characters.player_characters.me;

public partial class Me : PlayerCharacter
{
    public Area2D InteractionArea { get; private set; }
    private List<ITalkable> Talkables { get; } = [];
    private dialog.Dialogic Dialogic { get; set; }
    private bool Talking { get; set; }
    
    public override void _Ready()
    {
        base._Ready();
        
        StateMachine = new StateMachine(new StateIdleMe(this));
        InteractionArea = GetNode<Area2D>("%InteractionArea");
        
        Node dialogicGameHandler = GetNode("%DialogicGameHandler");
        Dialogic = new dialog.Dialogic(dialogicGameHandler);

        InteractionArea.BodyEntered += AddInteractable;
        InteractionArea.BodyExited += RemoveInteractable;
        Dialogic.TimelineEnded += EndDialog;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        
        InteractionArea.BodyEntered -= AddInteractable;
        InteractionArea.BodyExited -= RemoveInteractable;
        Dialogic.TimelineEnded -= EndDialog;
    }

    public override void _Process(double delta)
    {
        if (Talking) return;
        
        base._Process(delta);

        if (Input.IsActionJustPressed(Inputs.Interact) && Talkables != null)
        {
            StartDialog();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Talking) return;
        
        base._PhysicsProcess(delta);
    }

    private void StartDialog()
    {
        if (Talkables.Count == 0) return;
            
        string timelineName = Talkables[0].GetDialog();
        if (Dialogic.TimelineNull &&  timelineName != null)
        {
            Dialogic.Start(timelineName);
            Talking = true;
        }
    }

    private void EndDialog()
    {
        Talking = false;
    }

    private void AddInteractable(Node2D body)
    {
        if (body is ITalkable talkable)
        {
            Talkables.Add(talkable);
        }
    }

    private void RemoveInteractable(Node2D body)
    {
        if (body is ITalkable talkable)
        {
            Talkables.Remove(talkable);
        }
    }
}