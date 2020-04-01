using System.Collections.Generic;
using UnityEngine;

public class BC_UTILS_LoopRect : MonoBehaviour
{
    public Transform shownRefs;
    public Transform hideRefs;
    public List<Transform> items;
    public bool clickToRefreshItems = false;
    void OnValidate()
    {
        if( clickToRefreshItems )
        {
            items.Clear();
            for(int i = 0; i < transform.childCount; i++)
            {
                items.Add( transform.GetChild(i) );
            }
            clickToRefreshItems = false;
        }
    }

    public void PrevItem()
    {
        int index = 0;
        Transform cur = transform.GetChild( index );
        cur.SetAsLastSibling();
        cur.transform.position = hideRefs.position;

        Transform toShow = transform.GetChild( 0 );
        toShow.position = shownRefs.position;
        CustomReference.Access.soundMix.Play("Swish");
    }

    public void NextItem()
    {
        int index = transform.childCount - 1;
        Transform cur = transform.GetChild( index );
        cur.SetAsFirstSibling();
        cur.transform.position = shownRefs.position;

        Transform toHide = transform.GetChild( 1 );
        toHide.position = hideRefs.position;
        CustomReference.Access.soundMix.Play("Swish");
    }
}
