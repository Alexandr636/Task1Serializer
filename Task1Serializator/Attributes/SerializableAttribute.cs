using System;

namespace Task1Serializator.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SerializableAttribute : System.Attribute
	{
	}
}
