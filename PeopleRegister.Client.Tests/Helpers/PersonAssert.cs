using NUnit.Framework;
using PeopleRegister.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PeopleRegister.Client.Tests.Helpers
{
	public static class PersonAssert
	{
		public static void AreEqual(Person person1, Person person2)
			=> Assert.That(person1, Is.EqualTo(person2).Using<Person>(PersonComparisonHelper.AreEqualFunc));

		public static void AreEquivalentLists(IEnumerable<Person> list1, IEnumerable<Person> list2)
			=> Assert.That(list1, Is.EquivalentTo(list2).Using(PersonComparisonHelper.AreEqualFunc));
	}
}
