using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ToDoApp
{
	class NoteGroup
	{
		private Dictionary<string, NoteItem> noteItems;
		private ObservableCollection<NoteItem> noteItemPositions;

		public NoteGroup(string title, string id = null)
		{
			Title = title;

			if (string.IsNullOrEmpty(id))
				ID = Guid.NewGuid().ToString();
			else
				ID = id;

			noteItems = new Dictionary<string, NoteItem>();
			noteItemPositions = new ObservableCollection<NoteItem>();
		}

		public string Title { get; set; }

		public string ID { get; set; }

		#region NoteItems
		public void AddNoteItem(NoteItem item)
		{
			noteItems.Add(item.ID, item);
			noteItemPositions.Add(item);
		}

		public void RemoveNoteItem(string id)
		{
			noteItems.Remove(id);
			noteItemPositions.Remove(noteItemPositions.SingleOrDefault(i => i.ID.Equals(id)));
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
