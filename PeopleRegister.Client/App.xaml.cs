using Autofac;
using PeopleRegister.Client.Mvvm;
using PeopleRegister.Client.PersonDetails;
using PeopleRegister.Client.PersonList;
using PeopleRegister.Model;
using PeopleRegister.Persistence.InMemory;
using PeopleRegister.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PeopleRegister.Client
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private async void Application_Startup(object sender, StartupEventArgs e)
		{
			var container = CreateContainer();

			await AddFakeData(container.Resolve<IRepository>());

			container.Resolve<MainWindow>().Show();
		}

		private IContainer CreateContainer()
		{
			var builder = new ContainerBuilder();

			builder.RegisterInstance(new Repository()).As<IRepository>();
			builder.RegisterInstance(new UiDispatcher()).As<IDispatcher>();
			builder.RegisterType<MainWindow>().AsSelf();
			builder.RegisterType<MainWindowViewModel>().AsSelf();
			builder.RegisterType<PersonDetailsViewModel>().AsSelf();
			builder.RegisterType<PersonListViewModel>().AsSelf();

			return builder.Build();
		}

		private async Task AddFakeData(IRepository repository)
		{
			await repository.AddPersonAsync(new Person() { FirstName = "John", LastName = "Doe", DateOfBirth = "30.01.1900", Profession = "Programmer" });
			await repository.AddPersonAsync(new Person() { FirstName = "Mary", LastName = "Smith", DateOfBirth = "28.02.1901", Profession = "Analyst" });
		}
	}
}
