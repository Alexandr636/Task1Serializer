namespace Task1Serializator.Attributes
{
	public sealed class MaxLengthAttribute : System.Attribute
	{
		public uint MaxLength
		{
			get; set;
		}

		public MaxLengthAttribute(uint maxLength)
		{
			MaxLength = maxLength;
		}
	}
}
