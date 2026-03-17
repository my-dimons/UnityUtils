using UnityEngine;

public class SingletonExample : MonoBehaviour
{
    public static SingletonExample Instance { get; private set; }

    [Tooltip("If true, this object will persist through all scenes")]
    public bool dontDestroyOnLoad = true;
     
    private void Awake()
    {
        if (Instance == null) Instance = this; else Destroy(gameObject);

        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}