using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleRegister.Client.Helpers
{
	public static class CollectionExtensions
	{
		public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
		{
			var i = 0;

			foreach (var item in collection)
			{
				if (predicate(item))
					return i;

				i++;
			}

			return -1;
		}
	}
}
