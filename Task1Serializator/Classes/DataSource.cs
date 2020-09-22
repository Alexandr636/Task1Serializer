using System;
using System.Collections.Generic;
using ContactSerialiserLibrary.Enums;
using ContactSerialiserLibrary.Models;

namespace Task1Serializator
{
	public class DataSource : IDataSource
	{
		private Random random = new Random();

		//public Contact CreateContact()
		//{
		//	var address = new Address { Country = "Brazil", City = "Whatever", CityAddress = "Something Street, 43", AddressType = TypeOfAddress.Actual };
		//	var contact = new Contact("Лимонов", "Виктор", "Петрович", Gender.Male, new DateTime(1997, 4, 6), "345345345", address, "7943234234");
			
		//	return contact;
		//}

		public IEnumerable<Contact> CreateContacts(int numberOfContacts = 1)
		{
			//Сделал всем одинаковый адрес
			var address = new Address { Country = "Brazil", City = "Whatever", CityAddress = "Something Street, 43", AddressType = TypeOfAddress.Actual };
			var firstNames = new string[] { "Андрей", "Алексей", "Антон", "Дмитрий", "Максим", "Владимир" };
			var secondNames = new string[] { "Серов", "Белов", "Чернов", "Желтов", "Зеленов", "Бирюзовый" };
			var thirdNames = new string[] { "Андреевич", "Алексеевич", "Антонович", "Дмитриевич", "Максимович", "Владимирович" };
			var startDate = DateTime.Today.AddYears(-100);
			var contacts = new List<Contact>();

			for (var numOfElem = 0; numOfElem < numberOfContacts; numOfElem++)
			{
				contacts.Add(new Contact(
					secondNames[random.Next(secondNames.Length - 1)],
					firstNames[random.Next(secondNames.Length - 1)],
					thirdNames[thirdNames.Length - 1], (Gender)random.Next(1),
					startDate.AddDays(random.Next(36000)),
					(Math.Round(random.NextDouble()*1000000000000)).ToString(),
					address,
					(Math.Round(random.NextDouble()*100000000000)).ToString()
					));
			}

			return contacts;

		}
	}
}
