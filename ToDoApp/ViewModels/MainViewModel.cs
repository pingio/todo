using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ToDoApp
{
	class MainViewModel : BaseViewModel
	{

		#region Constructor

		private bool canExecute;
		private TodoFile file;
		private string inProgress;
		private string planned;
		private string ideas;


		public MainViewModel()
		{
			
			canExecute = true;
			file = PropertyHandler.Instance.CurrentFile;

			this.inProgress = file.InProgress;
			this.planned = file.Planned;
			this.ideas = file.Ideas;

		}

		#endregion

		#region Buttons
		#region OpenButton


		private ICommand openButton;
		public ICommand OpenButton
		{
			get
			{
				return openButton ?? (openButton = new RelayCommand(() => OpenButtonCommand(), canExecute));
			}
		}

		public String OpenButtonText { get; set; } = "Open...";

		private void OpenButtonCommand()
		{
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Filter = "ToDo files (*.todo)|*.todo|All files (*.*)|*.*";

			if (fileDialog.ShowDialog() == true)
			{
				UpdateContent(fileDialog.FileName);
			}

		}
		#endregion

		#region Save Button
		private ICommand saveButton;
		public ICommand SaveButton
		{
			get
			{
				return saveButton ?? (saveButton = new RelayCommand(() => SaveButtonCommand(), canExecute));
			}
		}

		public String SaveButtonText { get; set; } = "Save";

		private void SaveButtonCommand()
		{
			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.Filter = "ToDo files (*.todo)|*.todo|All files (*.*)|*.*";

			fileDialog.FileName = "My new todo file";
			fileDialog.AddExtension = true;
			fileDialog.DefaultExt = ".todo";


			if (fileDialog.ShowDialog() == true)
			{
				SaveText();
				bool isSaved = new SaveHandler(PropertyHandler.Instance.CurrentFile).Save(fileDialog.FileName);

			}
		}
		#endregion

		#region Settings Button
		private ICommand settingsButton;
		public ICommand SettingsButton
		{
			get
			{
				return settingsButton ?? (settingsButton = new RelayCommand(() => SettingsButtonCommand(), canExecute));
			}
		}

		public String SettingsButtonText { get; set; } = "Settings";

		private void SettingsButtonCommand()
		{
			SaveText();
			WindowViewModel.Instance.CurrentPage = AppPage.Settings;
		}
		#endregion

		#endregion

		#region Text
		public int FontSize => Properties.Settings.Default.FontSize;

		public String PathText { get; set; } = PropertyHandler.Instance.CurrentFilePath;

		public String InProgressText
		{
			get
			{
				return this.inProgress;
			}

			set
			{
				this.inProgress = value;
			}
		}

		public String PlannedText
		{
			get
			{
				return this.planned;
			}

			set
			{
				this.planned = value;
			}
		}

		public String IdeasText
		{
			get
			{
				return this.ideas;
			}

			set
			{
				this.ideas = value;
			}
		}

		#endregion

		#region Helpers

		private void UpdateContent(string path)
		{
			PropertyHandler.Instance.CurrentFile = new LoadHandler(path).LoadFile();
			PropertyHandler.Instance.CurrentFilePath = path;

			LoadText();
		}

		private void LoadText()
		{
			InProgressText = PropertyHandler.Instance.CurrentFile.InProgress;
			PlannedText = PropertyHandler.Instance.CurrentFile.Planned;
			IdeasText = PropertyHandler.Instance.CurrentFile.Ideas;
			PathText = PropertyHandler.Instance.CurrentFilePath;
		}

		private void SaveText()
		{
			PropertyHandler.Instance.CurrentFile.InProgress = InProgressText;
			PropertyHandler.Instance.CurrentFile.Planned = PlannedText;
			PropertyHandler.Instance.CurrentFile.Ideas = IdeasText;
		}

		#endregion
	}
}
