﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
		private ObservableCollection<NoteGroup> noteGroups;


		public MainViewModel()
		{
			canExecute = true;
			var noteGroup1 = new NoteGroup("test");
			noteGroup1.AddNoteItem(new NoteItem("test"));
			noteGroup1.AddNoteItem(new NoteItem("test2"));

			var noteGroup2 = new NoteGroup("test");
			noteGroup2.AddNoteItem(new NoteItem("test"));
			noteGroup2.AddNoteItem(new NoteItem("test2"));

			noteGroup2.AddNoteItem(new NoteItem("test2"));
			noteGroups = new ObservableCollection<NoteGroup>
			{
				noteGroup1,
				noteGroup2,
				new NoteGroup("test54")
			
			};
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
				SaveText();
				bool isSaved = new SaveHandler(PropertyHandler.Instance.CurrentFile).Save(PropertyHandler.Instance.CurrentFilePath);
		}
		#endregion

		#region Save  Button
		private ICommand saveAsButton;
		public ICommand SaveAsButton
		{
			get
			{
				return saveAsButton ?? (saveAsButton = new RelayCommand(() => SaveAsButtonCommand(), canExecute));
			}
		}

		public String SaveAsButtonText { get; set; } = "Save as...";

		private void SaveAsButtonCommand()
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
				
				if (isSaved)
					PathText = PropertyHandler.Instance.CurrentFilePath;

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
		public String PathText { get; set; } = PropertyHandler.Instance.CurrentFilePath;

		public ObservableCollection<NoteGroup> NoteGroups => noteGroups;


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
				
		}

		private void SaveText()
		{
		}

		public int FontSize => Properties.Settings.Default.FontSize;
		#endregion
	}
}
