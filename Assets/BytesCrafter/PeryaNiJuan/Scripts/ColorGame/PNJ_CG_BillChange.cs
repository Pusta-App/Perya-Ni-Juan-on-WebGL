using UnityEngine;
using UnityEngine.UI;

public class PNJ_CG_BillChange : MonoBehaviour 
{
	public int currentBill = 0;

	public ScrollRect target;
	public void SlideRight()
	{
		target.horizontalNormalizedPosition += 0.1f;
	}

	public void SlideLeft()
	{
		target.horizontalNormalizedPosition += 0.1f;
	}

	public void SetCurrentBillBet(Toggle target)
	{
		switch ( target.name )
		{
			case "Bill1":
				currentBill = 0;
				break;
			case "Bill5":
				currentBill = 1;
				break;
			case "Bill10":
				currentBill = 2;
				break;
			case "Bill20":
				currentBill = 3;
				break;
			case "Bill50":
				currentBill = 4;
				break;
			case "Bill100":
				currentBill = 5;
				break;
			case "Bill200":
				currentBill = 6;
				break;
			case "Bill500":
				currentBill = 7;
				break;
			case "Bill1000":
				currentBill = 8;
				break;
			default:
				break;
		} //currentBill = target.transform.GetSiblingIndex ();
		
		if(target.isOn)
		{
			if(CustomReference.Access.userData.userInfo.profile.cash > 0)
			{
				if( CustomReference.Access.gameState == PNJ_GameState.ColorGame)
				{
					//User is betting on color game table.
					if(CustomReference.Access.userData.userInfo.profile.cash >= CustomReference.Access.globalRefs.billPrefabs[currentBill].amount)
					{
						CustomReference.Access.colorGame.lowerBet.Set (false);
					}

					else			
					{
						CustomReference.Access.colorGame.lowerBet.Set (true);
					}
				}

				else if( CustomReference.Access.gameState == PNJ_GameState.DropBall)
				{
					//User is betting on dropball table.
					if(CustomReference.Access.userData.userInfo.profile.cash >= CustomReference.Access.globalRefs.billPrefabs[currentBill].amount)
					{
						CustomReference.Access.dropBall.lowerBet.Set (false);
					}

					else			
					{
						CustomReference.Access.dropBall.lowerBet.Set (true);
					}
				}

				else if( CustomReference.Access.gameState == PNJ_GameState.SabongWheel)
				{
					//User is betting on dropball table.
					if(CustomReference.Access.userData.userInfo.profile.cash >= CustomReference.Access.globalRefs.billPrefabs[currentBill].amount)
					{
						CustomReference.Access.sabongWheel.lowerBet.Set (false);
					}

					else			
					{
						CustomReference.Access.sabongWheel.lowerBet.Set (true);
					}
				}
			}

			else
			{
				CustomReference.Access.dialogs.store.Show();
			}
		}
	}
}