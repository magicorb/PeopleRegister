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
using PeopleRegister.Client.Helpers;

namespace PeopleRegister.Client.PersonList
{
	public class PersonListViewModel : ViewModelBase, IDisposable
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

			AddPersonCommand = new DelegateCommand(ExecuteAddPerson);
		}

		public ObservableCollection<PersonListItemViewModel> Persons { get; }

		private PersonListItemViewModel _selectedPerson;

		public PersonListItemViewModel SelectedPerson
		{
			get => _selectedPerson;
			set => SetProperty(ref _selectedPerson, value);
		}

		public DelegateCommand AddPersonCommand { get; }

		public EventHandler AddPersonRequested;

		public async Task InitializeAsync()
		{
			_repository.PersonAdded += Repository_PersonAdded;
			_repository.PersonDeleted += Repository_PersonDeleted;
			_repository.PersonUpdated += Repository_PersonUpdated;

			var persons = await _repository.GetPersonsAsync();

			foreach (var person in persons)
				TryAddPerson(person);
		}

		public void Dispose()
		{
			_repository.PersonAdded -= Repository_PersonAdded;
			_repository.PersonDeleted -= Repository_PersonDeleted;
			_repository.PersonUpdated -= Repository_PersonUpdated;
		}

		private void Repository_PersonAdded(object sender, PersonSnapshot e)
			=> _dispatcher.BeginInvoke(() => TryAddPerson(e));

		private void TryAddPerson(PersonSnapshot snapshot)
		{
			var isStale = Persons.Any(vm
				=> vm.Person.Id == snapshot.Id && vm.UpdateNumber >= snapshot.UpdateNumber);

			if (!isStale)
				Persons.Add(new PersonListItemViewModel(snapshot));
		}

		private void Repository_PersonDeleted(object sender, PersonSnapshot e)
			=> _dispatcher.BeginInvoke(() =>
			{
				var index = Persons.IndexOf(vm => vm.Person.Id == e.Id);

				if (index != -1)
					Persons.RemoveAt(index);
			});

		private void Repository_PersonUpdated(object sender, PersonSnapshot e)
			=> _dispatcher.BeginInvoke(() =>
			{
				var item = Persons.FirstOrDefault(vm => vm.Person.Id == e.Id);

				if (item != null && item.UpdateNumber < e.UpdateNumber)
					item.Reload(e);
			});

		private void ExecuteAddPerson()
		{
			SelectedPerson = null;
			AddPersonRequested?.Invoke(this, EventArgs.Empty);
		}
	}
}