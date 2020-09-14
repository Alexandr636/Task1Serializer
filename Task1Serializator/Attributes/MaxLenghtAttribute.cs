using System;

namespace Task1Serializator.Attributes
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
