using UnityEngine;
using System;
using System.Collections;
using PeryaNiJuan;

public class PNJ_UserData : MonoBehaviour 
{
	[Header("USER STATS!")]

	public PNJ_UserProfile userInfo = new PNJ_UserProfile();
	private PNJ_UserProfile prevUserInfo = new PNJ_UserProfile();

	void Start()
	{
		StartCoroutine( Initialized() );
	}

	IEnumerator Initialized()
	{
		yield return new WaitForSeconds(2f);
		
		if(PlayerPrefs.HasKey(userInfoKey))
		{
			string classString = Base64Decode(PlayerPrefs.GetString(userInfoKey));
			userInfo = BytesCrafter.DataProcessor.Serializer.StringToClass<PNJ_UserProfile>(classString);
			loadInitSave = true;
		}

		else 
		{
			loadInitSave = true;
		}

		CustomReference.Access.userData.userInfo.playStat.date_lastplayed = DateTime.Now.ToString();

		if(PlayerPrefs.HasKey(lastRewardDateKey))
		{
			string dateString = PlayerPrefs.GetString(lastRewardDateKey);
			DateTime dateTimed = System.Convert.ToDateTime (dateString);

			if(DateTime.Now.Subtract(dateTimed).Hours >= 12)
			{
				CustomReference.Access.dialogs.ShowNotify("Thank you for coming back! we missed you so we will reward you <b>"+ rewardReturn +"</b> of cash to enjoy.", () => {
					CustomReference.Access.userData.userInfo.profile.cash += rewardReturn;
					UpdateUserInfos (rewardReturn, false, true); 
					
					CustomReference.Access.Debugs("User is rewarded as a returnee.");

					CustomReference.Access.soundMix.Play ("Reward");
					PlayerPrefs.SetString (lastRewardDateKey, System.Convert.ToString (DateTime.Now));
				});
				
			}
		}

		else
		{
			CustomReference.Access.dialogs.ShowNotify("Thank you for downloading our game! We decided to give you a <b>"+ rewardWelcome +"</b> cash reward. Enjoy the game!", () => {
				
				CustomReference.Access.userData.userInfo.playStat.date_started = DateTime.Now.ToString();
				CustomReference.Access.userData.userInfo.profile.cash += rewardWelcome;
				UpdateUserInfos (rewardWelcome, false, true);

				CustomReference.Access.Debugs("User is rewarded as a welcome.");

				CustomReference.Access.soundMix.Play ("Reward");
				PlayerPrefs.SetString (lastRewardDateKey, System.Convert.ToString (DateTime.Now));
			});
		}
	}

	public string lastRewardDateKey = "LastRewardDateKey";
	public int rewardWelcome = 50;
	public int rewardReturn = 100;

	public string userInfoKey = "UserData";
	bool loadInitSave = false;
	float timer = 0f;
	void Update()
	{
		timer += Time.deltaTime;
		
		if(timer > 0.5f)
		{
			string cur = BytesCrafter.DataProcessor.Serializer.ClassToString<PNJ_UserProfile>(userInfo);
			string prev = BytesCrafter.DataProcessor.Serializer.ClassToString<PNJ_UserProfile>(prevUserInfo);

			//Fist check if userInfo has changed or not.
			if( !cur.Equals(prev) )
			{
				if( loadInitSave )
				{
					prevUserInfo = BytesCrafter.DataProcessor.Serializer.StringToClass<PNJ_UserProfile>(cur);
					PlayerPrefs.SetString(userInfoKey, Base64Encode(cur));
					CustomReference.Access.Debugs("User info is currently saving on Player Prefs.");
				}
			}
			
			timer = 0f;
		}
	}

	public string Base64Encode(string plainText) {
		var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
		return System.Convert.ToBase64String(plainTextBytes);
	}

	public string Base64Decode(string base64EncodedData) {
		var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
		return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
	}

	public void UpdateUserInfos(int increment, bool reset, bool animate)
	{
		if( CustomReference.Access.gameState == PNJ_GameState.ColorGame )
		{
			CustomReference.Access.colorGame.calcBoard.UpdateCashDisplay(userInfo.profile.cash, increment);
		} 

		else if( CustomReference.Access.gameState == PNJ_GameState.DropBall )
		{
			CustomReference.Access.dropBall.calDisplay.UpdateCashDisplay(userInfo.profile.cash, increment);
		}

		else if( CustomReference.Access.gameState == PNJ_GameState.SabongWheel )
		{
			CustomReference.Access.sabongWheel.calDisplay.UpdateCashDisplay(userInfo.profile.cash, increment);
		}

		userInfo.profile.cash += increment;
	}

	public void UpdateUserPlayRecord(int win, int bet, int cashReturn)
	{
		if(CustomReference.Access.gameState == PNJ_GameState.ColorGame)
		{
			userInfo.playStat.play_colorgame_draw += 1;
		}

		else if(CustomReference.Access.gameState == PNJ_GameState.DropBall)
		{
			userInfo.playStat.play_balldrop_draw += 1;
		}

		else if(CustomReference.Access.gameState == PNJ_GameState.SabongWheel)
		{
			userInfo.playStat.play_sabongwheel_draw += 1;
		}

		if(cashReturn > bet)
		{
			userInfo.playStat.win_num_occur += 1;
			if( win > userInfo.playStat.win_max_earn )
			{
				userInfo.playStat.win_max_earn = win;
			}
			userInfo.playStat.win_total_cash += win;
		}

		else
		{
			userInfo.playStat.loss_num_occur += 1;
			int curLost = cashReturn - bet;
			if( bet > userInfo.playStat.loss_max_bet )
			{
				userInfo.playStat.loss_max_bet = curLost;
			}
			userInfo.playStat.loss_total_cash += curLost;
		}

		if (bet > 0)
		{
			userInfo.playStat.bet_num_occur += 1;
			if( bet > userInfo.playStat.bet_max_placed )
			{
				userInfo.playStat.bet_max_placed = bet;
			}
			userInfo.playStat.bet_total_cash += bet;
		}

		else 
		{
			if(CustomReference.Access.gameState == PNJ_GameState.ColorGame)
			{
				userInfo.playStat.play_colorgame_nobet += 1;
			}

			else if(CustomReference.Access.gameState == PNJ_GameState.DropBall)
			{
				userInfo.playStat.play_balldrop_nobet += 1;
			}

			else if(CustomReference.Access.gameState == PNJ_GameState.SabongWheel)
			{
				userInfo.playStat.play_sabongwheel_nobet += 1;
			}
		}
	}
}