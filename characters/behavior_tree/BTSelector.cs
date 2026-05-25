using System;

public class BTSelector(string name, BTManager tree) : BTComposite(name, tree)
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
                case BTResult.Success:
                {
                    ResetChildren();
                    return BTResult.Success;
                }
                case BTResult.Failure:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        ResetChildren();
        return BTResult.Failure;
    }

    private void ResetChildren()
    {
        _tree.CurrentRunningId = null;
    }
}
