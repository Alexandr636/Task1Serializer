using System;
using Task1Serializator.Models;
using Task1Serializator.Enums;

namespace Task1Serializator
{
    class Program
    {
        static void Main(string[] args)
        {
			Address address = new Address { Country = "Brazil", City = "Whatever", CityAddress = "Something Street, 43",  AddressType = TypeOfAddress.Actual };
			Address address1 = new Address { Country = "Brazil", City = "Whatever", CityAddress = "Something Street, 43", AddressType = TypeOfAddress.Actual };
			Contact contact = new Contact("Лимонов", "Виктор", "Петрович" , Gender.Male, new DateTime(1997, 4, 6), "345345345", address, "7943234234" );
			Contact contact1 = new Contact("Лимонов", "Виктор", "Петрович", Gender.Male, new DateTime(1997, 4, 6), "345345345", address1, "7943234234");

			Console.WriteLine(contact.ToString());
			Console.WriteLine(contact.Equals(contact1));
			Console.WriteLine(contact.GetHashCode());
			Console.WriteLine(contact1.GetHashCode());

			//var contactSerializer = new ContactSerializer(@"C:\Users\Admin\Desktop\Новый.txt");
			var contactSerializer = new ContactSerializer();
			contactSerializer.Serialize(contact);

			Console.ReadKey();
		}
    }
}
