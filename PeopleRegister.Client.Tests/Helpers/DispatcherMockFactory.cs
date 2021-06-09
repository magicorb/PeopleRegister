using Moq;
using PeopleRegister.Client.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleRegister.Client.Tests.Helpers
{
	public static class DispatcherMockFactory
	{
		public static Mock<IDispatcher> CreateSynchronousDispatcherMock()
		{
			var dispatcherMock = new Mock<IDispatcher>();

			dispatcherMock
				.Setup(x => x.BeginInvoke(It.IsAny<Action>()))
				.Callback(new Action<Action>(action => action()));

			return dispatcherMock;
		}
	}
}
