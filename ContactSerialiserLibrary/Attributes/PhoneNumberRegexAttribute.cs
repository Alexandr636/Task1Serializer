using System.Text.RegularExpressions;

namespace ContactSerialiserLibrary.Attributes
{
	public sealed class PhoneNumberRegexAttribute : System.Attribute
	{
		public Regex PhoneNumberRegex
		{
			get; set;
		}

		public PhoneNumberRegexAttribute(string phoneNumberRegex)
		{
			PhoneNumberRegex = new Regex(phoneNumberRegex);
		}
	}
}
