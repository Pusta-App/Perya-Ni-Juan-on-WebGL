
using UnityEngine;

public enum PNJ_PP_SockType
{
    RED, WHITE, STAR, NONE
}

public class PNJ_PP_BallSocket : MonoBehaviour
{
    public PNJ_PP_SockType sockType = PNJ_PP_SockType.WHITE;

    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PNJ_PP_BallScript>().FinelSet(sockType);
        CustomReference.Access.Debugs(sockType.ToString());
    }
}
