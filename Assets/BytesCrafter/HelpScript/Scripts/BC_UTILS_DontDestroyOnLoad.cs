using UnityEngine;

public class BC_UTILS_DontDestroyOnLoad : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
