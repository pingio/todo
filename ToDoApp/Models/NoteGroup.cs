using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace ToDoApp
{
	class NoteGroup
	{
		private Dictionary<string, NoteItem> noteItems;
		private ObservableCollection<NoteItem> noteItemPositions;

		public NoteGroup(string title)
		{
			Title = title;
			noteItems = new Dictionary<string, NoteItem>();
			noteItemPositions = new ObservableCollection<NoteItem>();
		}

		public string Title { get; set; }

		#region NoteItems
		public void AddNoteItem(NoteItem item)
		{
			noteItems.Add(item.ID, item);
			noteItemPositions.Add(item);
		}

		public void RemoveNoteItem(string id)
		{
			if (ExistsNoteItem(id))
			{
				noteItems.Remove(id);
				noteItemPositions.Remove(GetNoteItem(id));
			}
		}

		public NoteItem GetNoteItem(string id)
		{
			return ExistsNoteItem(id) ? noteItems[id] : null;
		}

		public bool ExistsNoteItem(string id)
		{
			return noteItems.ContainsKey(id);
		}
		#endregion

		#region NoteItemPositions
		public int GetPosition(string id)
		{
			if (!ExistsNoteItem(id))
				return -1;

			var note = GetNoteItem(id);

			return noteItemPositions.IndexOf(note);
		}

		public void SetPosition(string id, int newPos)
		{
			if (!ExistsNoteItem(id))
				return;

			noteItemPositions.Move(GetPosition(id), newPos);
		}

		public ObservableCollection<NoteItem> GetNoteItems
		{
			get
			{
				return noteItemPositions;
			}
		}
		#endregion
	}
}
