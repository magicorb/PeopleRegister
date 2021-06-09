using PeopleRegister.Client.Mvvm;
using PeopleRegister.Client.PersonDetails;
using PeopleRegister.Client.PersonList;
using PeopleRegister.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace PeopleRegister.Client
{
	public class MainWindowViewModel : ViewModelBase, IDisposable
	{
		public MainWindowViewModel(
			PersonListViewModel personList,
			PersonDetailsViewModel personDetails)
		{
			PersonList = personList;
			PersonDetails = personDetails;

			PersonList.PropertyChanged += PersonList_PropertyChanged;
			PersonList.AddPersonRequested += PersonList_AddPersonRequested;
		}

		public PersonListViewModel PersonList { get; }

		public PersonDetailsViewModel PersonDetails { get; }

		public async Task InitializeAsync()
		{
			await PersonList.InitializeAsync();
		}

		private void PersonList_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(PersonList.SelectedPerson))
			{
				if (PersonList.SelectedPerson != null)
					PersonDetails.Load(PersonList.SelectedPerson.Person.Clone());
				else if (!PersonDetails.IsNew)
					PersonDetails.Reset();
			}
		}

		private void PersonList_AddPersonRequested(object sender, EventArgs e)
		{
			PersonDetails.Reset();
			PersonList.SelectedPerson = null;
		}

		public void Dispose()
		{
			PersonList.PropertyChanged -= PersonList_PropertyChanged;
			PersonList.AddPersonRequested -= PersonList_AddPersonRequested;
		}
	}
}