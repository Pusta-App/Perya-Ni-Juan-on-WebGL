using UnityEngine;
using UnityEngine.UI;
using System;

public class BC_UTILS_ImageURL
{
	private static BC_UTILS_ImageURL access = null;
	public static BC_UTILS_ImageURL Access 
	{
		get 
		{
			if (access == null) 
			{
				access = new BC_UTILS_ImageURL();
			}
			return access;
		}
	}

	public Texture2D Download(string url)
	{
		WWW textureData = new WWW(url);
		while(!textureData.isDone){}
		return textureData.texture;
	}

	public void Download(string url, RawImage display)
	{
		WWW textureData = new WWW(url);
		while(!textureData.isDone){}
		display.texture = textureData.texture;
	}

	public byte[] ConvertToBytes(Texture2D texture)
	{
		byte[] bytes = texture.EncodeToJPG ();
		return bytes;
	}

	public string ConvertToString(Texture2D texture)
	{
		byte[] bytesValue = texture.GetRawTextureData ();
		string stringValue = Convert.ToBase64String (bytesValue);
		return stringValue;
	}

	public Texture2D ConvertToTexture(string stringData)
	{
		byte[] bytesValue = Convert.FromBase64String (stringData);
		Texture2D newTexture = new Texture2D (1, 1);
		newTexture.LoadRawTextureData (bytesValue);
		newTexture.Apply ();
		return newTexture;
	}

	public Texture2D ConvertToTexture(byte[] bytesValue)
	{
		Texture2D newTexture = new Texture2D (1, 1);
		newTexture.LoadRawTextureData (bytesValue);
		newTexture.Apply ();
		return newTexture;
	}
}