using System;

namespace SproutandMeRPG.characters.behavior_tree;

public class BTSequence(string name, BTManager tree) : BTComposite(name, tree)
{
    private readonly BTManager _tree = tree;

    public override BTResult Execute()
    {
        foreach (BTBase child in Children)
        {
            BTResult result = child.Execute();
            switch (result)
            {
                case BTResult.Running:
                {
                    if (_tree.CurrentRunningId == null)
                    {
                        _tree.CurrentRunningId = child.Id;
                        _tree.CurrentRunningName = child.Name;
                    }
                    return BTResult.Running;
                }
                case BTResult.Failure:
                {
                    ResetChildren();
                    return BTResult.Failure;
                }
                case BTResult.Success:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        ResetChildren();
        return BTResult.Success;
    }

    private void ResetChildren()
    {
        _tree.CurrentRunningId = null;
    }
}