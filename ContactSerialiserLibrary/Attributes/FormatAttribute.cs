using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactSerialiserLibrary.Attributes
{
	public sealed class FormatAttribute : ValidationAttribute
	{
		public Regex FormatRegex
		{
			get; set;
		}

		public FormatAttribute(string format)
		{
			FormatRegex = new Regex(format);
		}
	}
}
