

using System;

namespace ContactSerialiserLibrary.Serializers
{
	public interface ISaver
	{
		void Save(Type exporter, byte[] dataForSave, string fileName = "Contact");
	}
}
