public class Constants
{
    public const string CAMERA_TWEEN_ID = "CameraTweenID";
    public const string ROCKETMAN_TWEEN_ID = "RocketmanTweenID";
    public const string IDLE = "Idle";
}
public class TAGS
{
    public const string PLAYER = "Player";
    public const string GROUND = "Ground";
    public const string JUMPER = "Jumper";
    public const string DOUBLE_JUMPER = "DoubleJumper";
}
public enum CameraStates
{
    InStick,
    OnFly,
}
public enum RocketmanStates
{
    InStick,
    Fly,
    Gliding,
    Fall,
}
public class StickAnimatorParams
{
    public const string BendAnimName = "Anim_Bend";
    public const string ReleaseTrigger = "Release";
}
 