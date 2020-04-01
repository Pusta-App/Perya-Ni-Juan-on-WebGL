using UnityEngine;

public class BC_UTILS_OpenURL : MonoBehaviour
{
	[Header("GMAIL EMAIL INFO")]

	public string email = "";
	public string subject = "";

	[TextArea(1, 5)]
	public string message = "";

	public void OpenThisURL(string url)
	{
		Application.OpenURL (url);
	}

	public void EmailOnGmail()
	{
		string[] initial = subject.Split (new char[] { ' ' });
		string customSubject = initial [0];

		for(int index = 1; index < initial.Length; index++)
		{
			customSubject += "+" + initial [index];
		}

		Application.OpenURL ("https://mail.google.com/mail/u/0/?view=cm&su=" + customSubject + "&to=" + email + "&body=" + message + "&fs=1&tf=1");
	}

	public void ShareOnGooglePlus()
	{
		string shareURL = "https://plus.google.com/share?url=https://play.google.com/store/apps/details?id=" + Application.identifier;

		Application.OpenURL (shareURL);
	}

	[Header("FACEBOOK SHARE INFO")]
	public string title = "";

	public void ShareOnFacebook()
	{
		string shareURL = "https://www.facebook.com/share.php?u=https://play.google.com/store/apps/details?id=" + Application.identifier;

		Application.OpenURL (shareURL);
	}


	public string facebookPage;
	public void LikeOurFacebook()
	{
		Application.OpenURL (facebookPage);
	}

	[Header("TWEETER SHARE INFO")]
	public string content = "";
	public bool useDefault = true;

	public void ShareOnTweeter()
	{
		string[] initial = content.Split (new char[] { ' ' });
		string customContent = initial [0];

		for(int index = 1; index < initial.Length; index++)
		{
			customContent += "%20" + initial [index];
		}

		if(useDefault)
		{
			customContent = "Now available at Google Play! Download Now for FREE. Please share!";
		}

		string shareURL = "https://twitter.com/intent/tweet?text=" + customContent 
			+ "&source=sharethiscom&related=BytesCrafter&via=BytesCrafter&url=" 
			+ "https://play.google.com/store/apps/details?id=" + Application.identifier;

		Application.OpenURL (shareURL);
		//CustomAdmob.Access.Admob_ShowIntertitial ();
	}

	public void RateThisGame()
	{
		Application.OpenURL ("https://play.google.com/store/apps/details?id=" + Application.identifier);
	}

	public void RequestGame()
	{
		Application.OpenURL ("https://www.facebook.com/PeryaNiJuan");
	}
}