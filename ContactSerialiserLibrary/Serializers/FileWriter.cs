using ContactSerialiserLibrary.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace ContactSerialiserLibrary.Serializers
{
	public class FileWriter : IFileWriter
	{
		public async void WriteInFile(string fileName, byte[] bytedText)
		{
			using (var fStream = new FileStream(fileName, FileMode.CreateNew))
			{
				await fStream.WriteAsync(bytedText, 0, bytedText.Length);
			}

		}

		public async void WriteListInFile(string fileName, List<byte[]> byteList)
		{
			using (var fStream = new FileStream(fileName, FileMode.CreateNew))
			{
				foreach (var byteArray in byteList)
					await fStream.WriteAsync(byteArray, 0, byteArray.Length);
			}
		}
	}
}
