using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleRegister.Model
{
	public class PersonSnapshot
	{
		public PersonSnapshot(
			Guid id,
			string firstName,
			string lastName,
			string dateOfBirth,
			string profession,
			int updateNumber)
		{
			Id = id;
			FirstName = firstName;
			LastName = lastName;
			DateOfBirth = dateOfBirth;
			Profession = profession;
			UpdateNumber = updateNumber;
		}

		public Guid Id { get; }

		public string FirstName { get; }

		public string LastName { get; }

		public string DateOfBirth { get; }

		public string Profession { get; }

		public int UpdateNumber { get; }

		public Person ToPerson()
			=> new Person()
			{
				Id = Id,
				FirstName = FirstName,
				LastName = LastName,
				DateOfBirth = DateOfBirth,
				Profession = Profession
			};

		public static PersonSnapshot FromPerson(Person person, int updateNumber)
			=> new PersonSnapshot(
				person.Id,
				person.FirstName,
				person.LastName,
				person.DateOfBirth,
				person.Profession,
				updateNumber
			);
	}
}
