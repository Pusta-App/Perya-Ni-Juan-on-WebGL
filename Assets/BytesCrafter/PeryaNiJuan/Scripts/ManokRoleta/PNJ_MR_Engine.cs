using System.Collections;
using UnityEngine;

public class PNJ_MR_Engine : MonoBehaviour
{
    public float delayCheck = 1f;
    public PNJ_MR_Rotator rotator;
    public PNJ_CG_BillChange billChange;
    
    public PNJ_SR_SabongWheel colorPicker;
    public PNJ_PP_UserMenu userMenu;
    public PNJ_CG_Blocker lowerBet;
    public PNJ_CG_Blocker resetFirst;
    public PNJ_CG_Board calDisplay;
    public bool toReset = false;
    public bool isReady = true;

    void OnEnable()
    {
        CustomReference.Access.gameState = PNJ_GameState.SabongWheel;
        CustomReference.Access.sabongWheel = this;
        isReady = true;
        
        calDisplay.Hide();
        CustomReference.Access.userData.userInfo.playStat.play_sabongwheel += 1;
        calDisplay.UpdateCashDisplay(CustomReference.Access.userData.userInfo.profile.cash, 0);
        userMenu.withdraw.interactable = false;
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

    public void RotateNow()
    {
        if( isReady )
        {
            userMenu.BallHadBeenDrop();
            CustomReference.Access.soundMix.Play("Roulette");
            userMenu.droset.interactable = false;
            userMenu.withdraw.interactable = false;
            rotator.Play();
            isReady = false;
        }

        else
        {
            if( userMenu.isOkayToReset ) {
                Reset();
            }
        }
    }

    public void GetBet()
    {
        if( isReady )
        {
             int returnbet = CustomReference.Access.sabongWheel.colorPicker.GetTotalBet;
             CustomReference.Access.userData.UpdateUserInfos (returnbet, false, false);
             colorPicker.Reset();
             userMenu.withdraw.interactable = false;
        }
    }

    public void CheckNow( string color, int index )
    {
        StartCoroutine( CheckingNow(color, index) );
    }

    IEnumerator CheckingNow( string color, int index )
    {
        CustomReference.Access.soundMix.Stop("Roulette", 0.7f);
        
        yield return new WaitForSeconds( delayCheck );
        curIndex = index; Debug.LogWarning( "Color: " + color + " Index: " + index );

        int win = colorPicker.colorPicked[index] * 2;
        CustomReference.Access.userData.UpdateUserInfos (win, false, false);

        // SHOW RESULT CALC DISPLAY!
        CustomReference.Access.dialogs.result.ShowDisplay (colorPicker.colorPicked, index);

        userMenu.drosetIcon.sprite = userMenu.resetIcon;
        userMenu.droset.interactable = true;
    }

    private int curIndex = 0;
    public void Reset()
    {        
        int win = colorPicker.colorPicked[curIndex] * 2;
        if( win > 0 )
        {
            CustomReference.Access.soundMix.Play("Reward");
        }

        CustomReference.Access.userData.UpdateUserInfos (0, false, false);
        colorPicker.Reset();
        userMenu.GameHasBeenReset(true);
        isReady = true;
        toReset = false;
    }
}
