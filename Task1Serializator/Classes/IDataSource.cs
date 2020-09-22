using ContactSerialiserLibrary.Models;
using System.Collections.Generic;

namespace Task1Serializator
{
	public interface IDataSource
	{
		IEnumerable<Contact> CreateContacts(int numberOfContacts);
	}
}
