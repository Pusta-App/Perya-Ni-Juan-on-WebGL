using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class BC_UTILS_AppInfo : MonoBehaviour 
{
	public string subTitle;

	[Header("REFERENCE")]
	public Text subTitleRef;
	public Text versionRef;

	void Start () 
	{
		subTitleRef.text = subTitle;
		versionRef.text = "Version " + Application.version;
	}
}
