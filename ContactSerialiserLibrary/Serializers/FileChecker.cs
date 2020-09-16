using ContactSerialiserLibrary.Interfaces;
using System.ComponentModel;
using System.IO;

namespace ContactSerialiserLibrary.Serializers
{
	public class FileChecker : IFileChecker
	{
		/// <summary>
		/// Если файл существует - возвращает true
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public bool CheckFileName(string fileName)
		{
			if (fileName.Length > 3)
			{
				if (File.Exists(fileName))
				{
					return true;
				}
			}
			return false;
		}

		public bool CheckFileNameForWriteFile(string fileName)
		{
			if (!CheckFileName(fileName))
			{
				if (fileName.Length > 3)
				{
					return true;
				}
			}
			//throw new FileLoadException();
			//Console.WriteLine("Файл существует");

			return false;
		}
	}
}
