using System;
using System.Text.RegularExpressions;
using Task1Serializator.Attributes;
using Task1Serializator.Enums;

namespace Task1Serializator.Models
{
    public sealed class Contact : ICloneable
    {
		[MaxLength(30)]
		[Attributes.Serializable]
		public string SecondName
		{
			get; set;
		}

		[MaxLength(30)]
		[Attributes.Serializable]
		public string FirstName
		{
			get; set;
		}

		[MaxLength(30)]
		[Attributes.Serializable]
		public string ThirdName
		{
			get; set;
		}

		[Attributes.Serializable]
		public Gender Gender
		{
			get; set;
		}

		//[MinBirthDate(new DateTime(2000,05,25))]
		[Attributes.Serializable]
		public DateTime BirthDate
		{
			get; set;
		}

		/// <summary>
		/// ИНН
		/// </summary>
		[Attributes.Serializable]
		public string INN
		{
			get; set;
		}

		[Attributes.Serializable]
		public Address Address
		{
			get; set;
		}

		[PhoneNumberRegex("[0-9]{11}")]
		[Attributes.Serializable]
		public string PhoneNumber
		{
			get; set;
		}

		/// <summary>
		/// Сколько целых лет этому человеку
		/// </summary>
		private int _FullYears;

		[Attributes.Serializable]
		public int FullYears
		{
			get
			{
				_FullYears = CalculateFullYears(BirthDate);
				return _FullYears;
			}
		}
		
        public Contact(string secondName, string firstName, string thirdName,
            Gender gender, DateTime birthDate, string iNN, Address address, string phoneNumber)
        {
            SecondName = secondName;
            FirstName = firstName;
            ThirdName = thirdName;
            Gender = gender;
            BirthDate = birthDate;
            INN = iNN;
			Address = address;
            PhoneNumber = phoneNumber;

            _FullYears = CalculateFullYears(birthDate);
        }

		private Contact() { }

        /// <summary>
        /// Подсчитывает сколько лет человеку исходя из даты рождения и нынешней даты
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns></returns>
        private int CalculateFullYears(DateTime birthDate)
        {
			DateTime todayDate = DateTime.Today;
            int fullYears = todayDate.Year - birthDate.Year;

            if (todayDate.Month < birthDate.Month || (todayDate.Month == birthDate.Month && todayDate.Day < birthDate.Day))
            {
                fullYears--;
            }

            return fullYears;
        }

		public object Clone()
        {
			var address = new Address
			{
				Country = Address.Country,
				City = Address.City,
				CityAddress = Address.CityAddress,
				AddressType = Address.AddressType
			};
			var clonedContact = new Contact
			{
				SecondName = SecondName,
				FirstName = FirstName,
				ThirdName = ThirdName,
				Gender = Gender,
				BirthDate = BirthDate,
				INN = INN,
				Address = address,
				PhoneNumber = PhoneNumber
			};

			return clonedContact;
		}

		public override string ToString()
		{
			return $"Contact {{ SecondName:{SecondName}, FirstName:{FirstName}, ThirdName:{ThirdName},  Gender:{Gender}, BirthDate:{BirthDate}, INN:{INN}, Address {Address.ToString()}, PhoneNumber:{PhoneNumber}, FullYears:{FullYears}  }} ";
		
		}

		public override bool Equals(object obj)
		{
			var contact = obj as Contact;
			if (contact != null )
			{
				if (
					contact.SecondName.Equals(SecondName) &&
					contact.FirstName.Equals(FirstName) &&
					contact.ThirdName.Equals(ThirdName) &&
					contact.Gender.Equals(Gender) &&
					contact.BirthDate.Equals(BirthDate) &&
					contact.INN.Equals(INN) &&
					contact.Address.Equals(Address) &&
					contact.PhoneNumber.Equals(PhoneNumber) &&
					contact.FullYears.Equals(FullYears)
					)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public override int GetHashCode()
		{
			var stringedContact = SecondName + FirstName + ThirdName + Gender.ToString() +
				BirthDate.ToString() + INN + Address.ToString() + PhoneNumber.ToString() + FullYears+ToString();
			
			return stringedContact.GetHashCode();
		}
	}
}

