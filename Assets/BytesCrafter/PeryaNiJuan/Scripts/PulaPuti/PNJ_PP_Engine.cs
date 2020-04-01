using System.Collections;
using UnityEngine;

public enum PNJ_PP_State
{
    BETTING, ONDRAW, WANDER, TORESET
}

public class PNJ_PP_Engine : MonoBehaviour
{
    public float delayCheck = 1f;
    public PNJ_PP_State state = PNJ_PP_State.BETTING;
    public PNJ_CG_Blocker lowerBet;
    public PNJ_CG_Blocker resetFirst;
    public PNJ_CG_BillChange billChange;
    public PNJ_PP_BallGroup ballGroup; 

    public PNJ_PP_Picker colorPicker;

    public PNJ_PP_UserMenu userMenu;
    public PNJ_CG_Board calDisplay;

    [Header("TABLE PRIZE RATIO")]
    public int xThreeRed = 7;
    public int xOneStar = 15;
    public int xThreeWhite = 7;
    public int xTwoRed = 1;
    public int xTwoStar = 250;
    public int xTwoWhite = 7;

    void OnEnable()
    {
        CustomReference.Access.gameState = PNJ_GameState.DropBall;
        CustomReference.Access.dropBall = this;

        CustomReference.Access.dropBall.state = PNJ_PP_State.BETTING;
        userMenu.withdraw.interactable = false;
        calDisplay.Hide();

        CustomReference.Access.userData.userInfo.playStat.play_balldrop += 1;
        calDisplay.UpdateCashDisplay(CustomReference.Access.userData.userInfo.profile.cash, 0);
    }

    void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
		{
			RaycastHit hitInfo = CustomReference.Access.RaycastReturn;
			if(hitInfo.collider != null)
			{
				colorPicker.Raycasting (hitInfo);
			}
		}
    }

    public void Action_HomeButton()
    {
        Action_GetBet();
        CustomReference.Access.cameraControl.sceneScreen.LoadScene("ToMenu");
    }

    public void Action_DrawReset()
    {
        if(state == PNJ_PP_State.BETTING || state == PNJ_PP_State.ONDRAW)
        {
            ballGroup.DropTheBall();
        }

        else if( state == PNJ_PP_State.TORESET )
        {
            if(CustomReference.Access.dropBall.ballGroup.isAllBallOnPlatform)
            {
                colorPicker.Reset();
                CustomReference.Access.dropBall.state = PNJ_PP_State.BETTING;
                CustomReference.Access.dropBall.userMenu.GameHasBeenReset(true);
                CustomReference.Access.dropBall.calDisplay.Hide();
            }

            else
            {
                CustomReference.Access.dropBall.state = PNJ_PP_State.ONDRAW;
                CustomReference.Access.dropBall.userMenu.GameHasBeenReset(false);
            }
            ballGroup.Reset();
            
            resetFirst.Set (false);
            lowerBet.Set (false);            
        }
    }

    public void Action_GetBet()
    {
        if(state == PNJ_PP_State.BETTING)
        {
            resetFirst.Set (false);
            lowerBet.Set (false);
            int returnbet = CustomReference.Access.dropBall.colorPicker.GetTotalBet;
            CustomReference.Access.userData.UpdateUserInfos (returnbet, false, false);
            colorPicker.Reset();
            userMenu.withdraw.interactable = false;
        }
    }

    public void CheckNow()
    {
        StartCoroutine( CheckingNow() );
    }

    IEnumerator CheckingNow()
    {
        yield return new WaitUntil( () => ballGroup.allSphereSleep );
        yield return new WaitForSeconds( delayCheck );
        CustomReference.Access.dropBall.state = PNJ_PP_State.TORESET;

        //Declare wherther the ball is for reset and reball.
        if(CustomReference.Access.dropBall.ballGroup.isAllBallOnPlatform)
        {
            PNJ_PP_ResultBall result = GetListWin();
            CustomReference.Access.Debugs("INFORMATION! " + 
                "\nxThreeRed: " + result.xThreeRed + 
                "\nxTwoRed: " + result.xTwoRed + 
                "\nxThreeWhite: " + result.xThreeWhite + 
                "\nxTwoWhite: " + result.xTwoWhite + 
                "\nxTwoStar: " + result.xTwoStar + 
                "\nxOneStar: " + result.xOneStar
            );

            CustomReference.Access.dialogs.result.ShowDisplay (result);
        }

        else
        {
            CustomReference.Access.dialogs.ShowNotify("Some ball were stock in the funnel mouth and we need to re-drop all balls just to make it fair for both of us.", () => {
                userMenu.droset.interactable = true;
            });
        }
    }

    public PNJ_PP_ResultBall GetListWin()
    {
        PNJ_PP_ResultBall curBall = new PNJ_PP_ResultBall();
        
        int numReds = 0;
        int numWhite = 0;
        int numStar = 0;

        for(var i = 0; i < ballGroup.balls.Length; i++)
        {
            if(ballGroup.balls[i].socketOn == PNJ_PP_SockType.RED)
            {
                numReds += 1;
            }

            if(ballGroup.balls[i].socketOn == PNJ_PP_SockType.WHITE)
            {
                numWhite += 1;
            }

            if(ballGroup.balls[i].socketOn == PNJ_PP_SockType.STAR)
            {
                numStar += 1;
            }
        }

        curBall.xThreeRed = numReds == 3 ? true : false;
        curBall.xTwoRed = numReds == 2 ? true : false;

        curBall.xThreeWhite = numWhite == 3 ? true : false;
        curBall.xTwoWhite = numWhite == 2 ? true : false;

        curBall.xTwoStar = numStar == 2 ? true : false;
        curBall.xOneStar = numStar == 1 ? true : false;
        return curBall;
    }
}