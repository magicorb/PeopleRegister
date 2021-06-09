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
		private readonly IRepository _repository;

		public PersonDetailsViewModel(IRepository repository)
		{
			_repository = repository;

			SubmitCommand = new DelegateCommand(async () => await ExecuteSubmitAsync(), CanSubmit);
			DeleteCommand = new DelegateCommand(async () => await ExecuteDeleteAsync(), CanDelete);

			Reset();
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
			private set => SetProperty(ref _isNew, value);
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

		public string SubmitButtonText
			=> IsNew ? "Submit New" : "Submit Changes";

		public void Load(Person person)
		{
			Person = person;
			HasChanges = false;
			IsNew = false;

			RaisePropertyChanged(string.Empty);
			DeleteCommand.RaiseCanExecuteChanged();
		}

		public void Reset()
		{
			Person = new Person();
			HasChanges = false;
			IsNew = true;

			RaisePropertyChanged(string.Empty);
			DeleteCommand.RaiseCanExecuteChanged();
		}

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

			Reset();
		}

		private bool CanDelete()
			=> !IsNew;
	}
}