using NUnit.Framework;
using PeopleRegister.Client.PersonDetails;
using PeopleRegister.Client.PersonList;
using PeopleRegister.Client.Tests.Helpers;
using PeopleRegister.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeopleRegister.Client.Tests
{
	[TestFixture]
	public class MainWindowViewModelTests
	{
		[Test]
		public async Task Selecting_person_in_the_list_reloads_details()
		{
			var persons = new PersonSnapshot[]
			{
				new PersonSnapshot(Guid.NewGuid(), "John", "Doe", "30.01.1900", "Programmer", 1),
				new PersonSnapshot(Guid.NewGuid(), "Mary", "Smith", "28.02.1901", "Analyst", 2)
			};

			var repositoryMock = RepositoryMockFactory.CreateRepositoryMock(persons);
			var dispatcherMock = DispatcherMockFactory.CreateSynchronousDispatcherMock();

			var listViewModel = new PersonListViewModel(repositoryMock.Object, dispatcherMock.Object);
			var detailsViewModel = new PersonDetailsViewModel(repositoryMock.Object);

			var sut = new MainWindowViewModel(listViewModel, detailsViewModel);
			await sut.InitializeAsync();

			PersonAssert.AreEqual(new Person(), detailsViewModel.Person);

			listViewModel.SelectedPerson = listViewModel.Persons[1];

			PersonAssert.AreEqual(listViewModel.Persons[1].Person, detailsViewModel.Person);
		}
	}
}
