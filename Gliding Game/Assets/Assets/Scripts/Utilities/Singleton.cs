using UnityEngine;
public class Singleton<T> : CustomBehaviour where T : CustomBehaviour
{
    private static T mInstance;
    private static readonly object mInstanceLock = new object();
    private static bool mQuitting = false;
    public static T Instance
    {
        get
        {
            lock (mInstanceLock)
            {
                if (mInstance != null || mQuitting) return mInstance;
                mInstance = GameObject.FindObjectOfType<T>();
                if (mInstance != null) return mInstance;
                var go = new GameObject(typeof(T).ToString());
                mInstance = go.AddComponent<T>();
                DontDestroyOnLoad(mInstance.gameObject);
                return mInstance;
            }
        }
    }
    protected virtual void Awake()
    {
        if (mInstance == null) mInstance = gameObject.GetComponent<T>();
        else if (mInstance.GetInstanceID() != GetInstanceID())
        {
            Destroy(gameObject);
            throw new System.Exception($"Instance of {GetType().FullName} already exists, removing {ToString()}");
        }
    }
    protected virtual void OnApplicationQuit()
    {
        mQuitting = true;
    }
}