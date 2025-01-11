public class Constants
{
    public const string CAMERA_TWEEN_ID = "CameraTweenID";
    public const string ROCKETMAN_TWEEN_ID = "RocketmanTweenID";
    public const string IDLE = "Idle";
}
public enum CameraStates
{
    InStick,
    OnFly,
}
public enum PlayerStates
{
    InStick,
    Launched,
    InGliding,
    AfterGliding,
    Fall,
}
public class StickAnimatorParams
{
    public const string BendAnimName = "Anim_Bend";
    public const string ReleaseTrigger = "Release";
}
 