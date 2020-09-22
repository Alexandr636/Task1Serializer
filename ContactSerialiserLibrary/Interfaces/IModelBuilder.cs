using ContactSerialiserLibrary.Models;
using SimpleInjector;
using System.Collections.Generic;

namespace ContactSerialiserLibrary.Serializers
{
	public interface IModelBuilder
	{
		IEnumerable<IContactView> BuildContactView(IEnumerable<Contact> contacts, Container container);
	}
}
