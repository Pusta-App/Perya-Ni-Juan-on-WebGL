using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BC_UTILS_Instantiator : MonoBehaviour
{
    public KeyCode keyPress = KeyCode.Space;
    public Transform target;
    public Transform origin;
    public Vector3 variancePos;
    void Update()
    {
        if( Input.GetKeyDown(keyPress) )
        {
            Instantiate();
        }
    }

    public void Instantiate()
    {
        Instantiates();
    }

    public Transform Instantiates()
    {
        Vector3 random = new Vector3(
                origin.position.x + Random.Range(-variancePos.x, variancePos.x), 
                origin.position.y + Random.Range(-variancePos.y, variancePos.y), 
                origin.position.z + Random.Range(-variancePos.z, variancePos.z));
        return Instantiate(target, random, Quaternion.identity);
    }
}
