using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace BytesCrafter.DataProcessor.Tool
{
	public class forTOOL
	{
		public static string rootFile(string fileName, string extention)
		{
			string path = Path.Combine (Application.dataPath, "Data");
			if(!Directory.Exists (path)) { Directory.CreateDirectory (path); }

			string fileExtention = extention;
			return Path.Combine (path, fileName + fileExtention);
		}

		public static string subFile(string fileName, string[] subFolder, string extention)
		{
			string path = Path.Combine (Application.dataPath, "Data");
			if(!Directory.Exists (path)) { Directory.CreateDirectory (path); }

			string curPath = path;
			for(int i = 0; i < subFolder.Length; i++)
			{
				curPath = Path.Combine (curPath, subFolder[i]);
				if(!Directory.Exists (curPath)) { Directory.CreateDirectory (curPath); }
			}

			string fileExtention = extention;
			return Path.Combine (curPath, fileName + fileExtention);
		}
	}
}