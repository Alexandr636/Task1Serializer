using System.Collections.Generic;

namespace ContactSerialiserLibrary.Interfaces
{
	public interface IFileWriter
	{
		void WriteInFile(string fileName, byte[] bytedText);
		void WriteListInFile(string fileName, List<byte[]> byteList);
	}
}