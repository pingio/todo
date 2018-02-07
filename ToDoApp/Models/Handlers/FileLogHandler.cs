using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
	class FileLogHandler : ILogger
	{
		public FileLogHandler()
		{
		}

		public async Task LogMessage(string str)
		{
			await Task.Delay(0);
		}
	}
}
