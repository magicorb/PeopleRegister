using NUnit.Framework;
using PeopleRegister.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PeopleRegister.Client.Tests.Helpers
{
	public static class PersonComparisonHelper
	{
		public static bool AreEqual(Person person1, Person person2)
			=> person1.Id == person2.Id
			&& person1.FirstName == person2.FirstName
			&& person1.LastName == person2.LastName
			&& person1.DateOfBirth == person2.DateOfBirth
			&& person1.Profession == person2.Profession;

		public static Func<Person, Person, bool> AreEqualFunc { get; }
			= new Func<Person, Person, bool>(AreEqual);
	}
}
