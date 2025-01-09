public class Constants
{
    public const string CAMERA_TWEEN_ID = "CameraTweenID";
    public const string ROCKETMAN_TWEEN_ID = "RocketmanTweenID";
}
public enum CameraStates
{
    InStick,
    OnFly,
}
public enum PlayerStates
{
    InStick,
    InAir,
    InGliding,
    Fall,
}
public class StickAnimatorParams
{
    public const string BendAnimName = "Anim_Bend";
    public const string ReleaseTrigger = "Release";
}