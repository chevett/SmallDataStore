using System;
using System.Collections.Generic;
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
			return System.IO.Path.Combine(GetDirectoryName<T>(), string.Format("{0}.txt", typeof(T).Name));
		}

		private string GetDirectoryName<T>()
		{
			return System.IO.Path.Combine(_filePath, _applicationName);
		}

		public override IEnumerable<T> GetAll<T>()
		{
			var fileName = GetFileName<T>();
			if (!System.IO.File.Exists(fileName))
				return Enumerable.Empty<T>();

			var str = System.IO.File.ReadAllText(fileName);
			var jsonConvertor = new JavaScriptSerializer();

			return jsonConvertor.Deserialize<List<T>>(str);
		}

		protected override void SaveAll<T>(List<T> list)
		{
			var jsonConvertor = new JavaScriptSerializer();
			var str = jsonConvertor.Serialize(list);

			if (!System.IO.Directory.Exists(GetDirectoryName<T>()))
				System.IO.Directory.CreateDirectory(GetDirectoryName<T>());

			System.IO.File.WriteAllText(GetFileName<T>(), str);
		}
	}
}
