
using UnityEngine;

public class PNJ_CG_Picker : MonoBehaviour 
{
	public PNJ_CG_Result playResult = new PNJ_CG_Result (new int[6]);
	public int[] colorPicked = new int[6];
	private GameObject betHolder = null;

	public void Reset()
	{		
		playResult = new PNJ_CG_Result (new int[6]);
		colorPicked = new int[6];
		Destroy (betHolder);
	}

	public void Raycasting(RaycastHit hitInfo)
	{
		if(hitInfo.collider.tag.Contains("ColorPick"))
		{
			if(CustomReference.Access.colorGame.state == PNJ_PP_State.BETTING)
			{
				CustomReference.Access.colorGame.resetFirst.Set (false);
				///CustomReference.Access.colorGame.releaseControl.Animate (false);

				if(CustomReference.Access.userData.userInfo.profile.cash > 0)
				{
					int currentBill = CustomReference.Access.colorGame.billChange.currentBill;
					int currentBet = CustomReference.Access.globalRefs.billPrefabs [currentBill].amount;

					if(CustomReference.Access.userData.userInfo.profile.cash >= currentBet)
					{
						CustomReference.Access.colorGame.userMenu.StartedBetting();
						
						//ADD THE SPECIFIED AMOUNT TO THE COLOR PICKED!
						playResult.UpdateBets
						(
							hitInfo.collider.transform.GetSiblingIndex (), //GET THE CURRENT INDEX OF THE COLOR PICKED.
							currentBet //GET THE CURRENT AMOUNT OF CHIP PLACED.
						);

						//GET THE CURRENT INDEX OF THE COLOR PICKED!
						int index = hitInfo.collider.transform.GetSiblingIndex (); 

						//ADD THE SPECIFIED AMOUNT TO THE COLOR PICKED!
						colorPicked[index] += currentBet;

						//SUBCTRACT THE AMOUNT FROM THE TOTAL CASH!
						//CustomReference.Access.userData.userInfo.profile.cash -= CustomReference.Access.colorGame.billPrefabs[currentBill].amount;

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
							CustomReference.Access.colorGame.betHolder = betHolder;
						}

						//INSTANTIATE THE CURRENT BILL PREFAB SELECTED!
						Vector3 tempPos = new Vector3(hitInfo.point.x, hitInfo.point.y + CustomReference.Access.globalRefs.betHeight, hitInfo.point.z);
						BillValue tempCoin = Instantiate(CustomReference.Access.globalRefs.billPrefabs[currentBill].transform, tempPos, Quaternion.identity).GetComponent<BillValue>();
						tempCoin.transform.parent = betHolder.transform;

						//UPDATE USER STATS INFO.
						CustomReference.Access.userData.UpdateUserInfos (-currentBet, false, false);
						CustomReference.Access.colorGame.calcBoard.Bet(playResult);
					}

					else
					{
						CustomReference.Access.colorGame.lowerBet.Set (true);
					}
				}

				else
				{
					CustomReference.Access.colorGame.state = PNJ_PP_State.WANDER;
					CustomReference.Access.dialogs.store.Show();
				}
			}

			else if(CustomReference.Access.colorGame.state == PNJ_PP_State.TORESET)
			{
				CustomReference.Access.colorGame.Call_OnBlockResetFirst();
			}
		}
	}
}