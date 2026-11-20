using Godot;
using SproutandMeRPG.characters.behavior_tree;
using SproutandMeRPG.characters.player_characters.me;
using SproutandMeRPG.misc;
using System;
using System.Collections.Generic;

namespace SproutandMeRPG.characters.non_player_characters.enemies;

public partial class Skeleton : CharacterBody2D
{
    private BehaviorSkeleton _behavior;
    private float _distanceMoved;
    private Area2D _chaseArea;
    private Me _player;

    public AnimationTree AnimationTree { get; private set; }

    private static float RandomWanderDistance => 150f;
    private static float AttackRadius => 40f;
    private static string RunBlendPositionPath => "parameters/RunBlendSpace/blend_position";
    private static string AttackBlendPositionPath => "parameters/AttackBlendSpace/blend_position";
    private int Health { get; set; }
    private static float Speed => 100f;
    private Random Rng { get; } = new();

    public override void _Ready()
    {
        SetupBehavior();

        _chaseArea = GetNode<Area2D>("%ChaseArea");
        AnimationTree = GetNode<AnimationTree>("%AnimationTree");
        
        ChangeDirectionRandom();

        _chaseArea.BodyEntered += HandleBodyEntered;
        _chaseArea.BodyExited += HandleBodyExited;
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        
        _chaseArea.BodyEntered -= HandleBodyEntered;
        _chaseArea.BodyExited -= HandleBodyExited;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        /*BTResult status = */_behavior.BehaviorManager.Cycle();
    }

    private void SetupBehavior()
    {
        Dictionary<string, Delegate> actions = new()
        {
            ["Wandering"] = Wandering,
            ["Chasing"] = Chasing,
            ["InCombat"] = InCombat,
            ["Wander"] = Wander,
            ["Chase"] = Chase,
        };
        _behavior = new BehaviorSkeleton(actions, () => BTResult.Failure);
    }

    // TODO: Put each action in its own class that derives from BTAction?
    // Same with conditions?
    private BTResult Wandering()
    {
        bool wandering = _player == null;
        return wandering ? BTResult.Success : BTResult.Failure;
    }

    private BTResult Chasing()
    {
        bool chasing = _player != null && (_player.GlobalPosition - GlobalPosition).Length() > AttackRadius;
        return chasing ? BTResult.Success : BTResult.Failure;
    }

    private BTResult InCombat()
    {
        bool inCombat = _player != null && (_player.GlobalPosition - GlobalPosition).Length() <= AttackRadius;
        return inCombat ? BTResult.Success : BTResult.Failure;
    }

    private BTResult Wander()
    {
        Vector2 oldPosition = GlobalPosition;
        MoveAndSlide();
        Vector2 newPosition = GlobalPosition;
        
        _distanceMoved += (newPosition - oldPosition).Length();
        if (_distanceMoved > RandomWanderDistance)
        {
            _distanceMoved = 0;
            ChangeDirectionRandom();
        }
        
        return BTResult.Running;
    }

    private BTResult Chase()
    {
        Vector2 direction = (_player.GlobalPosition - GlobalPosition).Normalized();
        Velocity = direction * Speed;
        MoveAndSlide();
        SetBlendPositions(direction);
        
        return BTResult.Running;
    }

    private void HandleBodyEntered(Node2D body)
    {
        if (body is Me me)
        {
            _player = me;
        }
    }

    private void HandleBodyExited(Node2D body)
    {
        if (body is Me)
        {
            _player = null;
        }
    }

    private void ChangeDirectionRandom()
    {
        float angle = Rng.RandomFRange(-MathF.PI, MathF.PI);
        Vector2 direction =  Vector2.FromAngle(angle);
        Velocity = direction * Speed;
        
        SetBlendPositions(direction);
    }

    private void SetBlendPositions(Vector2 direction)
    {
        AnimationTree.Set(RunBlendPositionPath, direction);
        AnimationTree.Set(AttackBlendPositionPath, direction);
    }
}
