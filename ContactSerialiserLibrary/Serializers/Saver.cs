using ContactSerialiserLibrary.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace ContactSerialiserLibrary.Serializers
{
	public class Saver : ISaver
	{
		private ILog logger;
		private string endOfEncodingType = "";

		public Saver(ILog logger)
		{
			this.logger = logger;
			
		}



		/// <summary>
		/// Нужно передать тот тип Exporter в который тип нужно экспортит файл(JSON, Excel, ...)
		/// </summary>
		/// <param name="exporter">тип используемого экспортера</param>
		/// <param name="dataForSave"></param>
		public void Save(Type exporter, byte[] dataForSave, string fileName)
		{
			
			

			switch (exporter.Name)
			{
				case "JSONExporter":
					endOfEncodingType = ".json";
					break;
				case "ExcelExporter" :
					endOfEncodingType = ".xlsx";
					break;
				case "CSVExporter":
					endOfEncodingType = ".csv";
					break;
				default:
					endOfEncodingType = ".txt";
					break;
			}
			WriteInFile($"{fileName}", dataForSave);
		}


		public async void WriteInFile(string fileName, byte[] bytedText)
		{
			var appSettings = ConfigurationManager.AppSettings;

			//var a = appSettings.Get("Rewrite").ToString();
			if (appSettings.Get("Rewrite").ToString().Equals("0"))
			{
				fileName += endOfEncodingType;
				try
				{
					using (var fStream = new FileStream(fileName, FileMode.CreateNew))
					{
						await fStream.WriteAsync(bytedText, 0, bytedText.Length);
					}
				}
				catch (Exception e)
				{
					logger.Error(e.StackTrace + "; " + e.Message);
					fileName += fileName;
					WriteInFile(fileName, bytedText);
					Console.WriteLine("Файл с таким именем существует.Файл будет сохранён.");
				}

			}
			else
			{
				fileName += endOfEncodingType;
				try
				{
					using (var fStream = new FileStream(fileName, FileMode.OpenOrCreate))
					{
						await fStream.WriteAsync(bytedText, 0, bytedText.Length);
					}
				}
				catch (Exception e)
				{
					logger.Error(e.StackTrace + "; " + e.Message);
					fileName += fileName;
					WriteInFile(fileName, bytedText);
					Console.WriteLine("Файл с таким именем существует.Файл будет сохранён.");
				}

			}

		}

		public async void WriteListInFile(string fileName, List<byte[]> byteList)
		{
			using (var fStream = new FileStream(fileName, FileMode.CreateNew))
			{
				foreach (var byteArray in byteList)
					await fStream.WriteAsync(byteArray, 0, byteArray.Length);
			}
		}
	}
}
