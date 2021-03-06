﻿using Newtonsoft.Json;
using System;
using System.IO;

namespace ToDoApp
{
	class SaveHandler
	{
		private TodoFile saveFile;

		public SaveHandler(TodoFile file) => this.saveFile = file;

		public bool Save(string path)
		{

			string jsonSave = JsonConvert.SerializeObject(saveFile);
			try
			{
				File.WriteAllText(path, jsonSave);
				PropertyHandler.Instance.CurrentFilePath = path;
				return true;
			}

			/**
				* Not doing anything to these catches, just returning false as it means the file was not saved.
				* Letting the program crash otherwise.
				*/
			catch (UnauthorizedAccessException)
			{
			}
			catch (DirectoryNotFoundException)
			{
			}
			catch (PathTooLongException)
			{
			}
			return false;
		}

	}
}
