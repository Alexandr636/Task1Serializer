using System;

namespace ContactSerialiserLibrary.Attributes
{
	public sealed class MinBirthDateAttribute : Attribute
	{
		public DateTime MinBirthDate
		{
			get; set;
		}

		public MinBirthDateAttribute(DateTime minBirthDate)
		{
			MinBirthDate = minBirthDate;
		}
	}
}
