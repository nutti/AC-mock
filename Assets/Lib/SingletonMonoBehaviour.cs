using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    public static T Instance
    {
        get
        {
            instance = FindObjectOfType<T>();
            if (instance == null) {
                Debug.LogError("Instance of " + typeof(T) + " is needed in this scene.");
            }
            return instance;
        }
    }
}
