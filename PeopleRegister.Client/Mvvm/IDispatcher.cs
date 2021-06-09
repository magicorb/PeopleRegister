using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleRegister.Client.Mvvm
{
	public interface IDispatcher
	{
		void BeginInvoke(Action action);
	}
}
