using Microsoft.Win32;
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
		private Dictionary<string, NoteGroup> noteGroupKeys;


		public MainViewModel()
		{
			canExecute = true;

			noteGroupKeys = new Dictionary<string, NoteGroup>();
			noteGroups = new ObservableCollection<NoteGroup>();

			NoteGroups = noteGroups;

			LoadText();
		}

		#endregion

		#region Buttons

		#region FileButtons

		#region OpenButton


		private ICommand openButton;
		public ICommand OpenButton
		{
			get
			{
				return openButton ?? (openButton = new RelayCommand(param => OpenButtonCommand(), canExecute));
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
				return saveButton ?? (saveButton = new RelayCommand(param => SaveButtonCommand(), canExecute));
			}
		}

		public String SaveButtonText { get; set; } = "Save";

		private void SaveButtonCommand()
		{
			SaveText();
			bool isSaved = new SaveHandler(PropertyHandler.Instance.CurrentFile).Save(PropertyHandler.Instance.CurrentFilePath);
			Debug.WriteLine(isSaved);
		}
		#endregion

		#region Save As Button
		private ICommand saveAsButton;
		public ICommand SaveAsButton
		{
			get
			{
				return saveAsButton ?? (saveAsButton = new RelayCommand(param => SaveAsButtonCommand(), canExecute));
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
				return settingsButton ?? (settingsButton = new RelayCommand(param => SettingsButtonCommand(), canExecute));
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

		#region NoteButtons

		#region AddNoteGroupButton
		private ICommand addNoteGroupButton;
		public ICommand AddNoteGroupButton
		{
			get
			{
				return addNoteGroupButton ?? (addNoteGroupButton = new RelayCommand(param => AddNoteGroupCommand(), canExecute));
			}
		}

		private void AddNoteGroupCommand()
		{
			var newNoteGroup = new NoteGroup("New Note Group");

			NoteGroups.Add(newNoteGroup);
			noteGroupKeys[newNoteGroup.ID] = newNoteGroup;
		}

		#endregion

		#region RemoveNoteGroupButton

		private ICommand removeNoteGroupButton;
		public ICommand RemoveNoteGroupButton
		{
			get
			{
				return removeNoteGroupButton ?? (removeNoteGroupButton = new RelayCommand(param => RemoveNoteGroupCommand(param), canExecute));
			}
		}

		private void RemoveNoteGroupCommand(object param)
		{
			var noteGroup = GetNoteGroup(param.ToString());
			NoteGroups.Remove(noteGroup);
			noteGroupKeys.Remove(param.ToString());

		}

		#endregion

		#region AddNoteItemButton

		private ICommand addNoteItemButton;
		public ICommand AddNoteItemButton
		{
			get
			{
				return addNoteItemButton ?? (addNoteItemButton = new RelayCommand(param => AddNoteItemCommand(param), canExecute));
			}
		}

		private void AddNoteItemCommand(object param)
		{
			var noteGroup = GetNoteGroup(param.ToString());

			noteGroup.AddNoteItem(new NoteItem("Hello."));
		}

		#endregion

		#endregion

		#endregion

		#region Content
		public String PathText { get; set; } = PropertyHandler.Instance.CurrentFilePath;

		public ObservableCollection<NoteGroup> NoteGroups { get; set; }


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

			PathText = PropertyHandler.Instance.CurrentFilePath;
			NoteGroups = PropertyHandler.Instance.CurrentFile.NoteGroups;
			foreach (NoteGroup n in NoteGroups ?? Enumerable.Empty<NoteGroup>())
			{
				noteGroupKeys[n.ID] = n;
			}
		}

		private void SaveText()
		{
			PropertyHandler.Instance.CurrentFile.NoteGroups = NoteGroups;
			Debug.WriteLine(NoteGroups.Count);
		}

		public int FontSize => Properties.Settings.Default.FontSize;


		public NoteGroup GetNoteGroup(string id)
		{
			return noteGroupKeys[id];
		}
		#endregion

	}
}
