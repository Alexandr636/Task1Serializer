using ContactSerialiserLibrary.Interfaces;
using log4net;
using SimpleInjector;
using System;
using System.Collections.Generic;

namespace ContactSerialiserLibrary.Serializers.ExportTypes
{
	public class JSONExporter : IExporter
	{
		private Container container;
		

		public void Export(IEnumerable<IContactView> contactView, string fileName, Container container)
		{
			//this.container = container;
			//container.Register<IExporter, JSONExporter>();
			//container.Register<ISaver, Saver>();
			//var exporter = container.GetInstance<IExporter>();
			//var saver = container.GetInstance<ISaver>();
			var logger = container.GetInstance<ILog>();

			logger.Info("Сохраняю байты в файл");
			//saver.Save(this.GetType(), contactView.GetLo(), fileName);
			throw new NotImplementedException();
		}

		//private IContactView BuildJSONView(IEnumerable contacts)
		//{
		//	IContactView JSONContactView = new ContactView();
		//	var listedContacts = (List<Contact>)contacts;

		//	//Для каждого контакта
		//	foreach (var contact in listedContacts)
		//	{
		//		var contactsProperties = contact.GetType().GetProperties();
		//		strBuilder.Append($@"{{ ""{contact.GetType().Name}"" : ");
		//		//Взять каждый атрибут
		//		foreach(var property in contactsProperties)
		//		{
		//			//Не смог до конца применить
		//			//Attribute.IsDefined(property, Attributes.SerializableAttribute, false);
		//			//Провести валидацию, записать в нужном виде
		//			//Следует проводить валидацию по атрибутам при создании объекта ???
		//			if (IsSerializable(property) /*&& ValidateAttribute(property)*/)
		//			{
		//				strBuilder.Append($@" ""{property.Name}"" : ""{property.GetValue(contact)}"", {'\n'} ");
		//			}

		//		}
		//		strBuilder.Append($"}} ");

		//	}


		//	//Перевожу в байты, то что получилось
		//	byte[] bytedContacts = Encoding.UTF8.GetBytes(strBuilder.ToString());
		//	JSONContactView.SetPreparedContacts(bytedContacts);
		//	//throw new NotImplementedException();

		//	return JSONContactView;
		//}


		///// <summary>
		///// Сохраняет сериализованый класс в файл
		///// </summary>
		///// <param name="person"></param>
		//public void SerializeToJSON(Contact person, string fileName = "Contact.txt")
		//{
		//	if (fileChecker.CheckFileNameForWriteFile(fileName))
		//	{
		//		byte[] qwe = System.Text.Encoding.Default.GetBytes(person.ToString());
		//		fileWriter.WriteInFile(fileName, qwe);
		//	}
		//}

		///// <summary>
		///// Позволяет сериализовать массив Contact[]
		///// </summary>
		///// <param name="persons"></param>
		///// <param name="fileName"></param>
		//public void SerializeToJSON(Contact[] persons, string fileName = "Contact.txt")
		//{
		//	if (fileChecker.CheckFileNameForWriteFile(fileName))
		//	{
		//		var byteList = new List<byte[]>();
		//		foreach (var person in persons)
		//		{
		//			byteList.Add(System.Text.Encoding.Default.GetBytes(person.ToString()));
		//		}
		//		fileWriter.WriteListInFile(fileName, byteList);
		//	}

		//}
	}
}
