using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;
using SimpleInjector;

namespace ContactSerialiserLibrary.Serializers.ExportTypes
{
	public sealed class CSVExporter : IExporter
	{
		private Container container;

		public void Export(IEnumerable<IContactView> contactView, string fileName, Container container)
		{

			this.container = container;
			var logger = container.GetInstance<ILog>();
			var titles = contactView.First().GetType().GetProperties();
			var strBuilder = new StringBuilder();

			logger.Info("Получаю перечисление contactView, начинаю экспорт в SCV");

			//Вписываю заголовки таблицы
			foreach (var title in titles)
			{
				if (IsSerializable(title))
				{
					strBuilder.Append($@"{title.Name};");
				}
			}

			//Новая строчка для значений столбцов
			strBuilder.Append($@" {'\n'} ");

			PropertyInfo[] contactsProperties;
			
			foreach (var contact in contactView)
			{
				contactsProperties = contact.GetType().GetProperties();
				//Взять каждый атрибут
				foreach (var property in contactsProperties)
				{
					if (IsSerializable(property))
					{
						strBuilder.Append($"{property.GetValue(contact)};");
					}
				}
				strBuilder.Append('\n');
			}

			byte[] bytedContacts = Encoding.Default.GetBytes(strBuilder.ToString());


			var saver = container.GetInstance<ISaver>();

			logger.Info("Сохраняю байты в файл CSV");
			saver.Save(this.GetType(), bytedContacts, fileName);
			
		}


		//private IContactView BuildCSVView(IEnumerable contacts)
		//{
		//	IContactView CSVContactView = new ContactView();
		//	//var listedContacts = /*(List<Contact>)*/contacts;
		//	var counter = 0;
		//	var titles = new object[] { "counter", "shortName", "secondName", "firstName", "thirdName",
		//		"birthDate", "INN", "phoneNumber", "contry", "city", "cityAddress" };

		//	

		//	//Новая строчка для значений столбцов
		//	strBuilder.Append($@" {'\n'} ");

		//	//Для каждого контакта
		//	foreach (Contact contact in contacts)
		//	{
		//		strBuilder.Append($"{counter++};");
		//		strBuilder.Append($"{contact.SecondName} {contact.FirstName.ToCharArray()[0]}.{contact.ThirdName.ToCharArray()[0]}.;");
		//		strBuilder.Append(contact.SecondName + ";");
		//		strBuilder.Append(contact.FirstName + ";");
		//		strBuilder.Append(contact.ThirdName + ";");
		//		strBuilder.Append(contact.BirthDate + ";");
		//		strBuilder.Append(contact.INN + ";");
		//		strBuilder.Append(contact.PhoneNumber + ";");
		//		strBuilder.Append(contact.Address.Country + ";");
		//		strBuilder.Append(contact.Address.City + ";");
		//		strBuilder.Append(contact.Address.CityAddress + ";");

		//		strBuilder.Append($"{'\n'}");

		//	}

		//	byte[] bytedContacts = Encoding.Default.GetBytes(strBuilder.ToString());
		//	CSVContactView.SetPreparedContacts(bytedContacts);
		//	//throw new NotImplementedException();

		//	return CSVContactView;

		private bool IsSerializable(PropertyInfo propertyInfo)
		{
			var customAttributes = propertyInfo.GetCustomAttributes(false);
			foreach (Attribute property in customAttributes)
			{
				if (property.GetType().Name.Equals("SerializableAttribute"))
				{
					return true;
				}
			}
			return false;
		}

	}
}
