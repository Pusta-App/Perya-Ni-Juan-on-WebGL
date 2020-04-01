
using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class PNJ_ListenerHooker : MonoBehaviour
{
    public AudioListener listener;
    void OnValidate()
    {
        if(listener == null)
        {
            listener = GetComponent<AudioListener>();
        }
    }

    void OnEnable()
    {
        if(CustomReference.Access.soundMix != null && listener != null)
        {
            CustomReference.Access.soundMix.audioListener = listener;
        }
    }
}
