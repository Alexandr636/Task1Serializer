using Task1Serializator.Models;
using System;
using System.IO;
using Task1Serializator.Enums;

namespace Task1Serializator
{
	public class ContactSerializer
	{
		public ContactSerializer()
		{

		}

		/// <summary>
		/// Не понял, что должен делать этот конструктор, кроме как принимать имя файла
		/// </summary>
		/// <param name="fileName"></param>
		public ContactSerializer(string fileName)
		{
			if (CheckFileName(fileName))
			{
				var fStream = new FileStream(fileName, FileMode.Open);
			}
		}

		/// <summary>
		/// Сохраняет сериализованый класс в файл
		/// </summary>
		/// <param name="person"></param>
		public void Serialize(Contact person, string fileName = "Contact.txt")
		{
			if (CheckFileName(fileName))
			{
				throw new FileLoadException();
			}
			else
			{
				if (fileName.Length > 3)
				{
					byte[] qwe = System.Text.Encoding.Default.GetBytes(person.ToString());
					WriteInFile(fileName, qwe);
				}
			}
		}

		public void Serialize(Contact[] person, string fileName = "Contact.txt")
		{
			if (CheckFileName(fileName))
			{
				throw new FileLoadException();
			}
			else
			{
				if (fileName.Length > 3)
				{
					byte[] qwe = System.Text.Encoding.Default.GetBytes(person.ToString());
					WriteInFile(fileName, qwe);
				}
			}
		}

		public Contact Deserialize(string fileName)
		{
			if (CheckFileName(fileName))
			{
				var fStream = new FileStream(fileName, FileMode.Open);
				
			}
			var testReturn = new Contact("", "", "", Gender.Male, new DateTime(1997, 4, 6), "345345345", new Address(), "7943234234");
			return testReturn;
		}

		private bool CheckFileName(string fileName)
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

		private void WriteInFile(string fileName, byte[] bytedText)
		{
			var fStream = new FileStream(fileName, FileMode.CreateNew);
			fStream.WriteAsync(bytedText, 0, bytedText.Length);
		}
	}
}
