using System;
using System.Collections.Generic;

namespace SproutandMeRPG.characters.behavior_tree;

public abstract class Behavior
{
    public BTManager BehaviorManager { get; protected set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="actions">sfa</param>
    /// <param name="dangerInterrupt"></param>
    protected Behavior(
        Dictionary<string, Delegate> actions,
        Func<BTResult> dangerInterrupt)
    {
        //BehaviorManager = new BTManager(manager, dangerInterrupt);
        BehaviorManager = new BTManager(tree =>
            {
                BTBuilder builder = CreateBuilder(tree, actions);
                return builder.Build();
            }, 
            dangerInterrupt);
    }
    
    protected abstract BTBuilder CreateBuilder(BTManager tree, Dictionary<string, Delegate> actions);
}