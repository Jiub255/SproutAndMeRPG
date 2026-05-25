using Godot;
using System;

public partial class Dialogic : GodotObject
{
   // public event Action<Variant> SignalEvent;
    public event Action TimelineEnded;

    public Dialogic(Node dialogicGameHandler)
    {
        DialogicGameHandler = dialogicGameHandler;

        //DialogicGameHandler.Connect("signal_event", Callable.From<Variant>(InvokeSignalEvent));
        DialogicGameHandler.Connect("timeline_ended", Callable.From(InvokeTimelineEnded));
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
    
    public Node Start(string timelineName)
    {
        return (Node)DialogicGameHandler.Call("start",  timelineName);
    }

    // private void InvokeSignalEvent(Variant variant)
    // {
    //     SignalEvent?.Invoke(variant);
    // }

    private void InvokeTimelineEnded()
    {
        TimelineEnded?.Invoke();
    }
}
