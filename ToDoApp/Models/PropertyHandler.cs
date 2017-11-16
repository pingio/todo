using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
				class PropertyHandler
				{
								private static PropertyHandler instance;
								public PropertyHandler() => instance = this;

								public static PropertyHandler Instance => instance ?? new PropertyHandler();

								public TodoFile CurrentFile { get; set; }

								public string CurrentFilePath { get; set; }
				}
}
