using System.Collections;
using UnityEngine;

public class PNJ_CG_Blocker : MonoBehaviour
{
	public bool isReady = false;
	public bool colorBlock = false;
	public Animator colorBlocker;

	public void Set(bool active)
	{
		if(active)
		{
			if(!colorBlock)
			{
				colorBlocker.SetTrigger ("Show");
				colorBlock = true;
			}
		}

		else
		{
			if(colorBlock)
			{
				colorBlocker.SetTrigger ("Hide");
				colorBlock = false;
			}
		}
	}
}