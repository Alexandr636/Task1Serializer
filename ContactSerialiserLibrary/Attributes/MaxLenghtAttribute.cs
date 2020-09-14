using System;

namespace ContactSerialiserLibrary.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class MaxLengthAttribute : Attribute
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
