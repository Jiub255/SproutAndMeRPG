using Godot;

public class Party
{
    private Character Sprout { get; }
    private Character Me { get; }
    
    private Character ActiveChar { get; set; }

    public Party(Character sprout, Character me)
    {
        Sprout = sprout;
        Me = me;
        ActiveChar = Me;
        Me.Selected = true;
        Sprout.Selected = false;
    }

    public void Switch()
    {
        if (ActiveChar == Me)
        {
            ActiveChar = Sprout;
            Me.Selected = false;
            Sprout.Selected = true;
        }
        else if (ActiveChar == Sprout)
        {
            ActiveChar = Me;
            Me.Selected = true;
            Sprout.Selected = false;
        }
        else
        {
            GD.PushWarning("No active char");
        }
    }
}
