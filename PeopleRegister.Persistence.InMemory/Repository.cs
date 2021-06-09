using PeopleRegister.Model;
using PeopleRegister.Persistence.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleRegister.Persistence.InMemory
{
	public class Repository : IRepository
	{
		private Dictionary<Guid, PersonSnapshot> _persons = new Dictionary<Guid, PersonSnapshot>();
		private int _lastUpdateNumber;

		public async Task AddPersonAsync(Person person)
		{
			person.Id = Guid.NewGuid();

			PersonSnapshot snapshot;

			lock (this)
			{
				snapshot = PersonSnapshot.FromPerson(person, _lastUpdateNumber++);
				_persons.Add(person.Id, snapshot);
			}

			PersonAdded?.Invoke(this, snapshot);
		}

		public async Task UpdatePersonAsync(Person person)
		{
			PersonSnapshot snapshot;

			lock (this)
			{
				if (!_persons.ContainsKey(person.Id))
					throw new ArgumentException();

				snapshot = PersonSnapshot.FromPerson(person, _lastUpdateNumber++);

				_persons[person.Id] = snapshot;
			}

			PersonUpdated?.Invoke(this, snapshot);
		}

		public async Task DeletePersonAsync(Person person)
		{
			PersonSnapshot snapshot;

			lock (this)
			{
				if (!_persons.Remove(person.Id))
					throw new ArgumentException();

				snapshot = PersonSnapshot.FromPerson(person, _lastUpdateNumber++);
			}

			PersonDeleted?.Invoke(this, snapshot);
		}

		public async Task<IEnumerable<PersonSnapshot>> GetPersonsAsync()
		{
			lock (this)
			{
				return _persons.Values;
			}
		}

		public event EventHandler<PersonSnapshot> PersonAdded;

		public event EventHandler<PersonSnapshot> PersonUpdated;

		public event EventHandler<PersonSnapshot> PersonDeleted;
	}
}
