using System;
using System.Collections.Generic;

public class BTBuilder
{
    private readonly Stack<BTComposite> _stack = new();
    private BTBase _root;
    private readonly BTManager _manager;

    public BTBuilder(BTManager manager)
    {
        _manager = manager;
    }

    public BTBuilder Selector(string name)
    {
        BTSelector node = new(name, _manager);
        AddNode(node);
        _stack.Push(node);
        return this;
    }

    public BTBuilder Sequence(string name)
    {
        BTSequence node = new(name, _manager);
        AddNode(node);
        _stack.Push(node);
        return this;
    }

    public BTBuilder Condition(string name, Func<BTResult> predicate)
    {
        BTCondition node = new(name, _manager, predicate);
        AddNode(node);
        return this;
    }

    public BTBuilder Action(string name, Func<BTResult> action)
    {
        BTAction node = new(name, _manager, action);
        AddNode(node);
        return this;
    }

    /// <summary>
    /// Needed to end any composite nodes (selectors or sequences)
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public BTBuilder End()
    {
        if (_stack.Count == 0)
        {
            throw new InvalidOperationException("No composite node to end. The stack is empty.");
        }

        _stack.Pop();
        return this;
    }

    /// <summary>
    /// Completes the tree and returns the root node.
    /// </summary>
    /// <returns>The root node of the behavior tree as a <see cref="BTBase"/> instance.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public BTBase Build()
    {
        if (_root == null)
        {
            throw new InvalidOperationException("Cannot build a behavior tree with a root node.");
        }
        
        if (_stack.Count > 0)
        {
            throw new InvalidOperationException("Unbalanced tree: some composite nodes were not closed with end");
        }
        
        return _root;
    }

    private void AddNode(BTBase node)
    {
        if (_stack.Count > 0)
        {
            _stack.Peek().AddChild(node);
        }
        else
        {
            _root = node;
        }
    }
}
