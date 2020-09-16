using ContactSerialiserLibrary.Interfaces;
using ContactSerialiserLibrary.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;


namespace ContactSerialiserLibrary.Serializers.ExportTypes
{
	public class ExportToExcel : IExportToExcel
	{
		public void SerializeToExcel(Contact person, string fileName = "Contact.xlsx")
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			ExcelPackage excel = new ExcelPackage();
			var sheet = excel.Workbook.Worksheets.Add("Лист1");

			sheet.TabColor = System.Drawing.Color.Blue;
			sheet.DefaultRowHeight = 12;
			sheet.Row(1).Height = 20;
			sheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
			sheet.Row(1).Style.Font.Bold = true;
			//sheet.Cells[1, 1].Value = person.GetType().GetCustomAttributes(true)[0].ToString();
			sheet.Cells[1, 1].Value = "id";
			sheet.Cells[1, 2].Value = "ShortName";
			sheet.Cells[1, 3].Value = "SecondName";
			sheet.Cells[1, 4].Value = "FirsName";
			sheet.Cells[1, 5].Value = "ThirdName";
			sheet.Cells[1, 6].Value = "BirthDate";
			FileStream fileStream = File.Create(fileName);
			fileStream.Close();

			File.WriteAllBytes(fileName, excel.GetAsByteArray());
		}
	}
}
