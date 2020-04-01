using UnityEngine;
using System;
using System.Collections;

public class PNJ_SDK_MultiAds : MonoBehaviour
{

    void Awake()
    {


        //StartCoroutine (ShowBannerWhenReady ());
    }

    public float delay = 2f;
    private float timer = 0f;
    private bool intAdsDone = false;
    private bool rewAdsDone = false;
    private bool allDone = false;
    void Update()
    {
        if( !allDone )
        {
            timer += Time.deltaTime;

            if( timer >= delay )
            {

                if( !intAdsDone )
                {
                        intAdsDone = true;
                }

                else
                {
                    if( !rewAdsDone )
                    {
                        rewAdsDone = true;
                        allDone = true;
                    }
                }

                timer = 0f;
            }
        }
    }

    public void RequestAndShowBanner()
    {
        //MultiAds.Access.BannerView_Request (BannerSize.Leaderboard, BannerPos.Top, true);
    }

    public void ShowInterstitialAds()
    {
        //MultiAds.Access.Interstitial_Show();
    }






    public void WatchUnityAdsForReward()
	{
		      
	}

    public void WatchAdsToDoubleWin(Action<bool> callback)
	{
		
	}
}