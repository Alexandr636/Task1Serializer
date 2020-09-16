using ContactSerialiserLibrary.Interfaces;
using ContactSerialiserLibrary.Models;
using System.Collections.Generic;


namespace ContactSerialiserLibrary.Serializers.ExportTypes
{
	public class ExportToJSON : IExportToJSON
	{
		private IFileChecker fileChecker = new FileChecker();
		private IFileWriter fileWriter = new FileWriter();

		/// <summary>
		/// Сохраняет сериализованый класс в файл
		/// </summary>
		/// <param name="person"></param>
		public void SerializeToJSON(Contact person, string fileName = "Contact.txt")
		{
			if (fileChecker.CheckFileNameForWriteFile(fileName))
			{
				byte[] qwe = System.Text.Encoding.Default.GetBytes(person.ToString());
				fileWriter.WriteInFile(fileName, qwe);
			}
		}

		/// <summary>
		/// Позволяет сериализовать массив Contact[]
		/// </summary>
		/// <param name="persons"></param>
		/// <param name="fileName"></param>
		public void SerializeToJSON(Contact[] persons, string fileName = "Contact.txt")
		{
			if (fileChecker.CheckFileNameForWriteFile(fileName))
			{
				var byteList = new List<byte[]>();
				foreach (var person in persons)
				{
					byteList.Add(System.Text.Encoding.Default.GetBytes(person.ToString()));
				}
				fileWriter.WriteListInFile(fileName, byteList);
			}

		}
	}
}
