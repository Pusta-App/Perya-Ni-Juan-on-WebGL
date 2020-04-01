using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PNJ_CG_Bet
{
	public int chipAmount = 0;
	public int chipNumber = 0;
	public int chipTotal
	{
		get
		{
			return chipAmount * chipNumber;
		}
	}

	public PNJ_CG_Bet(int amount, int number) 
	{
		chipAmount = amount;
		chipNumber = number;
	}
}

[System.Serializable]
public class PNJ_CG_Bets
{
	public List<PNJ_CG_Bet> list = new List<PNJ_CG_Bet>();

	public int getBetsTotal
	{
		get
		{
			int totalBet = 0;
			foreach(PNJ_CG_Bet bet in list)
			{
				totalBet += bet.chipTotal;
			}
			return totalBet;
		}
	}

	public int calcBetsWin(int colorFrequency)
	{
		return getBetsTotal * colorFrequency;
	}
}

[System.Serializable]
public class PNJ_CG_Result
{
	public int[] result = new int[6];
	public List<PNJ_CG_Bets> colorBets = new List<PNJ_CG_Bets>(6);

	public PNJ_CG_Result(int[] _result)
	{
		result = _result;
		colorBets = new List<PNJ_CG_Bets>(6);
		colorBets.Add ( new PNJ_CG_Bets() );
		colorBets.Add ( new PNJ_CG_Bets() );
		colorBets.Add ( new PNJ_CG_Bets() );
		colorBets.Add ( new PNJ_CG_Bets() );
		colorBets.Add ( new PNJ_CG_Bets() );
		colorBets.Add ( new PNJ_CG_Bets() );
	}

	public int getTotalPlayBet
	{
		get
		{
			int totalPlayBet = 0;
			foreach(PNJ_CG_Bets result in colorBets)
			{
				totalPlayBet += result.getBetsTotal;
			}
			return totalPlayBet;
		}
	}

	public int getTotalReceived
	{
		get
		{
			int totalPlayBet = 0;
			for(int i = 0; i < colorBets.Count; i++)
			{
				totalPlayBet += colorBets[i].calcBetsWin( result[i] * 2 );
			}
			return totalPlayBet;
		}
	}

	public int getTotalPlayWin
	{
		get
		{
			int totalPlayBet = 0;
			for(int i = 0; i < colorBets.Count; i++)
			{
				totalPlayBet += colorBets[i].calcBetsWin( result[i] );
			}
			return totalPlayBet;
		}
	}

	public void UpdateBets(int colorIndex, int chipAmount)
	{
		if(CustomReference.Access.colorGame.colorPicker.playResult.colorBets[colorIndex].list.Exists( x => x.chipAmount == chipAmount ))
		{
			CustomReference.Access.colorGame.colorPicker.playResult.colorBets [colorIndex].list.Find ( x => x.chipAmount == chipAmount ).chipNumber += 1;
		}

		else
		{
			CustomReference.Access.colorGame.colorPicker.playResult.colorBets [colorIndex].list.Add ( new PNJ_CG_Bet (chipAmount, 1) );
		}
	}
}

public class PNJ_CG_Checker : MonoBehaviour
{
	[Header("CUBE RIGIDBODIES")]
	public List<Rigidbody> cubePhysics = new List<Rigidbody> ();

	[Header("Radomization")]
	[Range(0f, 1f)]
	public float centerMassPercent = 0.5f;
	public float centerMassRandom 
	{
		get 
		{ 
			return centerMassPercent; 
		}

		set 
		{ 
			centerMassPercent = value;
		}
	}

	[Header("Debugger Information")]
	private float[] rotRandom = new float[4]{0F, 90F, 180F, 270F};
	public Vector3[] cubesPosition = new Vector3[3];
	public Quaternion[] cubesRotation = new Quaternion[3];

	public bool cubeFallDown = false;
	[HideInInspector] public List<GameObject> cubeChecked = new List<GameObject> ();

	/// Raises the trigger enter event when a cube pass.
	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag == "ColorCube")
		{
			if( !cubeChecked.Exists(x => x.name == collider.name) ) 
			{
				if( cubeChecked.Count <= 3 ) //Follow max cube list.
				{
					cubeChecked.Add(collider.gameObject);
				}
			}

			if(cubeChecked.Count > 0)
			{
				cubeFallDown = true;
				CustomReference.Access.colorGame.userMenu.Buttons(false);
				CustomReference.Access.colorGame.state = PNJ_PP_State.ONDRAW;
			}

			if(cubeChecked.Count == 3)
			{
				cubeChecked = new List<GameObject> ();
				CustomReference.Access.soundMix.Play ("Release");

				StartCoroutine (CheckingResult());
			}
		}
	}

	private bool isAllCubesSteady()
	{
		return (cubePhysics.FindAll (x => x.IsSleeping ()).Count == 3);
	}

	private IEnumerator CheckingResult()
	{
		CustomReference.Access.colorGame.userMenu.withdrawBet.interactable = false;
		
		//Show Loading UI.
		CustomReference.Access.colorGame.lowerBet.Set(false);
		CustomReference.Access.colorGame.releaseControl.Set (false);
		CustomReference.Access.colorGame.userMenu.handleAnim.enabled = false;

		yield return new WaitUntil(() => isAllCubesSteady() == true);
		yield return new WaitForSeconds(CustomReference.Access.colorGame.checkWait);
		//Hide Loading UI.

		int[] cubesResult = new int[6];
		for(int index = 0; index < transform.childCount; index++)
		{
			RaycastHit hitCheck = new RaycastHit();
			if(Physics.Raycast (transform.GetChild(index).position, Vector3.up, out hitCheck))
			{
				CustomReference.Access.Debugs ("Cube " + hitCheck.collider.transform.GetSiblingIndex () + ": " + 1);
				cubesResult[hitCheck.collider.transform.GetSiblingIndex ()] += 1;
				Debug.DrawLine(transform.GetChild(index).position, hitCheck.point, Color.red, 1f);
			}
		}
		CustomReference.Access.colorGame.colorPicker.playResult.result = cubesResult;
		CustomReference.Access.dialogs.result.ShowDisplay (CustomReference.Access.colorGame.colorPicker.playResult);
	}

	public bool clickToRefreshCube = false;
	void OnValidate()
	{
		if( clickToRefreshCube )
		{
			for (int index = 0; index < transform.childCount; index++)
			{
				cubesPosition[index] = transform.GetChild (index).position;
				cubesRotation[index] = transform.GetChild (index).rotation;
			}
			clickToRefreshCube = false;
		}
	}

	public void RandomCubeTransform () 
	{
		for(int index = 0; index < transform.childCount; index++)
		{
			Vector3 vSize = transform.GetChild (index).lossyScale;
			float size = ((vSize.x + vSize.y + vSize.z) / 3) * centerMassRandom;
			float cogX = Random.Range (-size, size);
			float cogY = Random.Range (-size, size);
			float cogZ = Random.Range (-size, size);
			transform.GetChild(index).GetComponent<Rigidbody>().centerOfMass = new Vector3(cogX, cogY, cogZ);

			transform.GetChild(index).position = cubesPosition[index];
			float quatX = cubesRotation [index].x + rotRandom [Random.Range (0, 3)];
			float quatY = cubesRotation [index].y + rotRandom [Random.Range (0, 3)];
			float quatZ = cubesRotation [index].z + rotRandom [Random.Range (0, 3)];
			transform.GetChild(index).localRotation = Quaternion.Euler (quatX, quatY, quatZ);
		}
	}
}