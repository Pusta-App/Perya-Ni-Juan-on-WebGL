
using System;
using UnityEngine;

namespace PeryaNiJuan
{
    [System.Serializable]
    public class Personal 
    {
        public string base64photo = "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";
        public Texture2D photo {
            get {
                Texture2D userPhoto = new Texture2D (1, 1);
                userPhoto.LoadImage(Convert.FromBase64String(base64photo)); 
                userPhoto.Apply ();
                return userPhoto;
            }
        }
        public string userid;
        public string username;
    }

    [System.Serializable]
    public class PlayGames 
    {
        public int signin_success = 0;
        public int signin_failed = 0;
        public int shown_achievement = 0;
        public int shown_leaderboard = 0;
    }

    [System.Serializable]
    public class PlayStat 
    {
        public string date_started;
        public TimeSpan span_playedsince {
            get {
                DateTime dateTime;
                if(DateTime.TryParse(date_started, out dateTime))
                {
                    return dateTime.Subtract(DateTime.Now);
                }

                else
                {
                    return new TimeSpan();
                }
            }
        }
        public string date_lastplayed;
        public TimeSpan span_lastplayed {
            get {
                DateTime dateTime;
                if(DateTime.TryParse(date_lastplayed, out dateTime))
                {
                    return dateTime.Subtract(DateTime.Now);
                }

                else
                {
                    return new TimeSpan();
                }
            }
        }
        
        public int bet_total_cash = 0;
        public int loss_total_cash = 0;
        public int win_total_cash = 0;
        public int bet_num_occur = 0;
        public int loss_num_occur = 0;
        public int win_num_occur = 0;
        public int bet_max_placed = 0;
        public int loss_max_bet = 0;
        public int win_max_earn = 0;

        public int play_colorgame = 0;
        public int play_colorgame_draw = 0;
        public int play_colorgame_redraw = 0;
        public int play_colorgame_nobet = 0;
        public int play_balldrop = 0;
        public int play_balldrop_draw = 0;
        public int play_balldrop_redraw = 0;
        public int play_balldrop_nobet = 0;
        public int play_sabongwheel = 0;
        public int play_sabongwheel_draw = 0;
        public int play_sabongwheel_redraw = 0;
        public int play_sabongwheel_nobet = 0;
    }

    [System.Serializable]
    public class Profile 
    {
        public int level = 1;
        public int exp = 0;
        public int cash = 100;
    }

    [System.Serializable]
    public class AdStat 
    {
        public int bannerview;
        public int interstitial;
        public int rewardads;
        public int unity_skipped;
        public int unity_reward;
        public int earned_rewards;
    }

    [System.Serializable]
    public class Rewards 
    {
        public int reward_30min_received = 0;
        public int reward_30min_claim = 0;
        public int reward_30min_start = 0;
        public int reward_1hour_received = 0;
        public int reward_1hour_claim = 0;
        public int reward_1hour_start = 0;
        public int reward_7hours_received = 0;
        public int reward_7hours_claim = 0;
        public int reward_7hours_start = 0;
        public int reward_12hours_received = 0;
        public int reward_12hours_claim = 0;
        public int reward_12hours_start = 0;
        public int reward_1day_claim = 0;
        public int reward_1day_received = 0;
        public int reward_1day_start = 0;

        public int reward_totalcash_claimed
        {
            get {
                return reward_1hour_received 
                + reward_30min_received
                + reward_1hour_received
                + reward_7hours_received
                + reward_1day_claim;
            }
        }

        public int reward_totaltime_spent
        {
            get {
                int hours = reward_30min_claim / 2;
                hours += reward_1hour_claim;
                hours += reward_7hours_claim * 7;
                hours += reward_12hours_claim * 12;
                hours += reward_1day_claim * 24;
                return hours;
            }
        }
    }

    [System.Serializable]
    public class PNJ_UserProfile
    {
        public Personal personal;
        public PlayGames playGames;
        public PlayStat playStat;
        public Profile profile;
        public AdStat adStat;
        public Rewards rewards;
    }
}