
using UnityEngine;
using UnityEngine.UI;

public class PNJ_UTILS_BtnSignedIn : MonoBehaviour
{
    public Button target;
    void OnValidate()
    {
        if( target == null )
        {
            target = GetComponent<Button>();
        }
    }

    void OnEnable()
    {
        //target.interactable = CustomReference.Access.playGames.isSignedIn;
    }
}
