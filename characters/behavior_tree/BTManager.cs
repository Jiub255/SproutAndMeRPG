using System;
using System.Threading;
using Godot;

namespace SproutandMeRPG.characters.behavior_tree;

public partial class BTManager : Node
{
    private int _idCounter;
    public int? CurrentRunningId { get; set; }
    public string? CurrentRunningName { get; set; }
    public BTBase RootNode { get; set; }

    private readonly Func<BTResult> _dangerInterrupt;

    public BTManager(Func<BTManager, BTBase> manager, Func<BTResult> dangerInterrupt)
    {
        _dangerInterrupt = dangerInterrupt;
        RootNode = manager?.Invoke(this)
                   ?? throw new ArgumentNullException(nameof(manager), "Behavior tree manager cannot return null.");
    }
    
    /// <summary>
    /// Thread-safe way to get next unique id
    /// </summary>
    public int GetNextId()
    {
        return Interlocked.Increment(ref _idCounter);
    }

    /// <summary>
    /// Executes the behavior tree starting from the root node and handles danger interruptions.
    /// </summary>
    /// <returns>A <see cref="BTResult"/> indicating the outcome of the behavior execution. </returns>
    public BTResult Cycle()
    {
        if (RootNode == null) return BTResult.Failure;
        
        // Call danger interrupt function each turn
        if (_dangerInterrupt() == BTResult.Success)
        {
            CurrentRunningId = null;
            CurrentRunningName = null;
        }
        return RootNode.Execute();
    }
}