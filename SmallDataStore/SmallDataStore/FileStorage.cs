using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SmallDataStore
{
	public class FileStorage : SimpleStorageBase
	{
		private readonly string _filePath = @"C:\Users\Public";
		private readonly string _applicationName;

		public FileStorage(string applicationName, string filePath)
			: this(applicationName)
		{
			_filePath = filePath;
		}

		public FileStorage(string applicationName)
		{
			_applicationName = applicationName;
		}

		private string GetFileName<T>()
		{
			return Path.Combine(GetDirectoryName<T>(), string.Format("{0}.txt", typeof(T).Name));
		}

		private string GetDirectoryName<T>()
		{
			return Path.Combine(_filePath, _applicationName);
		}

		public override void DeleteAll<T>()
		{
			File.Delete(GetFileName<T>());
		}

		public override IEnumerable<T> GetAll<T>()
		{
			var fileName = GetFileName<T>();
			if (!File.Exists(fileName))
				return Enumerable.Empty<T>();

			var str = File.ReadAllText(fileName);
			var jsonConvertor = new JavaScriptSerializer();

			return jsonConvertor.Deserialize<List<T>>(str);
		}

		public override void SaveAll<T>(IEnumerable<T> list)
		{
			var jsonConvertor = new JavaScriptSerializer();
			var str = jsonConvertor.Serialize(list);

			if (!Directory.Exists(GetDirectoryName<T>()))
				Directory.CreateDirectory(GetDirectoryName<T>());

			File.WriteAllText(GetFileName<T>(), str);
		}
	}
}
