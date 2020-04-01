using UnityEngine;

public class BC_UTILS_Destroyer : MonoBehaviour
{
    public GameObject target;
    public float delay;

    void OnEnable()
    {
        Destroy(target, delay);
    }
}
