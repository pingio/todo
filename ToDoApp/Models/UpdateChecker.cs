using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Deployment;
using System.Reflection;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ToDoApp
{
	class UpdateChecker
	{
		public bool HasUpdate()
		{
			var hasUpdate = false;

			using (WebClient client = new WebClient())
			{
				client.Headers["User-Agent"] = "Kaban Update Checker";
				client.Headers["Host"] = "github.com/pingio/todo";

				var url = Properties.Settings.Default.UpdateUrl;

				client.DownloadStringAsync(new Uri(url));

				client.DownloadStringCompleted += (sender, e) =>
			   {
				   var jsonContent = e.Result;

				   JArray arrayItems = JArray.Parse(jsonContent);

				   var latestRelease = arrayItems[0];

				   var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

				   var newVersion = new Version(latestRelease["tag_name"].ToString());

				   var result = currentVersion.CompareTo(newVersion);

				   if (result < 0)
					   hasUpdate = true;
			   };

			}

			return hasUpdate;
		}
	}
}
