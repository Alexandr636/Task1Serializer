namespace ContactSerialiserLibrary.Interfaces
{
	public interface IFileChecker
	{
		bool CheckFileName(string fileName);
		bool CheckFileNameForWriteFile(string fileName);
	}
}