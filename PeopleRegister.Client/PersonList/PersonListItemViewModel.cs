using PeopleRegister.Client.Mvvm;
using PeopleRegister.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleRegister.Client.PersonList
{
	public class PersonListItemViewModel : ViewModelBase
	{
		public PersonListItemViewModel(PersonSnapshot snapshot)
		{
			Person = snapshot.ToPerson();
			UpdateNumber = snapshot.UpdateNumber;
		}

		public Person Person { get; private set; }

		public string FullName
			=> $"{Person.FirstName} {Person.LastName}";

		public int UpdateNumber { get; private set; }

		public void Reload(PersonSnapshot snapshot)
		{
			Person = snapshot.ToPerson();
			UpdateNumber = snapshot.UpdateNumber;
			RaisePropertyChanged(string.Empty);
		}
	}
}
