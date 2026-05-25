using System;

namespace SproutandMeRPG.characters.behavior_tree;

public class BTAction(string name, BTManager tree, Func<BTResult> action) : BTBase(name, tree)
{
    public override BTResult Execute()
    {
        Tree.CurrentRunningName = Name;
        return action();
    }
}