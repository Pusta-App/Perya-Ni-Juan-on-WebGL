
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PNJ_PP_BallScript : MonoBehaviour
{
    public bool isRestingOnPlatfrom = false;
    public PNJ_PP_SockType socketOn;
    public Rigidbody rigidBody;
    void OnValidate()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void FinelSet(PNJ_PP_SockType socketOnReceived)
    {
        socketOn = socketOnReceived;
        isRestingOnPlatfrom = true;
    }

    public void Reset()
    {
        socketOn = PNJ_PP_SockType.NONE;
        isRestingOnPlatfrom = false;
    }
}