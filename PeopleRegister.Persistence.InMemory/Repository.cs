using PeopleRegister.Model;
using PeopleRegister.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeopleRegister.Persistence.InMemory
{
	public class Repository : IRepository
	{
		private Dictionary<Guid, PersonSnapshot> _persons;

		public async Task AddPersonAsync(Person person)
		{
			person.Id = Guid.NewGuid();

			var snapshot = PersonSnapshot.FromPerson(person);

			lock (this)
			{
				_persons.Add(person.Id, snapshot);
			}

			PersonAdded.Invoke(this, snapshot);
		}

		public async Task UpdatePersonAsync(Person person)
		{
			var snapshot = PersonSnapshot.FromPerson(person);

			lock (this)
			{
				if (!_persons.ContainsKey(person.Id))
					throw new ArgumentException();

				_persons[person.Id] = snapshot;
			}

			PersonUpdated.Invoke(this, snapshot);
		}

		public async Task DeletePersonAsync(Person person)
		{
			var snapshot = PersonSnapshot.FromPerson(person);

			lock (this)
			{
				if (!_persons.Remove(person.Id))
					throw new ArgumentException();
			}
			
			PersonDeleted.Invoke(this, snapshot);
		}

		public event EventHandler<PersonSnapshot> PersonAdded;

		public event EventHandler<PersonSnapshot> PersonUpdated;

		public event EventHandler<PersonSnapshot> PersonDeleted;
	}
}
