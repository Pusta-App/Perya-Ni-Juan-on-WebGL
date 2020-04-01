using UnityEngine;
using System.Collections;

public class PNJ_CG_Engine : MonoBehaviour
{
	public PNJ_PP_State state = PNJ_PP_State.BETTING;
	public float checkWait = 2f;

	[Header("Components")]
	public PNJ_CG_Checker cubeChecker;
	public PNJ_CG_Release releaseControl;
	public PNJ_CG_Blocker lowerBet;
	public PNJ_CG_Blocker resetFirst;
	public PNJ_CG_Picker colorPicker;
	public PNJ_CG_BillChange billChange;
	public PNJ_CG_Board calcBoard;
	public PNJ_CG_UserMenu userMenu;

	[Header("TEMPORARY")]
	[HideInInspector]
	public GameObject betHolder;

	void OnEnable()
	{
		CustomReference.Access.gameState = PNJ_GameState.ColorGame;
		CustomReference.Access.colorGame = this;
		cubeChecker.RandomCubeTransform ();
		resetFirst.isReady = false;

		calcBoard.UpdateCashDisplay(CustomReference.Access.userData.userInfo.profile.cash, 0);
		CustomReference.Access.userData.userInfo.playStat.play_colorgame += 1;
	}

	void Update () 
	{
		//UPDATE CUBE RELEASE ROTATION FROM THE HANDLEBAR VALUE!
		releaseControl.holder.rotation = Quaternion.AngleAxis(57F * releaseControl.slider.value, Vector3.right);
		//CubeReleaseMechanism ();

		if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
		{
			RaycastHit hitInfo = CustomReference.Access.RaycastReturn;
			if(hitInfo.collider != null)
			{
				colorPicker.Raycasting (hitInfo);
			}
		}
	}

	public void Call_Reset(bool resetCalc)
	{
		if(resetCalc)
		{
			userMenu.handleAnim.enabled = false;
        	releaseControl.SetToResetIcon(false);
		}

		colorPicker.Reset ();
		releaseControl.Set (true);
		resetFirst.Set (false);
	}

	public void Call_OnBlockResetFirst()
	{
		resetFirst.Set (true);
		releaseControl.Animate (true);
	}

	public void Call_OnResultHide()
	{
		state = PNJ_PP_State.TORESET;
		userMenu.DrawOrReset( true );
		StartCoroutine( Call_OnResultHide_Delay() );
	}

	IEnumerator Call_OnResultHide_Delay()
	{
		yield return new WaitForSeconds(0.49f);
		// Activate or Enable showing of RESET FIRST block.
		resetFirst.isReady = true;
	}

	public void Call_ShowResult(PNJ_ResultItem resultItem, PNJ_CG_Result playResult)
	{
		calcBoard.Set(playResult);

		//Display related comments!
		if(playResult.getTotalPlayBet > 0)
		{
			if(colorPicker.playResult.getTotalReceived > colorPicker.playResult.getTotalPlayBet)
			{
				//headerResult.text = "YOU WIN!";
				CustomReference.Access.dialogs.result.resultLights.speed = 2.49f;
				CustomReference.Access.soundMix.Play ("Winner");
				
				// CustomReference.Access.playGames.ReportLeaderboardScore( "CgkI65TlqOsREAIQBQ", System.Convert.ToInt64( playResult.getTotalPlayWin ) );
				// CustomReference.Access.playGames.ReportLeaderboardScore( "CgkI65TlqOsREAIQGQ", System.Convert.ToInt64(playResult.getTotalPlayWin) );
				// CustomReference.Access.firebase.ReportEvent("earn_virtual_currency", playResult.getTotalPlayWin.ToString());
			}

			else
			{
				//headerResult.text = "YOU LOSE!";
				CustomReference.Access.dialogs.result.resultLights.speed = 0.75f;
				CustomReference.Access.soundMix.Play ("Loser");
			}

			//BC: Update the user data CASH whether WIN or LOSE.
			int cash = colorPicker.playResult.getTotalReceived;
			int wins = colorPicker.playResult.getTotalPlayWin;
			int bets = colorPicker.playResult.getTotalPlayBet;

			//BC: Update user records on bet, win, loss, and draw.
			CustomReference.Access.userData.UpdateUserPlayRecord(wins, bets, cash);
			CustomReference.Access.userData.UpdateUserInfos (cash, false, false);
		} 

		else 
		{ 
			
			CustomReference.Access.dialogs.result.resultLights.speed = 1.00f;
			CustomReference.Access.soundMix.Play ("NoBet");
		}

	}

	public void Action_HomeButton(string sceneName)
	{
		Action_GetBet();
		CustomReference.Access.cameraControl.sceneScreen.LoadScene(sceneName);
	}

	public void Action_DrawReset()
	{
		if( state == PNJ_PP_State.TORESET )
		{
			resetFirst.Set (false);

			calcBoard.Reset();
			userMenu.Buttons(true);
			userMenu.withdrawBet.interactable = false;
			releaseControl.Reset ();
			resetFirst.isReady = false;

			colorPicker.playResult = new PNJ_CG_Result (new int[6]);
			state = PNJ_PP_State.BETTING;
		}

		cubeChecker.RandomCubeTransform ();
	}

	public void Action_GetBet()
	{
		if( state == PNJ_PP_State.BETTING )
		{
			int returnCashBet = 0; //Initilized a variable that will hold the total CASH on BET.

			for(var i = 0; i < colorPicker.colorPicked.Length; i++) {
				returnCashBet += colorPicker.colorPicked[i];
			} //Here we used ColorPicker of this engine to get all bet PLACED.

			if( returnCashBet > 0 ) {
				CustomReference.Access.soundMix.Play("Reward");
				CustomReference.Access.userData.UpdateUserInfos (returnCashBet, false, false);
			} //If we have a bet PLACED, play a REWARD sound and also update userCash vault.
			
			//Make sure to RESET all value in Color Picker.
			colorPicker.Reset();

			//Make the calc board display on initial view.
			calcBoard.Reset();

			// Make the WITHDRAW button INTERACTABLE again to the user.
			userMenu.WithdrawnBet();

			// Destroy the BETHOLDER of the 3D models of BET PLACED.
			if( betHolder != null)
			{
				Destroy(betHolder);
			}
		}
	}
}