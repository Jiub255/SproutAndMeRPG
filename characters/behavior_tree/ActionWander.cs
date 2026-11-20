using Godot;
using System;
using SproutandMeRPG.characters.behavior_tree;
using SproutandMeRPG.misc;

public partial class ActionWander(string name, BTManager tree, Func<BTResult> action) : BTAction(name, tree, action)
{
    /*private float _distanceMoved;
    private static float Speed => 100f;
    private static float RandomWanderDistance => 150f;
    private Random Rng { get; } = new();

    public override BTResult Execute()
    {
        Tree.CurrentRunningName = Name;
        return Wander();
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
    }*/
}
