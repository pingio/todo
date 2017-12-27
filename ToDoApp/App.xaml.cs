
using System;
using System.Windows;

namespace ToDoApp
{
				/// <summary>
				/// Interaction logic for App.xaml
				/// </summary>
				public partial class App : Application
				{
								private void Application_Startup(object sender, StartupEventArgs e)
								{
												if (AppDomain.CurrentDomain.SetupInformation.ActivationArguments != null
													&& AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null
													&& AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData.Length > 0)
												{

																Uri uri = new Uri(AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData[0]);
																PropertyHandler.Instance.CurrentFilePath = uri.LocalPath;
																PropertyHandler.Instance.CurrentFile = new LoadHandler(PropertyHandler.Instance.CurrentFilePath).LoadFile();
												}
												else
												{
																PropertyHandler.Instance.CurrentFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\new file.todo";
																PropertyHandler.Instance.CurrentFile = new LoadHandler(PropertyHandler.Instance.CurrentFilePath).NewFile();
												}

								}
				}
}
