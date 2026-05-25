using System;

public class BTCondition(string name, BTManager tree, Func<BTResult> predicate) : BTBase(name, tree)
{
    public override BTResult Execute()
    {
        Tree.CurrentRunningName = Name;
        return predicate();
    }
}
