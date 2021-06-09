using Moq;
using NUnit.Framework;
using PeopleRegister.Client.PersonDetails;
using PeopleRegister.Client.Tests.Helpers;
using PeopleRegister.Model;
using PeopleRegister.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleRegister.Client.Tests
{
	[TestFixture]
	public class PersonDetailsViewModelTests
	{
		[Test]
		public void Submit_command_updates_existing_person_in_repository()
		{
			var person = new Person();

			var repositoryMock = new Mock<IRepository>();

			var sut = new PersonDetailsViewModel(repositoryMock.Object);
			sut.Reload(person);

			sut.SubmitCommand.Execute(null);

			repositoryMock.Verify(r => r.UpdatePersonAsync(person));
		}

		[Test]
		public void Submit_command_is_enabled_on_change()
		{
			var repositoryMock = new Mock<IRepository>();

			var sut = new PersonDetailsViewModel(repositoryMock.Object);
			var isCanExecuteChanged = false;
			sut.SubmitCommand.CanExecuteChanged += (_, __) => isCanExecuteChanged = true;

			Assert.IsFalse(sut.SubmitCommand.CanExecute(null));

			sut.FirstName = "John";

			Assert.IsTrue(isCanExecuteChanged);
			Assert.IsTrue(sut.SubmitCommand.CanExecute(null));
		}

		[Test]
		public void Delete_command_deletes_existing_person_in_repository()
		{
			var person = new Person();

			var repositoryMock = new Mock<IRepository>();

			var sut = new PersonDetailsViewModel(repositoryMock.Object);
			sut.Reload(person);

			sut.DeleteCommand.Execute(null);

			repositoryMock.Verify(r => r.DeletePersonAsync(person));
		}

		[Test]
		public void Delete_command_is_disabled_for_empty_person()
		{
			var repositoryMock = new Mock<IRepository>();

			var sut = new PersonDetailsViewModel(repositoryMock.Object);
			var isCanExecuteChanged = false;
			sut.DeleteCommand.CanExecuteChanged += (_, __) => isCanExecuteChanged = true;

			Assert.IsFalse(sut.DeleteCommand.CanExecute(null));

			sut.Reload(new Person());

			Assert.IsTrue(isCanExecuteChanged);
			Assert.IsTrue(sut.DeleteCommand.CanExecute(null));
		}

		[Test]
		public void Delete_command_resets_person()
		{
			var repositoryMock = new Mock<IRepository>();

			var sut = new PersonDetailsViewModel(repositoryMock.Object);

			var person = new Person();
			sut.Reload(person);

			sut.DeleteCommand.Execute(null);

			Assert.AreNotEqual(person, sut.Person);
			PersonAssert.AreEqual(new Person(), sut.Person);
		}
	}
}