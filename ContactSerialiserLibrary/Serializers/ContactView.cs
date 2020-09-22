using ContactSerialiserLibrary.Attributes;
using System;
using System.Collections.Generic;


namespace ContactSerialiserLibrary.Serializers
{
	public class ContactView : IContactView
	{
		[Attributes.Serializable]
		public int Counter
		{
			get; set;
		}

		[Attributes.Serializable]
		public string ShortName
		{
			get; set;
		}

		[Attributes.Serializable]
		public string SecondName
		{
			get; set;
		}

		[Attributes.Serializable]
		public string FirstName
		{
			get; set;
		}

		[Attributes.Serializable]
		public string ThirdName
		{
			get; set;
		}

		[Format("test")]
		[Attributes.Serializable]
		public string BirthDate { get; set; }
		//public string SetBirthDate()
		//{
		//	BirthDate.GetType().Get
		//}


		[Attributes.Serializable]
		public string INN
		{
			get; set;
		}

		[Format("")]
		[Attributes.Serializable]
		public string PhoneNumber
		{
			get; set;
		}

		[Attributes.Serializable]
		public string Country
		{
			get; set;
		}

		[Attributes.Serializable]
		public string City
		{
			get; set;
		}

		[Attributes.Serializable]
		public string CityAddress
		{
			get; set;
		}

	}
}
