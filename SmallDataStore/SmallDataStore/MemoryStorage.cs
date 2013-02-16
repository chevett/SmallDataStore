using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallDataStore
{
	public class MemoryStorage: SimpleStorageBase
	{
		private readonly Dictionary<Type, List<ISmallStorageItem>> _dictionary = new Dictionary<Type,List<ISmallStorageItem>>();

		public override IEnumerable<T> GetAll<T>() 
		{
			List<ISmallStorageItem> list;

			if (_dictionary.TryGetValue(typeof(T), out list))
				return list.Cast<T>();

			return Enumerable.Empty<T>();
		}

		protected override void SaveAll<T>(List<T> list)
		{
			_dictionary[typeof(T)] = list
				.Cast<ISmallStorageItem>()
				.ToList();
		}
	}
}
