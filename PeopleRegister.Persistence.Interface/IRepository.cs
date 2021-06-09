using PeopleRegister.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeopleRegister.Persistence.Interface
{
	public interface IRepository
	{
		Task AddPersonAsync(Person person);

		Task UpdatePersonAsync(Person person);

		Task DeletePersonAsync(Person person);

		Task<IEnumerable<PersonSnapshot>> GetPersonsAsync();

		event EventHandler<PersonSnapshot> PersonAdded;

		event EventHandler<PersonSnapshot> PersonUpdated;

		event EventHandler<PersonSnapshot> PersonDeleted;
	}
}
