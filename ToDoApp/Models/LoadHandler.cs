using Newtonsoft.Json;
using System;
using System.IO;

namespace ToDoApp
{
	class LoadHandler
	{
		private string filePath;
		public LoadHandler(string path) => this.filePath = path;

		public TodoFile LoadFile(string path = null)
		{

			if (String.IsNullOrEmpty(path)) path = filePath;
			TodoFile file = null;

			try
			{
				string fileContents = File.ReadAllText(path);

				file = JsonConvert.DeserializeObject<TodoFile>(fileContents);
			}
			catch (FileNotFoundException)
			{
				file = NewFile();
			}


			return file;
		}

		public TodoFile NewFile()
		{
			return new TodoFile();
		}

	}
}
