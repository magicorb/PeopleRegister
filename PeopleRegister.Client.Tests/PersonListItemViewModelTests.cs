using Moq;
using NUnit.Framework;
using PeopleRegister.Client.Mvvm;
using PeopleRegister.Client.PersonList;
using PeopleRegister.Client.Tests.Helpers;
using PeopleRegister.Model;
using PeopleRegister.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleRegister.Client.Tests
{
	[TestFixture]
	public class PersonListViewModelTests
	{
		[Test]
		public async Task Initialize_populates_items_from_repository()
		{
			var persons = new PersonSnapshot[]
			{
				new PersonSnapshot(Guid.NewGuid(), "John", "Doe", "30.01.1900", "Programmer", 1),
				new PersonSnapshot(Guid.NewGuid(), "Mary", "Smith", "28.02.1901", "Analyst", 2)
			};

			var repositoryMock = RepositoryMockFactory.CreateRepositoryMock(persons);
			var dispatcherMock = DispatcherMockFactory.CreateSynchronousDispatcherMock();

			var sut = new PersonListViewModel(repositoryMock.Object, dispatcherMock.Object);

			await sut.InitializeAsync();

			var expectedPersons = persons.Select(s => s.ToPerson());
			var populatedPersons = sut.Persons.Select(vm => vm.Person);

			PersonAssert.AreEquivalentLists(expectedPersons, populatedPersons);
		}

		[Test]
		public async Task Items_are_updated_when_person_is_added_to_repository()
		{
			var person1 = new PersonSnapshot(Guid.NewGuid(), "John", "Doe", "30.01.1900", "Programmer", 1);

			var repositoryMock = RepositoryMockFactory.CreateRepositoryMock(person1);
			var dispatcherMock = DispatcherMockFactory.CreateSynchronousDispatcherMock();

			var sut = new PersonListViewModel(repositoryMock.Object, dispatcherMock.Object);

			await sut.InitializeAsync();

			var person2 = new PersonSnapshot(Guid.NewGuid(), "Mary", "Smith", "28.02.1901", "Analyst", 2);

			repositoryMock.Raise(r => r.PersonAdded += null, repositoryMock.Object, person2);

			var expectedPersons = new[] { person1.ToPerson(), person2.ToPerson() };
			var populatedPersons = sut.Persons.Select(vm => vm.Person);

			PersonAssert.AreEquivalentLists(expectedPersons, populatedPersons);

			dispatcherMock.Verify(d => d.BeginInvoke(It.IsAny<Action>()), Times.Once());
		}

		[Test]
		public async Task Stale_person_added_events_are_ignored()
		{
			var person = new PersonSnapshot(Guid.NewGuid(), "John", "Doe", "30.01.1900", "Programmer", 1);

			var repositoryMock = RepositoryMockFactory.CreateRepositoryMock(person);
			var dispatcherMock = DispatcherMockFactory.CreateSynchronousDispatcherMock();

			var sut = new PersonListViewModel(repositoryMock.Object, dispatcherMock.Object);

			await sut.InitializeAsync();

			repositoryMock.Raise(r => r.PersonAdded += null, repositoryMock.Object, person);

			var expectedPersons = new[] { person.ToPerson() };
			var populatedPersons = sut.Persons.Select(vm => vm.Person);

			PersonAssert.AreEquivalentLists(expectedPersons, populatedPersons);
		}
	}
}
