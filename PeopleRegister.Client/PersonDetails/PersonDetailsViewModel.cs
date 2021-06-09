using PeopleRegister.Client.Mvvm;
using PeopleRegister.Model;
using PeopleRegister.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleRegister.Client.PersonDetails
{
	public class PersonDetailsViewModel : ViewModelBase
	{
		private readonly IRepository _repository;

		public PersonDetailsViewModel(IRepository repository)
		{
			_repository = repository;

			Person = new Person();
		}

		public Person Person { get; private set; }

		public string FirstName
		{
			get => Person.FirstName;
			set
			{
				Person.FirstName = value;
				RaisePropertyChanged();
			}
		}

		public string LastName
		{
			get => Person.LastName;
			set
			{
				Person.LastName = value;
				RaisePropertyChanged();
			}
		}

		public string DateOfBirth
		{
			get => Person.DateOfBirth;
			set
			{
				Person.DateOfBirth = value;
				RaisePropertyChanged();
			}
		}

		public string Profession
		{
			get => Person.Profession;
			set
			{
				Person.Profession = value;
				RaisePropertyChanged();
			}
		}

		public void Reload(Person person)
		{
			Person = person;
			RaisePropertyChanged(string.Empty);
		}
	}
}
