using System;
using System.IO;
using System.Collections.Generic;
using ContactSerialiserLibrary.Models;
using ContactSerialiserLibrary.Enums;
using ContactSerialiserLibrary.Interfaces;
using ContactSerialiserLibrary.Serializers.ExportTypes;
//using System.ComponentModel;

namespace ContactSerialiserLibrary.Serializers
{
	public class ContactSerializer
	{
		//private Container container = new Container();
		private IFileChecker fileChecker = new FileChecker();
		private IFileWriter fileWriter = new FileWriter();

		public ContactSerializer()
		{

		}

		/// <summary>
		/// Не понял, что должен делать этот конструктор, кроме как принимать имя файла
		/// </summary>
		/// <param name="fileName"></param>
		//public ContactSerializer(string fileName)
		//{
		//	//container.Add(IFileChecker, "fileChecker");
		//	if (fileChecker.CheckFileName(fileName))
		//	{
		//		using (var fStream = new FileStream(fileName, FileMode.Open))
		//		{

		//		}
		//	}
		//}

		public void SerializeToJSON(Contact person, string fileName = "Contact.txt")
		{
			IExportToJSON toJSON = new ExportToJSON();

			toJSON.SerializeToJSON(person,fileName);
		}

		public void SerializeToJSON(Contact[] persons, string fileName = "Contact.txt")
		{
			IExportToJSON toJSON = new ExportToJSON();

			toJSON.SerializeToJSON(persons, fileName);
		}

		public void SerializeToExcel(Contact person, string fileName = "Contact.xlsx")
		{
			IExportToExcel exportToExcel = new ExportToExcel();
			exportToExcel.SerializeToExcel(person, fileName);
		}

		public Contact Deserialize(string fileName)
		{
			if (fileChecker.CheckFileName(fileName))
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

	}
}