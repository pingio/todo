using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

//TODO: Remove useage of Windows-specific methods.

namespace ToDoApp
{
	/// <summary>
	/// Deals with the MainPage.xaml view
	/// </summary>
	class MainViewModel : BaseViewModel
	{

		#region Constructor

		private bool canExecute;
		private Dictionary<string, NoteGroup> noteGroupKeys;


		public MainViewModel()
		{
			canExecute = true;

			//Create empty NoteGroups
			noteGroupKeys = new Dictionary<string, NoteGroup>();
			NoteGroups = new ObservableCollection<NoteGroup>();

			//Update the NoteGroups
			LoadText();
		}

		#endregion

		#region Buttons

		#region FileButtons

		#region OpenButton

		private ICommand openButton;
		/// <summary>
		/// Enables the usage of <see cref="OpenButtonCommand"/>
		/// </summary>
		public ICommand OpenButton => openButton ?? (openButton = new RelayCommand(param => OpenButtonCommand(), canExecute));
		public String OpenButtonText { get; set; } = "Open...";


		/// <summary>
		/// Lets user open an existing file and updates the content to that file's content.
		/// </summary>
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

		/// <summary>
		/// <see cref="SaveButtonCommand"/>
		/// </summary>
		public ICommand SaveButton => saveButton ?? (saveButton = new RelayCommand(param => SaveButtonCommand(), canExecute));
		public String SaveButtonText { get; set; } = "Save";

		/// <summary>
		/// Tries to save the contents to a file.
		/// <see cref="SaveText"/>
		/// <seealso cref="SaveHandler.Save"/>
		/// </summary>
		private void SaveButtonCommand()
		{
			SaveText();
			bool isSaved = new SaveHandler(PropertyHandler.Instance.CurrentFile).Save(PropertyHandler.Instance.CurrentFilePath);
		}
		#endregion

		#region Save As Button
		private ICommand saveAsButton;
		/// <summary>
		/// <see cref="SaveButtonCommand"/>
		/// </summary>
		public ICommand SaveAsButton => saveAsButton ?? (saveAsButton = new RelayCommand(param => SaveAsButtonCommand(), canExecute));
		public String SaveAsButtonText { get; set; } = "Save as...";

		/// <summary>
		/// tries to save the file as a .todo file where the user specifies it.
		/// </summary>
		private void SaveAsButtonCommand()
		{
			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.Filter = "ToDo files (*.todo)|*.todo|All files (*.*)|*.*";

			fileDialog.FileName = "new file";
			fileDialog.AddExtension = true;
			fileDialog.DefaultExt = ".todo";


			if (fileDialog.ShowDialog() == true)
			{
				SaveText();
				bool isSaved = new SaveHandler(PropertyHandler.Instance.CurrentFile).Save(fileDialog.FileName);

				// Updates the path in case the filepath changed.
				if (isSaved)
					PathText = PropertyHandler.Instance.CurrentFilePath;

			}
		}
		#endregion

		#region Settings Button
		private ICommand settingsButton;
		/// <summary>
		/// <see cref="SettingsButtonCommand"/>
		/// </summary>
		public ICommand SettingsButton => settingsButton ?? (settingsButton = new RelayCommand(param => SettingsButtonCommand(), canExecute));
		public String SettingsButtonText { get; set; } = "Settings";

		/// <summary>
		/// Saves current file's state and changes the view to the settingsview
		/// </summary>
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
		/// <summary>
		/// <see cref="AddNoteGroupCommand"/>
		/// </summary>
		public ICommand AddNoteGroupButton => addNoteGroupButton ?? (addNoteGroupButton = new RelayCommand(param => AddNoteGroupCommand(), canExecute));

		/// <summary>
		/// Creates a new note group with the default name "New note group"
		/// </summary>
		private void AddNoteGroupCommand()
		{
			var newNoteGroup = new NoteGroup("New note group");

			NoteGroups.Add(newNoteGroup);
			noteGroupKeys[newNoteGroup.ID] = newNoteGroup;
		}

		#endregion

		#region RemoveNoteGroupButton

		private ICommand removeNoteGroupButton;

		/// <summary>
		/// <see cref="RemoveNoteGroupCommand(object)"/>
		/// </summary>
		public ICommand RemoveNoteGroupButton =>
			removeNoteGroupButton ?? (removeNoteGroupButton = new RelayCommand(param => RemoveNoteGroupCommand(param), canExecute));

		/// <summary>
		/// Removes a note group from the file contents
		/// </summary>
		/// <param name="param">The command parameter from the view binding, will be the ID of the note group</param>
		private void RemoveNoteGroupCommand(object param)
		{
			var noteGroup = GetNoteGroup(param.ToString());
			NoteGroups.Remove(noteGroup);
			noteGroupKeys.Remove(param.ToString());

		}

		#endregion

		#region AddNoteItemButton
		private ICommand addNoteItemButton;
		/// <summary>
		/// <see cref="AddNoteItemCommand(object)"/>
		/// </summary>
		public ICommand AddNoteItemButton =>
			addNoteItemButton ?? (addNoteItemButton = new RelayCommand(param => AddNoteItemCommand(param), canExecute));

		/// <summary>
		/// Add a note item to the note group
		/// </summary>
		/// <param name="param">The command parameter from the view, will be the ID of the note group the item belongs in</param>
		public void AddNoteItemCommand(object param)
		{


			if (param.GetType() == typeof(object[]))
			{
				var para = (object[])param;
				var noteGroup = GetNoteGroup(para[0].ToString());

				noteGroup.AddNoteItem(new NoteItem((string)para[1].ToString()));
			}
			else
			{
				var noteGroup = GetNoteGroup(param.ToString());

				noteGroup.AddNoteItem(new NoteItem(""));

			}
		}

		#endregion

		#region RemoveNoteItemButton

		private ICommand removeNoteItemButton;
		/// <summary>
		/// <see cref=" RemoveNoteItemCommand(object)"/>
		/// </summary>
		public ICommand RemoveNoteItemButton =>
			removeNoteItemButton ?? (removeNoteItemButton = new RelayCommand(param => RemoveNoteItemCommand(param), canExecute));

		/// <summary>
		/// Removes a note item from the notegroup
		/// </summary>
		/// <param name="param">The command parameters from the view.
		/// param[0] will be the note group and param[1] will be the noteitem
		/// </param>
		public void RemoveNoteItemCommand(object param)
		{
			var values = (object[])param;

			var noteGroup = GetNoteGroup(values[0].ToString());
			noteGroup.RemoveNoteItem(values[1].ToString());


		}

		#endregion

		#endregion

		#region UpdateLink

		#region Show UpdateLink
		private ICommand updateLink;
		public ICommand UpdateLink => updateLink ?? (updateLink = new RelayCommand(param => UpdateLinkCommand(), canExecute));

		private void UpdateLinkCommand()
		{
			// Opens the link in the default web browser
			System.Diagnostics.Process.Start("https://github.com/pingio/todo/releases");
			ShowUpdateLink = false;
		}

		private bool ShowUpdateLink { get; set; } = true;

		#endregion

		private ICommand hideUpdateLink;
		public ICommand HideUpdateLink => hideUpdateLink ?? (hideUpdateLink = new RelayCommand(param => HideUpdateLinkCommand(), canExecute));

		private void HideUpdateLinkCommand() => ShowUpdateLink = false;
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
		/// <summary>
		/// Updates the current view with the contents from the current file.
		/// </summary>
		private void LoadText()
		{

			PathText = PropertyHandler.Instance.CurrentFilePath;
			NoteGroups = PropertyHandler.Instance.CurrentFile.NoteGroups;
			// Adding notegroup to the dictionary so we can fetch them by ID
			foreach (NoteGroup n in NoteGroups ?? Enumerable.Empty<NoteGroup>())
			{
				noteGroupKeys[n.ID] = n;
			}
		}

		private void SaveText()
		{
			PropertyHandler.Instance.CurrentFile.NoteGroups = NoteGroups;
		}

		/// <summary>
		/// Current font size for noteitems, set in the settings view.
		/// </summary>
		public int FontSize => Properties.Settings.Default.FontSize;


		public NoteGroup GetNoteGroup(string id) => NoteGroups.FirstOrDefault(i => i.ID == noteGroupKeys[id].ID);


		#endregion

	}
}
