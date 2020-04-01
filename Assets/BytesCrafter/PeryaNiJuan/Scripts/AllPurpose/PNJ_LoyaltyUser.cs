using System;
using UnityEngine;
using UnityEngine.UI;

public enum PNJ_LoyalReward
{
    ThirtyMin,
    OneHour,
    SevenHours,
    TwelveHours,
    OneDay
}

public class PNJ_LoyaltyUser : MonoBehaviour
{
    public Text amount;
    public PNJ_LoyalReward loyalRewardType = PNJ_LoyalReward.ThirtyMin;
    private string preKey = "Loyal_";
    public int timeSpan;
    public Button button;
    public Text text;

    void OnEnable()
    {
        timer = 2f;
        Update();
    }

    public int reward {
        get {
            int reward = 0;
            switch ( loyalRewardType )
            {
                case PNJ_LoyalReward.ThirtyMin:
                    reward = CustomReference.Access.settings.defaultConfigs.loyalRewards.thirtyMinutes;
                    break;
                case PNJ_LoyalReward.OneHour:
                    reward = CustomReference.Access.settings.defaultConfigs.loyalRewards.oneHour;
                    break;
                case PNJ_LoyalReward.SevenHours:
                    reward = CustomReference.Access.settings.defaultConfigs.loyalRewards.sevenHours;
                    break;
                case PNJ_LoyalReward.TwelveHours:
                    reward = CustomReference.Access.settings.defaultConfigs.loyalRewards.twelveHours;
                    break;
                case PNJ_LoyalReward.OneDay:
                    reward = CustomReference.Access.settings.defaultConfigs.loyalRewards.oneDay;
                    break;
                default:
                    break;
            } 
            return reward;
        }
    }
    private float timer;
    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer > 1f)
        {            
            amount.text = reward.ToString(); //Refresh display here.
            
            if( PlayerPrefs.HasKey( prefsKey ) )
            {
                if( PlayerPrefs.HasKey( countdone ) )
                {
                    if( !button.interactable )
                    {
                        button.interactable = true;
                    }
                    if( text.text != "CLAIM" )
                    {
                        text.text = "CLAIM";
                    }
                }

                else
                {
                    if( button.interactable )
                    {
                        button.interactable = false;
                    }

                    string startDate = PlayerPrefs.GetString(prefsKey);
                    DateTime dateStart = DateTime.Parse( startDate );
                    DateTime currentTime = DateTime.Now;
                    TimeSpan latestStamp = currentTime.Subtract(dateStart);
                    text.text = latestStamp.ToString(@"hh\:mm\:ss");

                    if( latestStamp.TotalSeconds >= timeSpan )
                    {
                        PlayerPrefs.SetString( countdone, "Done" );
                    }                
                }
            }
            
            else
            {
                if( !button.interactable )
                {
                    button.interactable = true;
                }
                if( text.text != "START" )
                {
                    text.text = "START";
                }
            }
            timer = 0f;
        }
    }

    private string prefsKey
    {
        get {
            return preKey + timeSpan.ToString();
        }
    }

    private string countdone
    {
        get {
            return preKey + timeSpan.ToString() + "_Done";
        }
    }

    public void ButtonClick()
    {
        if( text.text == "START" )
        {
            button.interactable = false;
            
            CustomReference.Access.soundMix.Play("Tap");
            PlayerPrefs.SetString(prefsKey, System.DateTime.Now.ToString());

            switch(loyalRewardType)
            {
                case PNJ_LoyalReward.ThirtyMin:
                    CustomReference.Access.userData.userInfo.rewards.reward_30min_start += 1;
                    break;
                case PNJ_LoyalReward.OneHour:
                    CustomReference.Access.userData.userInfo.rewards.reward_1hour_start += 1;
                    break;
                case PNJ_LoyalReward.SevenHours:
                    CustomReference.Access.userData.userInfo.rewards.reward_7hours_start += 1;
                    break;
                case PNJ_LoyalReward.TwelveHours:
                    CustomReference.Access.userData.userInfo.rewards.reward_12hours_start += 1;
                    break;
                case PNJ_LoyalReward.OneDay:
                    CustomReference.Access.userData.userInfo.rewards.reward_1day_start += 1;
                    break;
                default:
                    break;
            }  
        }

        else
        {
            if( text.text == "CLAIM" )
            {
                // Add reward to userdata. from (int)reward
                //MultiAds.Access.Interstitial_Show();

                CustomReference.Access.soundMix.Play("Reward");
                
                PlayerPrefs.DeleteKey( prefsKey );
                PlayerPrefs.DeleteKey( countdone );
                CustomReference.Access.userData.UpdateUserInfos(reward, false, false);

                if( !button.interactable )
                {
                    button.interactable = true;
                }
                if( text.text != "START" )
                {
                    text.text = "START";
                }
            }

            switch(loyalRewardType)
            {
                case PNJ_LoyalReward.ThirtyMin:
                    CustomReference.Access.userData.userInfo.rewards.reward_30min_claim += 1;
                    CustomReference.Access.userData.userInfo.rewards.reward_30min_received += reward;
                    break;
                case PNJ_LoyalReward.OneHour:
                    CustomReference.Access.userData.userInfo.rewards.reward_1hour_claim += 1;
                    CustomReference.Access.userData.userInfo.rewards.reward_1hour_received += reward;
                    break;
                case PNJ_LoyalReward.SevenHours:
                    CustomReference.Access.userData.userInfo.rewards.reward_7hours_claim += 1;
                    CustomReference.Access.userData.userInfo.rewards.reward_7hours_received += reward;
                    break;
                case PNJ_LoyalReward.TwelveHours:
                    CustomReference.Access.userData.userInfo.rewards.reward_12hours_claim += 1;
                    CustomReference.Access.userData.userInfo.rewards.reward_12hours_received += reward;
                    break;
                case PNJ_LoyalReward.OneDay:
                    CustomReference.Access.userData.userInfo.rewards.reward_1day_claim += 1;
                    CustomReference.Access.userData.userInfo.rewards.reward_1day_received += reward;
                    break;
                default:
                    break;
            }  
        }
    }
}
