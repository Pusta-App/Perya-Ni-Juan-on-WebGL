
using UnityEngine;

public class PNJ_SR_SabongWheel : MonoBehaviour
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
		if( !CustomReference.Access.sabongWheel.isReady )
        {
            //if( CustomReference.Access.sabongWheel.toReset )
            //{
            //    CustomReference.Access.dialogs.ShowNotify("Can't do that! Please press RESET first to continue playing.", null);
            //}
            return;
        }
        
        if(hitInfo.collider.tag.Contains("ColorPick"))
		{
            if(CustomReference.Access.userData.userInfo.profile.cash > 0)
            {
                int currentBill = CustomReference.Access.sabongWheel.billChange.currentBill;
                int currentBet = CustomReference.Access.globalRefs.billPrefabs [currentBill].amount;

                if(CustomReference.Access.userData.userInfo.profile.cash >= currentBet)
                {                  
                    //GET THE CURRENT INDEX OF THE COLOR PICKED!
                    int index = hitInfo.collider.transform.GetSiblingIndex (); 

                    //ADD THE SPECIFIED AMOUNT TO THE COLOR PICKED!
                    colorPicked[index] += currentBet;

                    if(currentBill < 3)
                    {
                        CustomReference.Access.soundMix.Play("Coin");
                    }

                    else
                    {
                        CustomReference.Access.soundMix.Play("Paper");
                    }

                    //CREATE A GAMEOBJECT TO HOLD BET PREFABS!
                    if(betHolder == null)
                    {
                        betHolder = new GameObject("BetHolder"); 
                        betHolder.tag = "UserBet" ;
                    }

                    //INSTANTIATE THE CURRENT BILL PREFAB SELECTED!
                    Vector3 tempPos = new Vector3(hitInfo.point.x, hitInfo.point.y + CustomReference.Access.globalRefs.betHeight, hitInfo.point.z);
                    BillValue tempCoin = Instantiate(CustomReference.Access.globalRefs.billPrefabs[currentBill].transform, tempPos, Quaternion.identity).GetComponent<BillValue>();
                    tempCoin.transform.parent = betHolder.transform;

                    //UPDATE USER STATS INFO.
                    CustomReference.Access.userData.UpdateUserInfos (-currentBet, false, false);

                    CustomReference.Access.sabongWheel.userMenu.withdraw.interactable = true;
                }

                else
                {
                    bool pastReady = CustomReference.Access.sabongWheel.isReady;
                    CustomReference.Access.sabongWheel.isReady = false;
                    CustomReference.Access.dialogs.ShowNotify("Insufficient balance! Please lower down your choosen BILL to place as bet.", () => {
                        CustomReference.Access.sabongWheel.isReady = pastReady;
                    });
                }
            }

            else
            {
                CustomReference.Access.sabongWheel.isReady = false;
                CustomReference.Access.dialogs.store.Show();
            }
		}
	}
}
