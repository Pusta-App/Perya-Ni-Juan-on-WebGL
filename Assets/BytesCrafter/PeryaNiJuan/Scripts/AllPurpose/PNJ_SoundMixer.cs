using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ_SoundMixer : MonoBehaviour 
{
	public List<AudioSource> soundEffects;
	public AudioListener audioListener;

	public void Play(string childName)
	{
		if(soundEffects.Exists (x => x.gameObject.name == childName))
		{
			soundEffects.Find (x => x.gameObject.name == childName).Play();
		}
	}

	public void Stop(string childName)
	{		
		if(soundEffects.Exists (x => x.gameObject.name == childName))
		{
			soundEffects.Find (x => x.gameObject.name == childName).Stop();
		}
	}

	public void Stop(string childName, float fadeTime)
	{
		StartCoroutine( Stopping(childName, fadeTime) );
	}
	IEnumerator Stopping(string childName, float fadeTime)
	{
		if(soundEffects.Exists (x => x.gameObject.name == childName))
		{
			AudioSource audioSource = soundEffects.Find (x => x.gameObject.name == childName);

			float startVolume = audioSource.volume;
 
			
			while (audioSource.volume > 0) {
				audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
	
				yield return null;
			}
	
			audioSource.Stop ();
			audioSource.volume = startVolume;
		}

		yield return new WaitForEndOfFrame();
	}
}