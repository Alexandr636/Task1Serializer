using ContactSerialiserLibrary.Models;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ContactSerialiserLibrary.Serializers
{
	/// <summary>
	/// Преобразует Данные в модель
	/// </summary>
	public class ModelBuilder : IModelBuilder
	{
		private Container container;

		/// <summary>
		/// Возвращает лист наполненых из Contact данными в всоостветствии с моделью
		/// </summary>
		/// <param name="contacts"></param>
		/// <param name="container"></param>
		/// <returns></returns>
		public IEnumerable<IContactView> BuildContactView(IEnumerable<Contact> contacts, Container container)
		{
			this.container = container;
			var contactProperties = new ContactView().GetType().GetProperties();
			//var viewsList = new List<ContactView>();
			var sortedContactList = (contacts as IEnumerable<Contact>).OrderBy(sort => sort.SecondName).ThenBy(sort => sort.FirstName);
			var contactViewList = new List<ContactView>();
			var counter = 1;
			//var 

			foreach (Contact contact in contacts)
			{
				contactViewList.Add(new ContactView
				{
					Counter = counter++,
					ShortName = contact.SecondName + contact.FirstName.ToCharArray()[0] + "." + contact.ThirdName.ToCharArray()[0] + ".",
					SecondName = contact.SecondName, FirstName = contact.FirstName,
					ThirdName = contact.ThirdName,
					BirthDate = FormatDate(contact.BirthDate, contactProperties),
					INN = contact.INN,
					PhoneNumber = contact.PhoneNumber,
					Country = contact.Address.Country , City = contact.Address.City,
					CityAddress = contact.Address.CityAddress
				});				
			}

			contactViewList = FormatViewList(contactViewList);

			
			return contactViewList;
			
		}

		private string FormatDate(DateTime birthDate, IEnumerable<PropertyInfo> properties)
		{
			foreach (var property in properties)
			{
				if (property.Name.Equals("BirthDate"))
				{
					//foreach(var attribute in property.GetCustomAttributes())
					//{
					//	if (attribute.GetType().Name.Equals("FormatAttribute"))
					//	{
					//		var a = attribute.GetType().GetProperty("FormatRegex");
							
					//		//attribute.GetType().GetFie
					//	}
					//}
				}
			}
			//throw new NotImplementedException();
			return "123";
		}

		private List<ContactView> FormatViewList(List<ContactView> contactsView)
		{
			//foreach(var contact in contactsView)
			//{
			//	foreach (var property in contact.GetType().GetProperties() )
			//	{
			//		foreach(var attribute in property.GetCustomAttributes(false))
			//		{
			//			if (attribute.GetType().Name.Equals("ForamtAttribute"))
			//			{

			//			}
			//		}

			//	}

			//}
			//Брать property, брать его атрибут = formatAttribute 
			//Брать из атрибута нужный формат, брать значение атрибута и форматировать
			return contactsView;
		}

		/// <summary>
		/// Проверяет атрибут и если валидный, то возвращает true
		/// </summary>
		/// <param name="property"></param>
		/// <returns></returns>
		private bool ValidateAttribute(PropertyInfo property)
		{
			var attributes = property.GetCustomAttributes();
			foreach(Attribute attribute in attributes)
			{
				if (!attribute.GetType().Name.Equals("SerializableAttribute"))
				{
					var a = attribute.GetType();
				}
			}
			return true;
		}
		
		private bool IsSerializable(PropertyInfo propertyInfo)
		{
			var customAttributes = propertyInfo.GetCustomAttributes(false);
			foreach (Attribute property in customAttributes)
			{
				if( property.GetType().Name.Equals("SerializableAttribute") )
				{
					return true;
				}
			}
			return false;
		}

	}
}
