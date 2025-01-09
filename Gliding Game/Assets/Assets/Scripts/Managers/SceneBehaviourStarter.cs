using System.Linq;
public class SceneBehaviourStarter : CustomBehaviour
{
    public static SceneBehaviourStarter Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        Initailize();
    }
    private void Initailize()
    {
        transform.GetComponentsInChildren<CustomBehaviour>().ToList().ForEach(c => c.Initialize());
    }
}