using UnityEngine;

public class DontDestroyOnLoadObject : MonoBehaviour
{
    void Awake()
    {
        // Keep this object when loading new scenes
        DontDestroyOnLoad(gameObject);
    }
}