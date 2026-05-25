using Godot;

namespace SproutandMeRPG.characters.player_characters;

public class Party
{
    private PlayerCharacter Sprout { get; }
    private PlayerCharacter Me { get; }
    private Camera Camera { get; }
    

    public Party(PlayerCharacter sprout, PlayerCharacter me, Camera camera)
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

    public void ToggleFollower()
    {
        Sprout.Following = !Sprout.Following;
        Me.Following = !Me.Following;
    }
}