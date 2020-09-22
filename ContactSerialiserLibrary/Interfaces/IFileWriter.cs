using System.Collections.Generic;

namespace ContactSerialiserLibrary.Interfaces
{
	public interface ISaver
	{
		void WriteInFile(string fileName, byte[] bytedText);
		void WriteListInFile(string fileName, List<byte[]> byteList);
	}
}