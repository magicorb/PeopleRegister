using PeopleRegister.Client.Mvvm;
using PeopleRegister.Model;
using PeopleRegister.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeopleRegister.Client.PersonDetails
{
	public class PersonDetailsViewModel : ViewModelBase
	{
		private static readonly Person EmptyPerson = new Person();

		private readonly IRepository _repository;

		public PersonDetailsViewModel(IRepository repository)
		{
			_repository = repository;

			Person = EmptyPerson;

			SubmitCommand = new DelegateCommand(async () => await ExecuteSubmitAsync(), CanSubmit);
			DeleteCommand = new DelegateCommand(async () => await ExecuteDeleteAsync(), CanDelete);
		}

		public Person Person { get; private set; }

		public string FirstName
		{
			get => Person.FirstName;
			set
			{
				Person.FirstName = value;
				RaisePropertyChanged();
				HasChanges = true;
			}
		}

		public string LastName
		{
			get => Person.LastName;
			set
			{
				Person.LastName = value;
				RaisePropertyChanged();
				HasChanges = true;
			}
		}

		public string DateOfBirth
		{
			get => Person.DateOfBirth;
			set
			{
				Person.DateOfBirth = value;
				RaisePropertyChanged();
				HasChanges = true;
			}
		}

		public string Profession
		{
			get => Person.Profession;
			set
			{
				Person.Profession = value;
				RaisePropertyChanged();
				HasChanges = true;
			}
		}

		private bool _isNew;

		public bool IsNew
		{
			get => _isNew;
			set
			{
				SetProperty(ref _isNew, value);
				DeleteCommand.RaiseCanExecuteChanged();
			}
		}

		private bool _hasChanges;

		public bool HasChanges
		{
			get => _hasChanges;
			private set
			{
				SetProperty(ref _hasChanges, value);
				SubmitCommand.RaiseCanExecuteChanged();
			}
		}

		public void Reload(Person person)
		{
			Person = person;

			RaisePropertyChanged(string.Empty);
			DeleteCommand.RaiseCanExecuteChanged();
			HasChanges = false;
		}

		public void Reset()
			=> Reload(EmptyPerson);

		public DelegateCommand SubmitCommand { get; }

		public DelegateCommand DeleteCommand { get; }

		private async Task ExecuteSubmitAsync()
		{
			if (IsNew)
				await _repository.AddPersonAsync(Person);
			else
				await _repository.UpdatePersonAsync(Person);

			HasChanges = false;
		}

		private bool CanSubmit()
			=> HasChanges;

		private async Task ExecuteDeleteAsync()
		{
			await _repository.DeletePersonAsync(Person);

			Person = EmptyPerson;
		}

		private bool CanDelete()
			=> !IsNew && Person != EmptyPerson;
	}
}