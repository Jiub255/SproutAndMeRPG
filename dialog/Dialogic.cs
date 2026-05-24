using Godot;

public partial class Dialogic : GodotObject
{
    public Dialogic(Node dialogicGameHandler)
    {
        DialogicGameHandler = dialogicGameHandler;
    }
    
    public bool TimelineNull
    {
        get
        {
            Variant timeline = DialogicGameHandler.Get("current_timeline");
            return timeline.VariantType == Variant.Type.Nil;
        }
    }

    private Node DialogicGameHandler { get; }
    
    public void Start(string timelineName)
    {
        DialogicGameHandler.Call("start",  timelineName);
    }
}
