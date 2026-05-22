using Godot;

public class Party
{
    private Character Sprout { get; }
    private Character Me { get; }
    private Camera Camera { get; }
    

    public Party(Character sprout, Character me, Camera camera)
    {
        Sprout = sprout;
        Me = me;
        Camera = camera;
        
        Camera.FollowTarget = Me;
        Me.Selected = true;
        Sprout.Selected = false;
    }

    public void Switch()
    {
        if (Camera.FollowTarget == Me)
        {
            Camera.FollowTarget = Sprout;
            Me.Selected = false;
            Sprout.Selected = true;
        }
        else if (Camera.FollowTarget == Sprout)
        {
            Camera.FollowTarget = Me;
            Me.Selected = true;
            Sprout.Selected = false;
        }
        else
        {
            GD.PushWarning("No follow target on camera");
        }
    }
}
