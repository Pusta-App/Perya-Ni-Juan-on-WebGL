using System;
using UnityEngine;
using UnityEngine.UI;
using HungryCannibal.UnderTheSeaUIKit.Dialogs;

public class PNJ_DialogManager : MonoBehaviour
{
    public PW_ResultInfo result;
    public DialogBehaviour store;
    public DialogBehaviour about;
    public DialogBehaviour more;
    public DialogBehaviour settings;
    public DialogBehaviour rewards;
    public DialogBehaviour howToPlay;
    public Text howToPlayDescription;
    public DialogBehaviour notify;
    public Text notifyDescription;
    public DialogBehaviour confirm;
    public Action<bool> callbackConfirmed;

    public bool isVisible {
        get {
            bool oneIsVisible = false;
            if( result.dialog.isVisible ) { oneIsVisible = true; }
            if( store.isVisible ) { oneIsVisible = true; }
            if( about.isVisible ) { oneIsVisible = true; }
            if( more.isVisible ) { oneIsVisible = true; }
            if( settings.isVisible ) { oneIsVisible = true; }
            if( rewards.isVisible ) { oneIsVisible = true; }
            if( howToPlay.isVisible ) { oneIsVisible = true; }
            if( notify.isVisible ) { oneIsVisible = true; }
            if( confirm.isVisible ) { oneIsVisible = true; }
            return oneIsVisible;
        }
    }

    public void ShowHowToPlay(string htpInfo)
    {
        howToPlayDescription.text = htpInfo;
        if(CustomReference.Access.gameState == PNJ_GameState.ColorGame)
        {
            CustomReference.Access.colorGame.state = PNJ_PP_State.WANDER;
        }

        else if(CustomReference.Access.gameState == PNJ_GameState.DropBall)
        {
            CustomReference.Access.dropBall.state = PNJ_PP_State.WANDER;
        }

        else if(CustomReference.Access.gameState == PNJ_GameState.SabongWheel)
        {
            CustomReference.Access.sabongWheel.isReady = false;
        }
        howToPlay.Show();
    }

    public Action notifyEvent = null;
    public void ShowNotify(string description, Action listener)
    {
        notifyEvent = listener;
        notifyDescription.text = description;
        notify.Show();
    }

    public void CloseNotify()
    {
        if(notifyEvent != null)
        {
            notifyEvent.Invoke();
            notifyEvent = null;
        } 

        notify.Hide();
    }


    public void ShowConfirm(Action<bool> callbackConfirm)
    {
        callbackConfirmed = callbackConfirm;
    }

    public void Confirm(bool yes)
    {
        confirm.Hide();
        if(callbackConfirmed != null)
        {
            callbackConfirmed(yes);
        }
    }
}
