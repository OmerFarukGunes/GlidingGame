using System;
using System.Linq;
using UnityEngine;
public class GameManager : Singleton<GameManager>
{
    public static event Action OnLevelFailed;
    public static event Action OnLevelLoaded;
    public static event Action OnLevelRestarted;
    public static event Action<float> OnLaunched;
    public static event Func<Transform> OnGetRocketmanParent;

    [SerializeField] private Transform managers;
    protected override void Awake()
    {
        Application.targetFrameRate = 60;
        base.Awake();
        InitializeManagers();
    }
    private void InitializeManagers()
    {
        managers.GetComponentsInChildren<CustomBehaviour>().ToList().ForEach(manager => manager.Initialize());
    }
    public static void LevelLoaded() => OnLevelLoaded?.Invoke();
    public static void LevelFailed() => OnLevelFailed?.Invoke();
    public static void LevelRestarted() => OnLevelRestarted?.Invoke();
    public static void Launch(float pullAmount) => OnLaunched?.Invoke(pullAmount);
    public static Transform GetRocketmanParent()
    {
        return OnGetRocketmanParent?.Invoke();
    }
}