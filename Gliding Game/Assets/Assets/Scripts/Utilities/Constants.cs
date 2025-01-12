public class Constants
{
    public const string CAMERA_TWEEN_ID = "CameraTweenID";
    public const string ROCKETMAN_TWEEN_ID = "RocketmanTweenID";
    public const string IDLE = "Idle";

    public const int INIT_PLANE_COUNT = 5;
    public const int MIN_JUMPER_COUNT = 25;
    public const int MAX_JUMPER_COUNT = 40;

    public const int PLANE_Z_SIZE_MULT = 10;

    public const float MIN_Y_LIMIT = .5f;
}
public class TAGS
{
    public const string PLAYER = "Player";
    public const string GROUND = "Ground";
    public const string JUMPER = "Jumper";
    public const string DOUBLE_JUMPER = "DoubleJumper";
}
public class SaveKeys
{
    public const string BEST_SCORE = "BestScore";
}
public enum CameraStates
{
    InStick,
    OnFly,
}
public enum RocketmanStates
{
    Idle,
    Fly,
    Gliding,
    Fall,
}
public class StickAnimatorParams
{
    public const string BendAnimName = "Anim_Bend";
    public const string ReleaseTrigger = "Release";
}

public class RocketmanAnimatorParams
{
    public const string CLOSE_WINGS_NAME = "Anim_CloseWings";
    public const string OPEN_WINGS_NAME = "Anim_OpenWings";
}