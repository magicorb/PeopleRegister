using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleRegister.Model
{
	public class PersonSnapshot
	{
		public PersonSnapshot(Guid id, string firstName, string lastName, string dateOfBirth, string profession)
		{
			Id = id;
			FirstName = firstName;
			LastName = lastName;
			DateOfBirth = dateOfBirth;
			Profession = profession;
		}

		public Guid Id { get; }

		public string FirstName { get; }

		public string LastName { get; }

		public string DateOfBirth { get; }

		public string Profession { get; }

		public Person ToPerson()
			=> new Person()
			{
				Id = Id,
				FirstName = FirstName,
				LastName = LastName,
				DateOfBirth = DateOfBirth,
				Profession = Profession
			};

		public static PersonSnapshot FromPerson(Person person)
			=> new PersonSnapshot(
				person.Id,
				person.FirstName,
				person.LastName,
				person.DateOfBirth,
				person.Profession
			);
	}
}
