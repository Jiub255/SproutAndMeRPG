using Godot;
using System;
using System.Collections.Generic;

namespace SproutandMeRPG.characters.behavior_tree;

public partial class TestBtChar : CharacterBody2D
{
    private float _lowHealthPercentage = 50f;
    private int _maxHealth = 10;
    private float _distanceMoved;
    private BehaviorTest _behaviorTest;
    private float _damageTimer = 0.5f;
    private Label _hpLabel;
    public int Health { get; private set; }
    
    public override void _Ready()
    {
        Health = _maxHealth;
        
        _hpLabel = GetNode<Label>("Label");
        _hpLabel.Text = Health.ToString();

        Dictionary<string, Delegate> testCharActions = new()
        {
            ["Injured"] = Injured,
            ["LocateShelter"] = LocateShelter,
            ["DangerNearby"] = DangerNearby,
        };
        _behaviorTest = new BehaviorTest(testCharActions);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        BTResult status = _behaviorTest.BehaviorManager.Cycle();
        
        _damageTimer -= (float)delta;
        if (_damageTimer <= 0)
        {
            _damageTimer = 0.5f;
            Health--;
            _hpLabel.Text = Health.ToString();
        }
    }

    public BTResult Injured()
    {
        return Health < _maxHealth * _lowHealthPercentage / 100f ? BTResult.Success : BTResult.Failure;
    }

    public BTResult LocateShelter()
    {
        Vector2 oldPosition = GlobalPosition;
        Velocity = Vector2.Down * 25f;
        MoveAndSlide();
        Vector2 newPosition = GlobalPosition;
        _distanceMoved += (newPosition - oldPosition).Length();
        if (_distanceMoved > 100)
        {
         //   GD.Print("moved 100");
            _distanceMoved = 0;
            Health = _maxHealth;
            _hpLabel.Text = Health.ToString();
            
            return BTResult.Success;
        }
        return BTResult.Running;
    }

    public BTResult DangerNearby()
    {
        return BTResult.Failure;
    }
}