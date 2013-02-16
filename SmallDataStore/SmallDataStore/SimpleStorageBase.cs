﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallDataStore
{
	public abstract class SimpleStorageBase : ISmallStorage
	{
		public abstract IEnumerable<T> GetAll<T>() where T : ISmallStorageItem;

		protected abstract void SaveAll<T>(List<T> list);

		public T Get<T>(string key) where T : ISmallStorageItem
		{
			return GetAll<T>().SingleOrDefault(i => i.GetKey() == key);
		}

		public void Save<T>(T item) where T : ISmallStorageItem
		{
			var allItems = GetAll<T>().ToList();
			var key = item.GetKey();
			var found = false;

			for (var i = 0; i < allItems.Count(); i++)
			{
				var currentItem = allItems[i];

				if (currentItem.GetKey() != key)
					continue;

				allItems[i] = item;
				found = true;
				break;
			}

			if (!found)
				allItems.Add(item);

			SaveAll(allItems);
		}
	}
}
