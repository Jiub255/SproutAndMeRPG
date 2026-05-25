using Godot;
using SproutandMeRPG.characters.behavior_tree;
using SproutandMeRPG.characters.player_characters.me;
using SproutandMeRPG.misc;
using System;
using System.Collections.Generic;

namespace SproutandMeRPG.characters.non_player_characters.enemies;

public partial class Skeleton : CharacterBody2D
{
    public enum StateSkeleton
    {
        Wander,
        Chase,
        Combat,
    }
    private StateSkeleton _state;
    private BehaviorSkeleton _behavior;
    private float _distanceMoved;
    private Area2D _chaseArea;
    private Me _player;

    public BTResult Wandering => _state == StateSkeleton.Wander ? BTResult.Success :  BTResult.Failure;
    public BTResult Chasing => _state == StateSkeleton.Chase ? BTResult.Success :  BTResult.Failure;
    public BTResult InCombat => _state == StateSkeleton.Combat ? BTResult.Success :  BTResult.Failure;
    public BTResult PlayerInChaseRange => _player == null ? BTResult.Failure :  BTResult.Success;
    public AnimationTree AnimationTree { get; private set; }
    
    private static string RunBlendPositionPath => "parameters/RunBlendSpace/blend_position";
    private static string AttackBlendPositionPath => "parameters/AttackBlendSpace/blend_position";
    private int Health { get; set; }
    private static float Speed => 10f;
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

        BTResult status = _behavior.BehaviorManager.Cycle();
    }

    public BTResult ChangeState(StateSkeleton newState)
    {
        GD.Print($"changing from {_state} to {newState}");
        _state = newState;
        return BTResult.Success;
    }

    // TODO: How to get delta here?
    public BTResult Wander()
    {
        Vector2 oldPosition = GlobalPosition;
        MoveAndSlide();
        Vector2 newPosition = GlobalPosition;
        _distanceMoved += (newPosition - oldPosition).Length();
        if (_distanceMoved > 150)
        {
            _distanceMoved = 0;
            ChangeDirectionRandom();
        }
        
        // TODO: How to handle transitioning to chase state? In the behavior or here?
        return /*PlayerInChaseRange ? BTResult.Success : */BTResult.Running;
    }

    public BTResult Chase()
    {
        if (_player == null)
        {
            ChangeState(StateSkeleton.Wander);
            return BTResult.Failure;
        }
        Vector2 displacement = _player.GlobalPosition - GlobalPosition;
        Vector2 direction = displacement.Normalized();
        float distance = displacement.Length();
        Velocity = direction * Speed;
        MoveAndSlide();
        SetBlendPositions(direction);
        
        if (distance > 20)
        {
            return BTResult.Running;
        }
        ChangeState(StateSkeleton.Combat);
        return BTResult.Success;
    }

    private void SetupBehavior()
    {
        Dictionary<string, Delegate> actions = new()
        {
            ["Wandering"] = () => Wandering,
            ["Chasing"] = () => Chasing,
            ["InCombat"] = () => InCombat,
            ["InChaseRange"] = () => PlayerInChaseRange,
            ["ChangeToChase"] = () => ChangeState(StateSkeleton.Chase),
            ["Wander"] = Wander,
            ["Chase"] = Chase,
        };
        _behavior = new BehaviorSkeleton(actions, () => BTResult.Failure);
    }

    private void HandleBodyEntered(Node2D body)
    {
        if (body is Me me) _player = me;
    }

    private void HandleBodyExited(Node2D body)
    {
        if (body is Me) _player = null;
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


    // Selector (which general state you're in)
    // Wander state (sequence)   <--  condition nodes for the state checks, then selectors/sequences after.
        // Action (wander in random direction for random duration)
        // Condition (player in chase range)
            // Action (Go toward player (go to chase state))
    // Chase state (sequence)
        // Action (Go toward player)
        // Condition (player in attack range)
            // Action (go to combat state)
    // Combat state (selector)
        // Condition (0 hp)
            // Action (die)
        // Condition (player out of range)
            // Action (go to chase state)
        // Condition (attack timer done)
            // Sequence
                // Action (attack player)
                // Action (reset attack timer)
                // Condition (player dead)
                    // Action (return to wander state)
}
