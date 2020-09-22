using log4net;
using OfficeOpenXml;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ContactSerialiserLibrary.Serializers.ExportTypes
{
	public class ExcelExporter : IExporter
	{
		//Здесь должен быть функционал работы с Excel
		//или например возвращать байты

		public void Export(IEnumerable<IContactView> contactView, string fileName, Container container)
		{
			var logger = container.GetInstance<ILog>();
			logger.Info("Получаю перечисление contactView, начинаю экспорт в Excel");
			var ExcelContactView = container.GetInstance<IContactView>();
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			ExcelPackage excel = new ExcelPackage();
			var sheet = excel.Workbook.Worksheets.Add("Лист1");
			var rowNumber = 1;
			var lineNumber = 1;
			//Сортирую сначала по фамилии, потом по имени 
			
			var titles = contactView.First().GetType().GetProperties();

			//Вписываю заголовки таблицы
			foreach (var title in titles)
			{
				if (IsSerializable(title))
				{
					sheet.Cells[1, lineNumber++].Value = title.Name;
				}
			}

			PropertyInfo[] contactsProperties;
			lineNumber = 2;
			foreach (var contact in contactView)
			{
				contactsProperties = contact.GetType().GetProperties();
				//Взять каждый атрибут
				foreach (var property in contactsProperties)
				{
					if (IsSerializable(property))
					{
						sheet.Cells[lineNumber, rowNumber++].Value = property.GetValue(contact);
					}
				}
				lineNumber += 1;
				rowNumber = 1;
			}

			byte[] bytedContacts = excel.GetAsByteArray();

			var saver = container.GetInstance<ISaver>();
			logger.Info("Сохраняю байты в файл Excel");
			saver.Save(this.GetType(),bytedContacts, fileName);

		}

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
