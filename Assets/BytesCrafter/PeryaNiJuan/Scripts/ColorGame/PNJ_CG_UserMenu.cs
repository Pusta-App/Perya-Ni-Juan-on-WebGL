using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PNJ_CG_UserMenu : MonoBehaviour 
{
	public Button home;
	public Button help;
	public Button settings;

	[Header("DRAW OR RESET")]
	public Sprite drawIcon;
	public Sprite resetIcon;
	public Image drawReset;

	public void DrawOrReset(bool toResetIcon)
	{
		drawReset.sprite = toResetIcon ? resetIcon : drawIcon;
	}

	public void Buttons(bool show)
	{
		home.interactable = show;
		help.interactable = show;
		settings.interactable = show;
	}

	void OnEnable ()
	{
		if( handleAnim != null && withdrawBet != null )
		{
			handleAnim.enabled = false;
			withdrawBet.interactable = false;
		}
	}

	#region Extras Mechanism

	public void SetRaycast(bool enabled)
	{
		StartCoroutine (Enabling());
	}

	private IEnumerator Enabling()
	{
		yield return new WaitForSeconds (0.49f);
		CustomReference.Access.colorGame.state = PNJ_PP_State.BETTING;
	}

	#endregion

	public Button withdrawBet;
	public Animator handleAnim;
	
	private float timer;
	void Update()
	{
		//timer += Time.deltaTime;	
		if(timer > 1f)
		{			
			if( CustomReference.Access.colorGame.state == PNJ_PP_State.BETTING )
			{
				if( !CustomReference.Access.colorGame.cubeChecker.cubeFallDown  )
				{
					withdrawBet.interactable = CustomReference.Access.colorGame.releaseControl.slider.value > 0.95f ? true : false;
				}

				else
				{
					withdrawBet.interactable = false;
				}
			}

			else
			{
				withdrawBet.interactable = false;
			}
			timer = 0f;			
		}
	}

	public void StartedBetting()
	{
		handleAnim.enabled = true;
		withdrawBet.interactable = true;
	}

	public void WithdrawnBet()
	{
		handleAnim.enabled = false;
		withdrawBet.interactable = false;
	}
}