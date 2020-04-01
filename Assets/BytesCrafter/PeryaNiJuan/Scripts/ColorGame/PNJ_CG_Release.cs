
using UnityEngine;
using UnityEngine.UI;

public class PNJ_CG_Release : MonoBehaviour 
{
	public Scrollbar slider;
	public Transform holder;
	public Button randomizer;
	public Animator resetAnim;
	public Image resdomIcon;
	public Sprite resetIcon;
	public Sprite randomIcon;

	public void Reset()
	{
		slider.value = 1f;
		randomizer.interactable = true;

		resdomIcon.sprite = randomIcon;
		if(resetAnim.GetCurrentAnimatorStateInfo(0).IsName("Active"))
		{
			resetAnim.SetTrigger("Deactivate");
			resetAnim.transform.localScale = Vector3.one;
		}
	}

	public void Set(bool enabled)
	{
		randomizer.interactable = enabled;
	}

	void OnEnable()
	{
		resdomIcon.sprite = randomIcon;
	}

	public void SetToResetIcon(bool deactivate)
	{
		resdomIcon.sprite = resetIcon;
		if(deactivate)
		{
			resetAnim.SetTrigger ("Deactivate");
		}
	}

	public void Animate(bool yes)
	{
		if(yes)
		{
			resdomIcon.sprite = resetIcon;
			resetAnim.SetTrigger ("Activate");
		}

		else
		{
			resdomIcon.sprite = randomIcon;
			resetAnim.SetTrigger ("Deactivate");
		}
	}
}