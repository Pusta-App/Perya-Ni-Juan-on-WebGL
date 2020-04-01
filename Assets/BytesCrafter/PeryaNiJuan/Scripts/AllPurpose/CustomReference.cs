using UnityEngine;

public enum PNJ_GameState
{
	MainMenu,
	ColorGame,
	DropBall,
	SabongWheel
}

public class CustomReference 
{
	private static CustomReference access = null;
	public static CustomReference Access 
	{
		get 
		{
			if (access == null) 
			{
				access = new CustomReference();
			}
			return access;
		}
	}

	public PNJ_GameState gameState = PNJ_GameState.MainMenu;

	public PNJ_SDK_MultiAds multiads;
	public PNJ_UserData userData;	

	public PNJ_RemoteConfigs remoteConfigs;
	

	public BC_UTILS_OpenURL openURL;
	public PNJ_DialogManager dialogs;
	public PNJ_SoundMixer soundMix;
	public PNJ_UserSettings settings;

	public PNJ_CameraControl cameraControl;

	public PNJ_PlayGames_Logic pglogic;
	public PNJ_GlobalRefs globalRefs;
	public PNJ_GlobalCalls globalCalls;
	public PNJ_CG_Engine colorGame;
	public PNJ_PP_Engine dropBall;
	public PNJ_MR_Engine sabongWheel;

	public PW_ResultInfo resultInfo;
	private CustomReference() 
	{
		multiads = GameObject.FindObjectOfType<PNJ_SDK_MultiAds>();
		
		userData = GameObject.FindObjectOfType<PNJ_UserData>();

		remoteConfigs = GameObject.FindObjectOfType<PNJ_RemoteConfigs>();
		//colorGame = GameObject.FindObjectOfType<PNJ_CG_Engine>();
		//dropBall = GameObject.FindObjectOfType<PNJ_PP_Engine>();

		openURL = GameObject.FindObjectOfType<BC_UTILS_OpenURL>();
		dialogs = GameObject.FindObjectOfType<PNJ_DialogManager>();
		soundMix = GameObject.FindObjectOfType<PNJ_SoundMixer>();

		settings = GameObject.FindObjectOfType<PNJ_UserSettings>();
		cameraControl = GameObject.FindObjectOfType<PNJ_CameraControl>();

		pglogic = GameObject.FindObjectOfType<PNJ_PlayGames_Logic>();
		globalRefs = GameObject.FindObjectOfType<PNJ_GlobalRefs>();
		globalCalls = GameObject.FindObjectOfType<PNJ_GlobalCalls>();
		resultInfo = GameObject.FindObjectOfType<PW_ResultInfo>();
	}

	public void Initialized()
	{
		//userDatabase.UpdateUserInfos ();
		CustomReference.Access.Debugs ("CustomReferences has been initiated!");

		//Auto login using play games sdk.
		//PlayMate.Access.Initialized();
		//PlayMate.Access.Authenticate(userInterfaces);
		
	}

	/// <summary>
	/// Gets the current raycast info.
	/// </summary>
	/// <value>RaycastHit</value>
	public RaycastHit RaycastReturn
	{
		get 
		{
			//Cursor or Touch raycast screen position.
			Vector3 castPos = Vector3.zero;
			if(Application.isMobilePlatform)
			{
				castPos = Input.GetTouch (0).position;
			}

			else
			{
				castPos = Input.mousePosition;
			}

			//Initialized Raycast!
			Ray rayTouch = Camera.main.ScreenPointToRay(castPos);
			RaycastHit hitInfo = new RaycastHit();
			if (Physics.Raycast (rayTouch, out hitInfo)) 
			{
				Debug.DrawLine (rayTouch.origin, hitInfo.point, Color.black, 1f, true);
			}

			return hitInfo;
		}
	}

	public void Debugs(string message)
	{
		#if UNITY_EDITOR
			Debug.LogWarning("Bytes Crafter: " + message);
		#endif
	}
}