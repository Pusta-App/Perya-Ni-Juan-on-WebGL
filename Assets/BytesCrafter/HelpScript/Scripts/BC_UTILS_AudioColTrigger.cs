using UnityEngine;

public class BC_UTILS_AudioColTrigger : MonoBehaviour
{
	private AudioSource audioSource = null;

	void Start()
	{
		audioSource = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter (Collider col)
	{
		audioSource.Play ();
	}
}
