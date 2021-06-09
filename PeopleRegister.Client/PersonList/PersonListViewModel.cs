using PeopleRegister.Client.Mvvm;
using PeopleRegister.Model;
using PeopleRegister.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PeopleRegister.Client.PersonList
{
	public class PersonListViewModel : ViewModelBase
	{
		private readonly IRepository _repository;
		private readonly IDispatcher _dispatcher;

		public PersonListViewModel(
			IRepository repository,
			IDispatcher dispatcher)
		{
			Persons = new ObservableCollection<PersonListItemViewModel>();
			_repository = repository;
			_dispatcher = dispatcher;
		}

		public ObservableCollection<PersonListItemViewModel> Persons { get; }

		private PersonListItemViewModel _selectedPerson;

		public PersonListItemViewModel SelectedPerson
		{
			get => SelectedPerson;
			set => SetProperty(ref _selectedPerson, value);
		}

		public async Task InitializeAsync()
		{
			_repository.PersonAdded += Repository_PersonAdded;

			var persons = await _repository.GetPersonsAsync();

			foreach (var person in persons)
				Persons.Add(new PersonListItemViewModel(person));
		}

		private void Repository_PersonAdded(object sender, PersonSnapshot e)
			=> _dispatcher.BeginInvoke(() => TryAddPerson(e));

		private void TryAddPerson(PersonSnapshot snapshot)
		{
			if (IsStale(snapshot))
				return;

			Persons.Add(new PersonListItemViewModel(snapshot));
		}

		private bool IsStale(PersonSnapshot snapshot)
			=> Persons.Any(p => p.Person.Id == snapshot.Id && p.UpdateNumber > snapshot.UpdateNumber);
	}
}
