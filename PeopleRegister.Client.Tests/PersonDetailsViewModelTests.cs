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
			sut.Load(person);

			sut.SubmitCommand.Execute(null);

			repositoryMock.Verify(r => r.UpdatePersonAsync(person));
		}

		[Test]
		public void Submit_command_is_enabled_on_change()
		{
			var sut = new PersonDetailsViewModel(Mock.Of<IRepository>());
			var isCanExecuteChanged = false;
			sut.SubmitCommand.CanExecuteChanged += (_, __) => isCanExecuteChanged = true;

			Assert.IsFalse(sut.SubmitCommand.CanExecute(null));

			sut.FirstName = "John";

			Assert.IsTrue(isCanExecuteChanged);
			Assert.IsTrue(sut.SubmitCommand.CanExecute(null));
		}

		[Test]
		public void Submit_command_updates_state()
		{
			var sut = new PersonDetailsViewModel(Mock.Of<IRepository>());
			sut.FirstName = "John";

			var isCanExecuteChanged = false;
			sut.DeleteCommand.CanExecuteChanged += (_, __) => isCanExecuteChanged = true;

			var areAllPropertiesChanged = false;
			sut.PropertyChanged += (_, args) => areAllPropertiesChanged = args.PropertyName == string.Empty;

			sut.SubmitCommand.Execute(null);

			Assert.IsTrue(isCanExecuteChanged);
			Assert.IsTrue(areAllPropertiesChanged);
			Assert.IsTrue(sut.DeleteCommand.CanExecute(null));
			Assert.IsFalse(sut.IsNew);
			Assert.AreEqual(Localization.SubmitChanges, sut.SubmitButtonText);
		}

		[Test]
		public void Delete_command_deletes_existing_person_in_repository()
		{
			var person = new Person();

			var repositoryMock = new Mock<IRepository>();

			var sut = new PersonDetailsViewModel(repositoryMock.Object);
			sut.Load(person);

			sut.DeleteCommand.Execute(null);

			repositoryMock.Verify(r => r.DeletePersonAsync(person));
		}

		[Test]
		public void Delete_command_resets_person()
		{
			var sut = new PersonDetailsViewModel(Mock.Of<IRepository>());

			var person = new Person();
			sut.Load(person);

			sut.DeleteCommand.Execute(null);

			Assert.AreNotEqual(person, sut.Person);
			PersonAssert.AreEqual(new Person(), sut.Person);
		}

		[Test]
		public void Deafult_state_is_new_person()
		{
			var sut = new PersonDetailsViewModel(Mock.Of<IRepository>());

			Assert.IsFalse(sut.DeleteCommand.CanExecute(null));
			Assert.IsTrue(sut.IsNew);
			PersonAssert.AreEqual(new Person(), sut.Person);
			Assert.AreEqual(Localization.SubmitNew, sut.SubmitButtonText);
		}

		[Test]
		public void Load_updates_state()
		{
			var sut = new PersonDetailsViewModel(Mock.Of<IRepository>());

			var isCanExecuteChanged = false;
			sut.DeleteCommand.CanExecuteChanged += (_, __) => isCanExecuteChanged = true;

			var areAllPropertiesChanged = false;
			sut.PropertyChanged += (_, args) => areAllPropertiesChanged = args.PropertyName == string.Empty;

			Assert.IsFalse(sut.DeleteCommand.CanExecute(null));

			var person = new Person() { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", DateOfBirth = "30.01.1900", Profession = "Programmer" };
			sut.Load(person);

			Assert.IsTrue(isCanExecuteChanged);
			Assert.IsTrue(areAllPropertiesChanged);
			Assert.IsTrue(sut.DeleteCommand.CanExecute(null));
			Assert.IsFalse(sut.IsNew);
			Assert.AreEqual(person, sut.Person);
			Assert.AreEqual(Localization.SubmitChanges, sut.SubmitButtonText);
		}

		[Test]
		public void Reset_updates_state()
		{
			var sut = new PersonDetailsViewModel(Mock.Of<IRepository>());

			var person = new Person() { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", DateOfBirth = "30.01.1900", Profession = "Programmer" };
			sut.Load(person);

			var isCanExecuteChanged = false;
			sut.DeleteCommand.CanExecuteChanged += (_, __) => isCanExecuteChanged = true;

			var areAllPropertiesChanged = false;
			sut.PropertyChanged += (_, args) => areAllPropertiesChanged = args.PropertyName == string.Empty;

			sut.Reset();

			Assert.IsTrue(isCanExecuteChanged);
			Assert.IsTrue(areAllPropertiesChanged);
			Assert.IsFalse(sut.DeleteCommand.CanExecute(null));
			Assert.IsTrue(sut.IsNew);
			PersonAssert.AreEqual(new Person(), sut.Person);
			Assert.AreEqual(Localization.SubmitNew, sut.SubmitButtonText);
		}
	}
}