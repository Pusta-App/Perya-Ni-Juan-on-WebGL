
using UnityEngine;
using UnityEngine.UI;

public class PNJ_PP_UserMenu : MonoBehaviour
{
    [Header("DROP AND RSET MECHANISM")]
    public Image drosetIcon;
    public Sprite dropIcon;
    public Sprite resetIcon;

    [Header("LIST OF IMPORTANT BUTTONS")]
    public Button help;
    public Button droset;
    public Button withdraw;
    public Button home;
    public Button settings;

    void OnEnable()
    {
        withdraw.interactable = false;
    }

    public bool isOkayToReset {
        get {
            return droset.interactable && drosetIcon.sprite.Equals(resetIcon) ? true : false;
        }
    }
    
    public void DrosetOnDrop(bool yes, bool interactable)
    {
        droset.interactable = interactable;
        if(yes)
        {
            drosetIcon.sprite = dropIcon;
        }

        else
        {
            drosetIcon.sprite = resetIcon;
        }
    }

    public void BallHadBeenDrop()
    {
        help.interactable = false;
        withdraw.interactable = false;
        home.interactable = false;
        settings.interactable = false;
    }

    public void GameHasBeenReset(bool normal)
    {
        DrosetOnDrop(true, true);
        
        if(normal)
        {
            help.interactable = true;
            withdraw.interactable = false;
            home.interactable = true;
            settings.interactable = true;
        }        
    }
}
