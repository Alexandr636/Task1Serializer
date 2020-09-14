using Task1Serializator.Enums;

namespace Task1Serializator.Models
{
    public sealed class Address
    {
        public string Country
		{
			get; set;
		}

        public string City
		{
			get; set;
		}

        public string CityAddress
		{
			get; set;
		}

        public TypeOfAddress AddressType
		{
			get; set;
		}

		public override bool Equals(object obj)
		{
			var address = obj as Address;
			if (address != null)
			{
				if (
					address.Country.Equals(Country) &&
					address.City.Equals(City) &&
					address.CityAddress.Equals(CityAddress) &&
					address.AddressType.Equals(AddressType)
					)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public override string ToString()
		{
			return $"{{ Country : {Country}, City : {City}, CityAddress : {CityAddress}, AddressType : {AddressType} }} ";
		}

		public override int GetHashCode()
		{
			var stringedAddress = Country + City + CityAddress + AddressType;
			return stringedAddress.GetHashCode();
		}
	}
}