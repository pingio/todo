using System.Collections.ObjectModel;

namespace ToDoApp
{
	class TodoFile
	{
		public FileSettings Settings { get; set; } = null;
		public ObservableCollection<NoteGroup> NoteGroups { get; set; } = new ObservableCollection<NoteGroup>();
	}
}
