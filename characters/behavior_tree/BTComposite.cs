using System.Collections.Generic;

namespace SproutandMeRPG.characters.behavior_tree;

public abstract class BTComposite(string name, BTManager tree) : BTBase(name, tree)
{
    protected List<BTBase> Children { get; } = [];

    public void AddChild(BTBase child)
    {
        Children.Add(child);
    }
}