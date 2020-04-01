
using UnityEngine;
using HungryCannibal.UnderTheSeaUIKit.Dialogs;

public class PNJ_GlobalCalls : MonoBehaviour
{
    public void OnPlayUnitySkipped()
    {
        // //MultiAds.Access.UnityAds_ShowSkippedVideo( (UAdsEvent events) => {
        //     if(events == UAdsEvent.VideoFinished || events == UAdsEvent.VideoSkipped)
        //     {
        //         CustomReference.Access.userData.userInfo.adStat.unity_skipped += 1;
        //     }
        // });
    }

    public void PlayGames_SignIn()
    {
        //CustomReference.Access.playGames.Authenticate();
    }

    public void PlayGames_SignOut()
    {
        //CustomReference.Access.playGames.SignOut();
    }

    public void PlayGames_ShowAchievement()
    {
        //CustomReference.Access.playGames.ShowAchievementUI();
    }

    public void PlayGames_Leaderboard()
    {
        //CustomReference.Access.playGames.ShowLeaderboardUI("CgkI65TlqOsREAIQBQ");
    }

    public void PlayGames_ColorGame()
    {
        //CustomReference.Access.playGames.ShowLeaderboardUI("CgkI65TlqOsREAIQGQ");
    }

    public void PlayGames_BallDrop()
    {
        //CustomReference.Access.playGames.ShowLeaderboardUI("CgkI65TlqOsREAIQGg");
    }

    public void PlayGames_SabongRoulette()
    {
        //CustomReference.Access.playGames.ShowLeaderboardUI("CgkI65TlqOsREAIQGw");
    }

    public void Firebase_CustomEvent(string key)
    {
        //CustomReference.Access.firebase.CustomEvent(key);
    }

    public void RateOnGooglePlay()
    {
        //CustomReference.Access.playGames.ReportAchievement("CgkI65TlqOsREAIQJQ");
        CustomReference.Access.openURL.RateThisGame();
    }

    public void ShareOnFacebook()
    {
        //CustomReference.Access.playGames.ReportAchievement("CgkI65TlqOsREAIQJg");
        CustomReference.Access.openURL.ShareOnFacebook();
    }

    public void RequestGame()
    {
        //CustomReference.Access.playGames.ReportAchievement("CgkI65TlqOsREAIQJA");
        CustomReference.Access.openURL.RequestGame();
    }

    public void Dialog_Settings()
    {
        ////CustomReference.Access.playGames.RecordEvent("CgkI65TlqOsREAIQHw");
        CustomReference.Access.dialogs.settings.Show();

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
    }

    public void Dialog_About()
    {
        //CustomReference.Access.playGames.RecordEvent("CgkI65TlqOsREAIQFw");
        CustomReference.Access.dialogs.about.Show();
    }

    public void Dialog_More()
    {
        ////CustomReference.Access.playGames.RecordEvent("CgkI65TlqOsREAIQIA");
        CustomReference.Access.dialogs.more.Show();
    }

    public void Dialog_Reward()
    {
        ////CustomReference.Access.playGames.RecordEvent("CgkI65TlqOsREAIQJw");
        CustomReference.Access.dialogs.rewards.Show();
    }

    public void Dialog_HowToPlay(BC_UTILS_TextField textField)
    {
        CustomReference.Access.dialogs.ShowHowToPlay(textField.texField);
    }

    public void PlaySound(string childName)
    {
        CustomReference.Access.soundMix.Play(childName);
    }

    public void Ads_AdmobInterstitial()
    {
        //MultiAds.Access.Interstitial_Show( );
    }

    public void Ads_AdmobReward()
    {
        //MultiAds.Access.RewardAds_Show( );
    }

    public void Ads_ShowUnitySkipped()
    {
        //MultiAds.Access.UnityAds_ShowSkippedVideo( null );
    }

    public void PlayGames_Event(string strindId)
    {
        ////CustomReference.Access.playGames.RecordEvent(strindId);
    }

    public void Ads_ShowUnityRewardAds(int reward)
    {
        
    }

    public void OnDialogClosed( DialogBehaviour dialog )
    {
        if(CustomReference.Access.gameState == PNJ_GameState.ColorGame)
        {
            if(CustomReference.Access.colorGame.state == PNJ_PP_State.TORESET)
            {
                CustomReference.Access.colorGame.releaseControl.SetToResetIcon(true);
            }

            else 
            {
                CustomReference.Access.colorGame.state = PNJ_PP_State.BETTING;
            }
        }
        
        else if(CustomReference.Access.gameState == PNJ_GameState.DropBall)
        {
            CustomReference.Access.dropBall.userMenu.droset.interactable = true;
            
            if(CustomReference.Access.dropBall.ballGroup.curBall > 0)
            {
                CustomReference.Access.dropBall.state = PNJ_PP_State.TORESET;
            }

            else
            {
                CustomReference.Access.dropBall.state = PNJ_PP_State.BETTING;
            }
        }

        else if(CustomReference.Access.gameState == PNJ_GameState.SabongWheel)
        {
            CustomReference.Access.sabongWheel.isReady = true;
        }
    
        if( dialog != null )
        {
            dialog.Hide(); //Instead of calling hide on button, automaticlly close target dialog.
        }
    }

    public void OnResultDialogContinue()
    {
        if(CustomReference.Access.gameState == PNJ_GameState.ColorGame)
        {
            CustomReference.Access.colorGame.Call_Reset(false);
        }
        
        else if(CustomReference.Access.gameState == PNJ_GameState.DropBall)
        {
            if(CustomReference.Access.dropBall.ballGroup.curBall > 0)
            {
                CustomReference.Access.dropBall.state = PNJ_PP_State.TORESET;
            }

            else
            {
                CustomReference.Access.dropBall.state = PNJ_PP_State.BETTING;
            }
        }

        else if(CustomReference.Access.gameState == PNJ_GameState.SabongWheel)
        {
            CustomReference.Access.sabongWheel.toReset = true;
        }
    
        CustomReference.Access.dialogs.result.HideDisplay();
    }

    public void Call_DialogBlocker(GameObject dialogBlocker)
    {
        bool canDisableDialogBlocker = false;

        switch ( CustomReference.Access.gameState )
        {
            case PNJ_GameState.ColorGame:
                canDisableDialogBlocker = CustomReference.Access.colorGame.state == PNJ_PP_State.WANDER ? true : false;
                break;
            case PNJ_GameState.DropBall:
                canDisableDialogBlocker = CustomReference.Access.dropBall.state == PNJ_PP_State.WANDER ? true : false;
                break;
            case PNJ_GameState.SabongWheel:
                canDisableDialogBlocker = !CustomReference.Access.sabongWheel.isReady ? true : false;
                break;
            default:
                break;
        }

        if( canDisableDialogBlocker )
        {
            OnDialogClosed(null);
        }

        dialogBlocker.SetActive(false);
    }
}