
using System;
using System.Windows;

namespace ToDoApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public void Application_Startup(object sender, StartupEventArgs e)
		{
			if (e.Args.Length > 0)
			{
				PropertyHandler.Instance.CurrentFile = new LoadHandler(e.Args[0]).LoadFile();
				PropertyHandler.Instance.CurrentFilePath = e.Args[0];
			}

			else
			{
				PropertyHandler.Instance.CurrentFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\newfile.todo";
				PropertyHandler.Instance.CurrentFile = new LoadHandler(PropertyHandler.Instance.CurrentFilePath).NewFile();

			}

		}

	}
}
