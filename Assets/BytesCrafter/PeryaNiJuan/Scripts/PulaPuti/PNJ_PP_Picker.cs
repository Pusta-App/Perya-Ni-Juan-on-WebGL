
using UnityEngine;



public class PNJ_PP_Picker : MonoBehaviour
{
	public int[] colorPicked = new int[6];
    public GameObject betHolder = null;

    public int GetTotalBet
    {
        get
        {
            int current = 0;
            for(var i = 0; i < colorPicked.Length; i++)
            {
                current += colorPicked[i];
            }
            
            if( current > 0 )
            {
                CustomReference.Access.soundMix.Play("Reward");
            }
            return current;
        }
    }
    public void Reset()
    {
		colorPicked = new int[6];
        Destroy(betHolder);
    }
    
    public void Raycasting(RaycastHit hitInfo)
	{
		if( CustomReference.Access.dropBall.state != PNJ_PP_State.BETTING )
        {
            return;
        }
        
        if(hitInfo.collider.tag.Contains("ColorPick"))
		{
			CustomReference.Access.dropBall.resetFirst.Set (false);
			///CustomReference.Access.dropBall.releaseControl.Animate (false);

            if(CustomReference.Access.userData.userInfo.profile.cash > 0)
            {
                int currentBill = CustomReference.Access.dropBall.billChange.currentBill;
                int currentBet = CustomReference.Access.globalRefs.billPrefabs [currentBill].amount;

                if(CustomReference.Access.userData.userInfo.profile.cash >= currentBet)
                {
                    ///CustomReference.Access.dropBall.userMenu.StartedBetting();
                    
                    //ADD THE SPECIFIED AMOUNT TO THE COLOR PICKED!
                    ////playResult.UpdateBets
                    ////(
                    ////	hitInfo.collider.transform.GetSiblingIndex (), //GET THE CURRENT INDEX OF THE COLOR PICKED.
                    ////	currentBet //GET THE CURRENT AMOUNT OF CHIP PLACED.
                    ////);

                    //GET THE CURRENT INDEX OF THE COLOR PICKED!
                    int index = hitInfo.collider.transform.GetSiblingIndex (); 

                    //ADD THE SPECIFIED AMOUNT TO THE COLOR PICKED!
                    colorPicked[index] += currentBet;

                    //SUBCTRACT THE AMOUNT FROM THE TOTAL CASH!
                    //CustomReference.Access.userData.currentCash -= CustomReference.Access.dropBall.billPrefabs[currentBill].amount;

                    if(currentBill < 3)
                    {
                        CustomReference.Access.soundMix.Play("Coin");
                    }

                    else
                    {
                        CustomReference.Access.soundMix.Play("Paper");
                    }

                    //UPDATE THE GAME INFO DISPLAY!
                    //CustomReference.Access.calcHolder.GetChild (index).GetComponent<Text>().text =
                    //	"P" + colorPicked[index] + ".00 X " + colorResult[index] + " = P" + (colorPicked[index] * colorResult[index]) + ".00";

                    //CREATE A GAMEOBJECT TO HOLD BET PREFABS!
                    if(betHolder == null)
                    {
                        betHolder = new GameObject("BetHolder"); 
                        betHolder.tag = "UserBet" ;
                        ///CustomReference.Access.dropBall.userMenu.betHolder = betHolder;
                    }

                    //INSTANTIATE THE CURRENT BILL PREFAB SELECTED!
                    Vector3 tempPos = new Vector3(hitInfo.point.x, hitInfo.point.y + CustomReference.Access.globalRefs.betHeight, hitInfo.point.z);
                    BillValue tempCoin = Instantiate(CustomReference.Access.globalRefs.billPrefabs[currentBill].transform, tempPos, Quaternion.identity).GetComponent<BillValue>();
                    tempCoin.transform.parent = betHolder.transform;

                    //UPDATE USER STATS INFO.
                    CustomReference.Access.userData.UpdateUserInfos (-currentBet, false, false);
                    ///CustomReference.Access.dropBall.calcBoard.Bet(playResult);

                    CustomReference.Access.dropBall.userMenu.withdraw.interactable = true;
                }

                else
                {
                    CustomReference.Access.dropBall.lowerBet.Set (true);
                }
            }

            else
            {
                CustomReference.Access.dropBall.state = PNJ_PP_State.WANDER;
                CustomReference.Access.dialogs.store.Show();
            }
		}
	}
}
