using Task1Serializator.Models;
using System;
using System.IO;
using Task1Serializator.Enums;
using System.Reflection;
using System.Collections.Generic;

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
				using (var fStream = new FileStream(fileName, FileMode.Open))
				{

				}
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
				//throw new FileLoadException();
				Console.WriteLine("Файл существует");
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
				using (var fStream = new FileStream(fileName, FileMode.Open))
				{
					var bytedContact = new byte[fStream.Length];
					var a = fStream.Read(bytedContact, 0, bytedContact.Length);
					string stringedContact = System.Text.Encoding.Default.GetString(bytedContact);
					var contact = TranslateToContact(stringedContact);
					return contact;
				}
			}
			return null;
		}

		private Contact TranslateToContact(string stringedContact)
		{
			var address = new Address();
			var contact = new Contact(null,null,null,Gender.Male,DateTime.MinValue,null,null,null);
			var contactType = contact.GetType();
			var addressType = address.GetType();
			var contactPropArray = contactType.GetProperties();
			var addressPropArray = addressType.GetProperties();
			var dictionaredContact = new Dictionary<string,string>();
			var splitedContact = PrepareStringedContact(stringedContact); 
			
			for (var elemNumber = 0; elemNumber < splitedContact.Length; elemNumber++)
			{
				var index = splitedContact[elemNumber].IndexOf(':');

				if (index > 0)
				{
					var key = splitedContact[elemNumber].Split(':')[0].Trim();
					var value = splitedContact[elemNumber].Split(':')[1];
					dictionaredContact.Add(key, value);
				}
				else
				{
					var key = splitedContact[elemNumber-1].Split(':')[0].Trim();
					dictionaredContact[key] += splitedContact[elemNumber];
				}
			}

			foreach (var property in contactPropArray)
			{
				switch (property.Name)
				{
					case "SecondName":
						contact.SecondName = dictionaredContact[property.Name];
						break;
					case "FirstName":
						contact.FirstName = dictionaredContact[property.Name];
						break;
					case "ThirdName":
						contact.ThirdName = dictionaredContact[property.Name];
						break;
					case "Gender":
						var gender = new Gender();
						if (Enum.TryParse(dictionaredContact[property.Name], true, out gender))
						{
							contact.Gender = gender;
						}
						break;
					case "BirthDate":
						var birthDate = new DateTime();
						dictionaredContact[property.Name] = dictionaredContact[property.Name].Remove(dictionaredContact[property.Name].LastIndexOf('0'), 1);
						if (DateTime.TryParse(dictionaredContact[property.Name], out birthDate))
						{
							contact.BirthDate = birthDate;
						}
						break;
					case "INN":
						contact.INN = dictionaredContact[property.Name];
						break;
					case "PhoneNumber":
						contact.PhoneNumber = dictionaredContact[property.Name];
						break;
				}

				
			}

			foreach (var property in addressPropArray)
			{
				switch(property.Name) {
					case "AddressType":
						var typeOfAddress = new TypeOfAddress();
						if (Enum.TryParse(dictionaredContact[property.Name], true, out typeOfAddress))
						{
							address.AddressType = typeOfAddress;
						}
						break;
					case "Country":
						address.Country = dictionaredContact[property.Name];
						break;
					case "City":
						address.City = dictionaredContact[property.Name];
						break;
					case "CityAddress":
						address.CityAddress = dictionaredContact[property.Name];
						break;
				}
			}

			contact.Address = address;

			return contact;
		}

		/// <summary>
		/// Возвращает разделённый по запятым и удалёнными названиями классов
		/// </summary>
		/// <param name="stringedContact"></param>
		/// <returns></returns>
		private string[] PrepareStringedContact(string stringedContact)
		{
			var indexOfContact = stringedContact.IndexOf('{');
			stringedContact = stringedContact.Remove(0,indexOfContact + 1);
			var indexOfAddress = stringedContact.IndexOf('{');
			stringedContact = stringedContact.Remove(indexOfAddress-8, 9);
			var splitedContact = stringedContact.Split(',');
			return splitedContact;
		}

		/// <summary>
		/// Если файл существует - возвращает true
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
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
		
		private async void  WriteInFile(string fileName, byte[] bytedText)
		{
			using (var fStream = new FileStream(fileName, FileMode.CreateNew))
			{
				await fStream.WriteAsync(bytedText, 0, bytedText.Length);
			}
		}
	}
}