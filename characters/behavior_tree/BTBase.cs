public abstract class BTBase
{
    public string Name { get; }
    public int Id { get; }
    protected BTManager Tree { get; private set; }

    protected BTBase(string name, BTManager tree)
    {
        Name = name;
        Tree = tree;
        Id = Tree.GetNextId();
    }

    public abstract BTResult Execute();
}
