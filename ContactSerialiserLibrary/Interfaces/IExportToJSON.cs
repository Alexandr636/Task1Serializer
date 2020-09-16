using ContactSerialiserLibrary.Models;

namespace ContactSerialiserLibrary.Interfaces
{
	public interface IExportToJSON
	{
		void SerializeToJSON(Contact person, string fileName = "Contact.txt");
		void SerializeToJSON(Contact[] persons, string fileName = "Contact.txt");
	}
}