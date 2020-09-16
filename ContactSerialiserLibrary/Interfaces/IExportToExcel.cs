using ContactSerialiserLibrary.Models;

namespace ContactSerialiserLibrary.Interfaces
{
	public interface IExportToExcel
	{
		void SerializeToExcel(Contact person, string fileName = "Contact.xlsx");
	}
}