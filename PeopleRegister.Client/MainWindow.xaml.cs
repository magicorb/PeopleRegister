using PeopleRegister.Client.Mvvm;
using PeopleRegister.Client.PersonDetails;
using PeopleRegister.Client.PersonList;
using PeopleRegister.Model;
using PeopleRegister.Persistence.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PeopleRegister.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainWindowViewModel _viewModel;

		public MainWindow(MainWindowViewModel viewModel)
		{
			InitializeComponent();

			DataContext = _viewModel = viewModel;
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			await _viewModel.InitializeAsync();
		}

		private void Window_Unloaded(object sender, RoutedEventArgs e)
		{
			_viewModel.Dispose();
			_viewModel.PersonList.Dispose();
		}
	}
}
