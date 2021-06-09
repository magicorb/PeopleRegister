using PeopleRegister.Client.Mvvm;
using PeopleRegister.Client.PersonDetails;
using PeopleRegister.Client.PersonList;
using PeopleRegister.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeopleRegister.Client
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel(
			PersonListViewModel personList,
			PersonDetailsViewModel personDetails)
		{
			PersonList = personList;
			PersonDetails = personDetails;
		}

		public PersonListViewModel PersonList { get; }

		public PersonDetailsViewModel PersonDetails { get; }

		public async Task InitializeAsync()
		{
			await PersonList.InitializeAsync();
		}
	}
}
