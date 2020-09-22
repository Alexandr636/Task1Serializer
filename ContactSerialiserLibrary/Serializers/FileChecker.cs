using ContactSerialiserLibrary.Interfaces;
using log4net;
using System.IO;

namespace ContactSerialiserLibrary.Serializers
{
	public class FileChecker : IFileChecker
	{
		//ConfirationManager
		//LogManager.GetLogger("LOGGER");

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
			//throw new Exception();
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
