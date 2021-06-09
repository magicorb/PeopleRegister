using Moq;
using PeopleRegister.Model;
using PeopleRegister.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleRegister.Client.Tests.Helpers
{
	public static class RepositoryMockFactory
	{
		public static Mock<IRepository> CreateRepositoryMock(params PersonSnapshot[] data)
		{
			var repositoryMock = new Mock<IRepository>();

			repositoryMock
				.Setup(r => r.GetPersonsAsync())
				.Returns(Task.FromResult(data.AsEnumerable()));

			return repositoryMock;
		}
	}
}
