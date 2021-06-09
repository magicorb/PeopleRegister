using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace PeopleRegister.Client.Mvvm
{
	public class UiDispatcher : IDispatcher
	{
		public void BeginInvoke(Action action)
			=> Application.Current.Dispatcher.BeginInvoke(action);
	}
}
