using System;
using ContactSerialiserLibrary.Serializers;
using SimpleInjector;
using System.Configuration;
using ContactSerialiserLibrary.Serializers.ExportTypes;
using log4net;

namespace Task1Serializator
{
    class Program
    {
		static void Main(string[] args)
        {
			Logger.InitLogger();
			Logger.Log.Info("Старт приложения");
		

			//Наполнил контейнер
			var container = FillContainer();
			var dataSource = container.GetInstance<IDataSource>();
			//Получается, что выбирать куда экспортировать придётся в методе, где наполняется container
			
			
			var dataForExport = dataSource.CreateContacts(5);
			//var contactSerializer = container.GetInstance<IContactSerializer>();

			Console.WriteLine("Введите имя файла, для сохранения:");
			var fileName = Console.ReadLine();

			//Получаю данные, представленные определённым образом, уже для экспорта в зависимости от типа Exporter
			var modelBuilder = container.GetInstance<IModelBuilder>();
			var FakeExporter = container.GetInstance<IExporter>();
			var contactView = modelBuilder.BuildContactView(dataForExport, container);

			//Экспортирую данные. Чтобы убрать часть дел из Главного метода -
			//сохранение в файл будет происходить в Exporter с помощью Saver
			//var saver = container.GetRegistration<ISaver>();
			var exporter = container.GetInstance<IExporter>();
			exporter.Export(contactView, fileName, container);


			//contactSerializer.Serialize(exporter, contactView);
			//ModelBuiler
			//IExport
			//var contactsArray = new Contact[] { contact, contact1 };
			//contactSerializer.Serialize(exporter,contactsArray);
			//contactSerializer.Serialize(contact);
			//var a = contactSerializer.Deserialize("Contact.txt");

			Console.WriteLine("Файл создан успешно");
			Logger.Log.Info("Ожидает ввода");
			Console.WriteLine("для выхода нажмите любую букву на клавиатуре");
			Console.ReadKey();
			Logger.Log.Info("Завершилось без ошибок");
		}

		private static Container FillContainer()
		{
			//Получается, что задавать  формат вывода придётся в этом методе, а не из мейна, иначе придётся сначала в мейне всё создавать
			var container = new Container();
			container.Register<ILog>(()=> Logger.Log);
			container.Register<IDataSource, DataSource>();
			container.Register<IExporter, ExcelExporter>();
			container.Register<ISaver, Saver>();
			container.Register<IModelBuilder, ModelBuilder>();
			container.Register<IContactView, ContactView>();

			return container;
		}
	}
}
