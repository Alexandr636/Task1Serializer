using ContactSerialiserLibrary.Models;

namespace ContactSerialiserLibrary.Interfaces
{
	public interface IExcelExporter
	{
		void SerializeToExcel(Contact person, string fileName = "Contact.xlsx");
	}
}