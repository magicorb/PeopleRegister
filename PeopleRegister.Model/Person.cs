using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleRegister.Model
{
	public class Person
	{
		public Guid Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string DateOfBirth { get; set; }

		public string Profession { get; set; }
	}
}
