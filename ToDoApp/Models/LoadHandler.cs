using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
				class LoadHandler
				{
								private string filePath;
								public LoadHandler(string path) => this.filePath = path;

								public TodoFile LoadFile(string path = null){

												if (String.IsNullOrEmpty(path)) path = filePath;

												string fileContents = File.ReadAllText(path);

												Debug.WriteLine(fileContents);

												TodoFile file = JsonConvert.DeserializeObject<TodoFile>(fileContents);

												return file;
								}

								public TodoFile NewFile(){
												return new TodoFile();
								}
				
				}
}
