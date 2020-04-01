
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using BytesCrafter.DataProcessor;
using BytesCrafter.DataProcessor.Tool;

public class forJSON
{
	private static forJSON jsonFor = null;
	public static forJSON Instance
	{
		get
		{
			if(jsonFor == null)
			{
				jsonFor = new forJSON ();
			}

			return jsonFor;
		}
	}

	public bool Exist(string fileName)
	{
		if ( File.Exists( forTOOL.rootFile(fileName, ".json") ))
		{
			Debug.LogWarning("FileSQL: File do exist!");
			return true;
		}

		else
		{
			Debug.LogWarning("FileSQL: File does not exist!");
			return false;
		}
	}

	public bool Exist(string fileName, string[] subFolders)
	{
		if ( File.Exists( forTOOL.subFile(fileName, subFolders, ".json") ))
		{
			Debug.LogWarning("FileSQL: File do exist!");
			return true;
		}

		else
		{
			Debug.LogWarning("FileSQL: File does not exist!");
			return false;
		}
	}

	public bool Insert<T>(string fileName, T fileContent)
	{
		if ( File.Exists( forTOOL.rootFile(fileName, ".json") ))
		{
			Debug.LogWarning("FileSQL: File already exist! Choose UPDATE instead.");
			return false;
		}

		string jsonString = JsonUtility.ToJson(fileContent);

		using (FileStream fs = File.Create( forTOOL.rootFile(fileName, ".json") ))
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes (jsonString); //Base64Encode (xmlString));

			for (int i = 0; i < bytes.Length; i++)
			{
				fs.WriteByte( bytes[i] );
			}
		}

		try
		{
			byte[] readBuffer = File.ReadAllBytes( forTOOL.rootFile(fileName, ".json") );
			string base64 = System.Text.Encoding.UTF8.GetString(readBuffer);

			if(jsonString.Equals(base64)) //Base64Decode(base64)))
			{
				return true;
			}

			else
			{
				return false;
			}
		}

		catch (IOException ioException)
		{
			Debug.LogError("FileSQL: Excemption - " + ioException.Message);
			return false;
		}
	}

	public bool Insert<T>(string fileName, string[] subFolders, T fileContent)
	{
		if ( File.Exists( forTOOL.subFile(fileName, subFolders, ".json") ))
		{
			Debug.LogWarning("FileSQL: File already exist! Choose UPDATE instead.");
			return false;
		}

		string jsonString = JsonUtility.ToJson(fileContent);

		using (FileStream fs = File.Create( forTOOL.subFile(fileName, subFolders, ".json") ))
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes (jsonString); //Base64Encode (xmlString));

			for (int i = 0; i < bytes.Length; i++)
			{
				fs.WriteByte( bytes[i] );
			}
		}

		try
		{
			byte[] readBuffer = File.ReadAllBytes( forTOOL.subFile(fileName, subFolders, ".json") );
			string base64 = System.Text.Encoding.UTF8.GetString(readBuffer);

			if(jsonString.Equals(base64)) //Base64Decode(base64)))
			{
				return true;
			}

			else
			{
				return false;
			}
		}

		catch (IOException ioException)
		{
			Debug.LogError("FileSQL: Excemption - " + ioException.Message);
			return false;
		}
	}

	public bool Update<T>(string fileName, T fileContent)
	{
		if ( !File.Exists( forTOOL.rootFile(fileName, ".json") ))
		{
			Debug.LogWarning("FileSQL: File dont exist! Choose UPDATE instead.");
			return false;
		}

		string jsonString = JsonUtility.ToJson(fileContent);

		using (FileStream fs = File.Open( forTOOL.rootFile(fileName, ".json"), FileMode.Create ))
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes (jsonString); //Base64Encode (xmlString) );

			for (int i = 0; i < bytes.Length; i++)
			{
				fs.WriteByte( bytes[i] );
			}
		}

		try
		{
			byte[] readBuffer = File.ReadAllBytes( forTOOL.rootFile(fileName, ".json") );
			string base64 = System.Text.Encoding.UTF8.GetString(readBuffer);

			if(jsonString.Equals(base64)) //Base64Decode(base64)))
			{
				return true;
			}

			else
			{
				return false;
			}
		}

		catch (IOException ioException)
		{
			Debug.LogError("FileSQL: Excemption - " + ioException.Message);
			return false;
		}
	}

	public bool Update<T>(string fileName, string[] subFolders, T fileContent)
	{
		if ( !File.Exists( forTOOL.subFile(fileName, subFolders, ".json") ))
		{
			Debug.LogWarning("FileSQL: File dont exist! Choose UPDATE instead.");
			return false;
		}

		string jsonString = JsonUtility.ToJson(fileContent);

		using (FileStream fs = File.Open( forTOOL.subFile(fileName, subFolders, ".json"), FileMode.Create ))
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes (jsonString); //Base64Encode (xmlString) );

			for (int i = 0; i < bytes.Length; i++)
			{
				fs.WriteByte( bytes[i] );
			}
		}

		try
		{
			byte[] readBuffer = File.ReadAllBytes( forTOOL.subFile(fileName, subFolders, ".json") );
			string base64 = System.Text.Encoding.UTF8.GetString(readBuffer);

			if(jsonString.Equals(base64)) //Base64Decode(base64)))
			{
				return true;
			}

			else
			{
				Debug.Log("AAAAAAAAA");
				return false;
			}
		}

		catch (IOException ioException)
		{
			Debug.LogError("FileSQL: Excemption - " + ioException.Message);
			return false;
		}
	}



	public T Select<T>(string fileName)
	{
		if ( !File.Exists( forTOOL.rootFile(fileName, ".json") ))
		{
			Debug.LogWarning("FileSQL: File does not exist! Check path or filename.");
			return default(T);
		}

		//FReading into byte[]
		byte[] readBuffer = File.ReadAllBytes( forTOOL.rootFile(fileName, ".json") );

		//Get string of the byte[]
		string base64 = System.Text.Encoding.UTF8.GetString( readBuffer );

		//Decode from base64 image.
		string stringData = base64; //Base64Decode(base64);

		return JsonUtility.FromJson<T> (stringData);
	}

	public T Select<T>(string fileName, string[] subFolders)
	{
		if ( !File.Exists( forTOOL.subFile(fileName, subFolders, ".json") ))
		{
			Debug.LogWarning("FileSQL: File does not exist! Check path or filename.");
			return default(T);
		}

		//FReading into byte[]
		byte[] readBuffer = File.ReadAllBytes( forTOOL.subFile(fileName, subFolders, ".json") );

		//Get string of the byte[]
		string base64 = System.Text.Encoding.UTF8.GetString( readBuffer );

		//Decode from base64 image.
		string stringData = base64; //Base64Decode(base64);

		return JsonUtility.FromJson<T> (stringData);
	}



	public void Delete(string fileName)
	{
		if ( !File.Exists( forTOOL.rootFile(fileName, ".json") ))
		{
			Debug.LogWarning("FileSQL: File does not exist! Check path or filename.");
			return;
		}

		//Delete into file
		File.Delete( forTOOL.rootFile(fileName, ".json") );
	}

	public void Delete(string fileName, string[] subFolders)
	{
		if ( !File.Exists( forTOOL.subFile(fileName, subFolders, ".json") ))
		{
			Debug.LogWarning("FileSQL: File does not exist! Check path or filename.");
			return;
		}

		//Delete into file
		File.Delete( forTOOL.subFile(fileName, subFolders, ".json") );
	}
}