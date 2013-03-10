using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallDataStore
{
	public interface ISmallStorage
	{
		IEnumerable<T> GetAll<T>()
			where T : ISmallStorageItem;

		T Get<T>(string key)
			where T : ISmallStorageItem;

		void DeleteAll<T>()
			where T : ISmallStorageItem;

		void Delete<T>(string key)
			where T : ISmallStorageItem;

		void Save<T>(T item)
			where T : ISmallStorageItem;

		void SaveAll<T>(IEnumerable<T> all)
			where T : ISmallStorageItem;
	}
}
