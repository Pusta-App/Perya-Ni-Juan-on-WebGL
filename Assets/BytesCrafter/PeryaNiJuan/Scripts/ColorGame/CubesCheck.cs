using UnityEngine;

public class CubesCheck : MonoBehaviour
{
	void OnTriggerEnter (Collider col)
	{
		GetComponent<AudioSource> ().Play ();
	}
}
