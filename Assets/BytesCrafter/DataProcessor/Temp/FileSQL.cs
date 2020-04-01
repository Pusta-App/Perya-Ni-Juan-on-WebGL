
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BytesCrafter.DataProcessor;

public class FileSQL
{
	public static forXML XML
	{
		get
		{
			return forXML.Instance;
		}
	}

	public static forJSON JSON
	{
		get
		{
			return forJSON.Instance;
		}
	}

	public static bool IsBase64Image(string base64)
	{
		base64 = base64.Trim();
		return (base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
	}

	public static string Base64Encode(string plainText)
	{
		var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
		return System.Convert.ToBase64String(plainTextBytes);
	}

	public static string Base64Decode(string base64EncodedData)
	{
		var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
		return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
	}

	//Attendance - for years. or Attendance/Year - for nonths
	public static string[] GetDirectories(string subRootPath)
	{
		string path = Path.Combine (Application.dataPath, "Data");
		if(!Directory.Exists (path)) { Directory.CreateDirectory (path); }

		string paths = Path.Combine (path, subRootPath);
		if(!Directory.Exists (paths)) { Directory.CreateDirectory (paths); }

		List<string> dirs = new List<string> ();
		dirs.AddRange(Directory.GetDirectories (paths));

		if(dirs.Count > 0 )
		{
			for(int i = 0; i < dirs.Count; i++)
			{
				string[] dirInfo = Serializer.StringSplit(dirs[i], Path.DirectorySeparatorChar.ToString(), false);
				dirs[i] = dirInfo[dirInfo.Length - 1];
			}

			return dirs.ToArray();
		}

		else
		{
			return null;
		}
	}

	public static string[] GetFiles(string subRootPath, string nextPath, string extentions)
	{
		string path = Path.Combine (Application.dataPath, "Data");
		if(!Directory.Exists (path)) { Directory.CreateDirectory (path); }

		string paths = Path.Combine (path, subRootPath);
		if(!Directory.Exists (paths)) { Directory.CreateDirectory (paths); }

		string filePath = Path.Combine (paths, nextPath);
		if(Directory.Exists (filePath))
		{ 
			string[] files = Directory.GetFiles (filePath, "*" + extentions);
			List<string> fs = new List<string> ();
			for(int i = 0; i < files.Length; i++)
			{
				string[] dirInfo = Serializer.StringSplit(files[i], Path.DirectorySeparatorChar.ToString(), false);
				string[] lastDir = Serializer.StringSplit(dirInfo[dirInfo.Length - 1], "@", false);

				//dirs[i] = dirInfo[dirInfo.Length - 1];

				fs.Add (lastDir[0]);
			}

			return fs.ToArray ();
		}

		else
		{
			return new string[0];
		}
	}
}