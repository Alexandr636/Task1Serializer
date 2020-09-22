using SimpleInjector;
using System.Collections.Generic;

namespace ContactSerialiserLibrary.Serializers
{
	/// <summary>
	/// Описывает экспорт в любой из выбраных типов
	/// </summary>
	public interface IExporter
	{
		void Export(IEnumerable<IContactView> contactView,string fileName, Container container);
	}
}