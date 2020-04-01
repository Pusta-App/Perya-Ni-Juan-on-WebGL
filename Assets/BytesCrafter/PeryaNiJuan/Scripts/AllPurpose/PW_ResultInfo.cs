
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using HungryCannibal.UnderTheSeaUIKit.Dialogs;
using HungryCannibal.UnderTheSeaUIKit.ProgressBars;

[System.Serializable]
public class PNJ_ResultItem
{
	public bool extraWinPrize = false;
	public int totalBetPlaced = 0;
	public int totalBetReturn = 0;
	public int totalWinnings = 0;

	public int totalToReceived 
	{
		get {
			int winTotal = extraWinPrize ? totalWinnings * 2: totalWinnings;
			return totalBetReturn + winTotal;
		}
	}
}

public class PW_ResultInfo : MonoBehaviour
{
	public PNJ_ResultItem resultItem = new PNJ_ResultItem();
	public GameObject watchAds;
	public string winTitle = "Congratulation!"; 
	public string loseTitle = "Sorry, try again!";
	public string nobetTitle = "Try your luck!"; 

	[Header("TYPES")]
	public GameObject typeCubeBall;
	public GameObject typeCockWin;
	public Image cockWinDisplay;

	[Header("REFERENCES")]
	public DialogBehaviour dialog = null;
	public Text headerResult = null;
	public CounterBar winningResult = null;
	public CounterBar totalbetAmount = null;
	public CounterBar totalwinAmount = null;
	public ImageGifier resultLights = null;
	public List<Image> cubeDisplay = new List<Image>();
	public Sprite neutral = null;
	public List<Sprite> cubes = new List<Sprite>();
	public List<Sprite> balls = new List<Sprite>();
	public List<Sprite> cocks = new List<Sprite>();

	private Sprite GetSpriteBall(PNJ_PP_SockType sockeOn)
	{
		if( sockeOn == PNJ_PP_SockType.RED )
		{
			return balls[0];
		}

		else if( sockeOn == PNJ_PP_SockType.WHITE )
		{
			return balls[1];
		}

		else
		{
			return balls[2];
		}
	}

	public int curPlayedTimes = 0;
	public int showadsPlayedTimes = 7;

	private void PrepareDisplay()
	{
		curPlayedTimes += 1;
		if(curPlayedTimes >= showadsPlayedTimes)
		{
			CustomReference.Access.multiads.ShowInterstitialAds();
			curPlayedTimes = 0;
		}
		
		cubeDisplay.ForEach((Image display) => {
			display.sprite = neutral;
		});

		dialog.Show ();
		watchAds.SetActive(false);

		winningResult.count = 0;
		totalbetAmount.count = 0;
		totalwinAmount.count = 0;

		resultItem = new PNJ_ResultItem();
	}

	//#region SHOWING RESULT

	/// <summary>
	/// Use this to show result from Color Game. Also, pass the class reference of the PNJ_CG_Result.
	/// </summary>
	/// <param name="result"></param>
	public void ShowDisplay(PNJ_CG_Result playResult)
	{
		//Check whether RESULT is 3s or 1s.
		typeCockWin.SetActive(false);
		typeCubeBall.SetActive(true);

		//Prepare and Translate RESULT VALUES to native result class.		
		PrepareDisplay();
		resultItem.totalBetPlaced = playResult.getTotalPlayBet;
		resultItem.totalWinnings = playResult.getTotalPlayWin;
		resultItem.totalBetReturn = playResult.getTotalPlayWin;

		//Display all play statistics.
		winningResult.IncrementCount(resultItem.totalToReceived, false);
		totalbetAmount.IncrementCount (resultItem.totalBetPlaced, false);
		totalwinAmount.IncrementCount (resultItem.totalWinnings, false);

		if( resultItem.totalBetPlaced > 0 ) 
		{
			headerResult.text = resultItem.totalToReceived > resultItem.totalBetPlaced ? winTitle : loseTitle;
			if(resultItem.totalToReceived > resultItem.totalBetPlaced)
			{
				watchAds.SetActive(true);
			}
		} 
		
		else 
		{
			headerResult.text = nobetTitle;
		}

		//Send DRAW information to the machine.
		CustomReference.Access.colorGame.Call_ShowResult(resultItem, playResult);
		
		//Process CUBE RESULT display for COLOR GAME.
		int curCubeIndex = 0;
		for(int i = 0; i < playResult.result.Length; i++)
		{
			if(playResult.result[i] >= 1)
			{
				cubeDisplay [curCubeIndex].sprite = cubes [i];
				curCubeIndex += 1;
			}

			if(playResult.result[i] >= 2)
			{
				cubeDisplay [curCubeIndex].sprite = cubes [i];
				curCubeIndex += 1;
			}

			if(playResult.result[i] == 3)
			{
				cubeDisplay [curCubeIndex].sprite = cubes [i];
			}
		}
	}

	/// <summary>
	/// Use this to show result from Ball Drop game. Also, pass the class reference of the PNJ_PP_ResultBall.
	/// </summary>
	/// <param name="result"></param>
	public void ShowDisplay(PNJ_PP_ResultBall result)
	{
		typeCockWin.SetActive(false);
		typeCubeBall.SetActive(true);

		PrepareDisplay();

		//Display the ball result as sprite.
		for(var i = 0; i < CustomReference.Access.dropBall.ballGroup.balls.Length; i++)
		{
			cubeDisplay [i].sprite = GetSpriteBall(CustomReference.Access.dropBall.ballGroup.balls[i].socketOn);
		}

		//Display all Result BET, RECEIVED, WIN.
		resultItem.totalBetPlaced = CustomReference.Access.dropBall.colorPicker.GetTotalBet;

		int[] betPicked = CustomReference.Access.dropBall.colorPicker.colorPicked;
		if(result.xThreeRed && betPicked[0] > 0)
		{
			resultItem.totalWinnings += betPicked[0] * CustomReference.Access.dropBall.xThreeRed;
			resultItem.totalBetReturn += betPicked[0];
		}
		if(result.xOneStar && betPicked[1] > 0)
		{
			resultItem.totalWinnings += betPicked[1] * CustomReference.Access.dropBall.xOneStar;
			resultItem.totalBetReturn += betPicked[1];
		}
		if(result.xThreeWhite && betPicked[2] > 0)
		{
			resultItem.totalWinnings += betPicked[2] * CustomReference.Access.dropBall.xThreeWhite;
			resultItem.totalBetReturn += betPicked[2];
		}
		if(result.xTwoRed && betPicked[3] > 0)
		{
			resultItem.totalWinnings += betPicked[3] * CustomReference.Access.dropBall.xTwoRed;
			resultItem.totalBetReturn += betPicked[3];
		}
		if(result.xTwoStar && betPicked[4] > 0)
		{
			resultItem.totalWinnings += betPicked[4] * CustomReference.Access.dropBall.xTwoStar;
			resultItem.totalBetReturn += betPicked[4];
		}
		if(result.xTwoWhite && betPicked[5] > 0)
		{
			resultItem.totalWinnings += betPicked[5] * CustomReference.Access.dropBall.xTwoWhite;
			resultItem.totalBetReturn += betPicked[5];
		}

		if(resultItem.totalBetPlaced > 0)
		{
			headerResult.text = resultItem.totalToReceived > resultItem.totalBetPlaced ? winTitle : loseTitle;

			if( resultItem.totalToReceived > resultItem.totalBetPlaced)
			{
				CustomReference.Access.userData.UpdateUserInfos(resultItem.totalToReceived, false, false);
				watchAds.SetActive(true);

				//headerResult.text = "YOU WIN!";
				CustomReference.Access.dialogs.result.resultLights.speed = 2.49f;
				CustomReference.Access.soundMix.Play ("Winner");

				// CustomReference.Access.playGames.ReportLeaderboardScore( "CgkI65TlqOsREAIQBQ", System.Convert.ToInt64( resultItem.totalWinnings ) );
				// CustomReference.Access.playGames.ReportLeaderboardScore( "CgkI65TlqOsREAIQGg", System.Convert.ToInt64( resultItem.totalWinnings ) );
				// CustomReference.Access.firebase.ReportEvent("earn_virtual_currency", resultItem.totalWinnings.ToString());
			}

			else
			{
				//headerResult.text = "YOU LOSE!";
				CustomReference.Access.dialogs.result.resultLights.speed = 0.75f;
				CustomReference.Access.soundMix.Play ("Loser");
			}
		}

		else
		{
			headerResult.text = nobetTitle;
			CustomReference.Access.dialogs.result.resultLights.speed = 1.00f;
			CustomReference.Access.soundMix.Play ("NoBet");
		}

		totalbetAmount.IncrementCount(resultItem.totalBetPlaced, false);
		winningResult.IncrementCount(resultItem.totalToReceived, false);
		totalwinAmount.IncrementCount(resultItem.totalWinnings, false);
	}

	/// <summary>
	/// Use this to show result from Sabong Wheel. Also, pass the class reference of the result and index.
	/// </summary>
	/// <param name="result"></param>
	/// <param name="index"></param>
	public void ShowDisplay(int[] result, int index)
	{
		typeCubeBall.SetActive(false);
		typeCockWin.SetActive(true);

		PrepareDisplay();

		cockWinDisplay.sprite = cocks[index];

		//totalBetPlaced 
		for(var i = 0; i < result.Length; i++)
		{
			resultItem.totalBetPlaced += result[i];
		}

		//totalbetReturn
		resultItem.totalBetReturn = result[index];

		//totalWinOnly
		resultItem.totalWinnings = result[index];
		
		if(resultItem.totalBetPlaced > 0)
		{
			headerResult.text = resultItem.totalToReceived > resultItem.totalBetPlaced ? winTitle : loseTitle;

			if( resultItem.totalToReceived > resultItem.totalBetPlaced)
			{
				watchAds.SetActive(true);
				//headerResult.text = "YOU WIN!";
				CustomReference.Access.dialogs.result.resultLights.speed = 2.49f;
				CustomReference.Access.soundMix.Play ("Winner");
				
				// CustomReference.Access.playGames.ReportLeaderboardScore( "CgkI65TlqOsREAIQBQ", System.Convert.ToInt64( resultItem.totalWinnings ) );
				// CustomReference.Access.playGames.ReportLeaderboardScore( "CgkI65TlqOsREAIQGw", System.Convert.ToInt64( resultItem.totalWinnings ) );
				// CustomReference.Access.firebase.ReportEvent("earn_virtual_currency", resultItem.totalWinnings.ToString());
			}

			else
			{
				//headerResult.text = "YOU LOSE!";
				CustomReference.Access.dialogs.result.resultLights.speed = 0.75f;
				CustomReference.Access.soundMix.Play ("Loser");
			}
		}

		else
		{
			headerResult.text = nobetTitle;
			CustomReference.Access.dialogs.result.resultLights.speed = 1.00f;
			CustomReference.Access.soundMix.Play ("NoBet");
		}

		totalbetAmount.IncrementCount(resultItem.totalBetPlaced, false);
		winningResult.IncrementCount(resultItem.totalToReceived, false);
		totalwinAmount.IncrementCount(resultItem.totalWinnings, false);
	}

	//#endregion SHOWING RESULT


	public void HideDisplay()
	{
		//Handheld.Vibrate();
		resultLights.speed = 1f;
		if( CustomReference.Access.gameState == PNJ_GameState.ColorGame)
		{			
			CustomReference.Access.colorGame.Call_OnResultHide();
		}
		
		else if (CustomReference.Access.gameState == PNJ_GameState.DropBall) {
			if(CustomReference.Access.dropBall.ballGroup.isAllBallOnPlatform)
			{
				//BC: Update user records on bet, win, loss, and draw.
				CustomReference.Access.userData.UpdateUserPlayRecord(resultItem.totalWinnings, resultItem.totalBetPlaced, resultItem.totalToReceived);

				CustomReference.Access.userData.UpdateUserInfos (resultItem.totalToReceived, false, false);
				CustomReference.Access.dropBall.userMenu.droset.interactable = true;
				CustomReference.Access.dropBall.calDisplay.Show();
			}

			else 
			{
				//BC: Call update redraw on ball drop.
				CustomReference.Access.userData.userInfo.playStat.play_balldrop_redraw += 1;
			}
		}

		resultItem.extraWinPrize = false;
		dialog.Hide ();
	}

	public void WatchToDoubleWin()
	{
		CustomReference.Access.multiads.WatchAdsToDoubleWin( (bool success) => {
			resultItem.extraWinPrize = success;
			
			if(success)
			{
				int totalWinnings = 0;
				int totalReceieved = 0;

				totalWinnings = resultItem.extraWinPrize ? resultItem.totalWinnings*2 : resultItem.totalWinnings;
				totalReceieved = resultItem.totalToReceived;

				winningResult.count = 0; //CASH RETURN
				winningResult.IncrementCount(totalReceieved, false);
				totalwinAmount.count = 0; //TOTAL WINS
				totalwinAmount.IncrementCount (totalWinnings, false);

				int currentReward = resultItem.totalWinnings;
				CustomReference.Access.userData.userInfo.adStat.unity_reward += currentReward;
				CustomReference.Access.userData.userInfo.adStat.earned_rewards += currentReward;
				CustomReference.Access.userData.UpdateUserInfos(currentReward, false, false);

				watchAds.SetActive(false);
			}
		});
	}
}