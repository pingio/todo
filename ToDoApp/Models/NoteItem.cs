using System;

namespace ToDoApp
{
	class NoteItem
	{

		public NoteItem(string shortString)
		{
			ShortString = shortString;
			ID = Guid.NewGuid().ToString();
		}

		public NoteItem(string shortString, string id)
		{
			ShortString = shortString;
			ID = id;
		}


		public string ID { get; set; }

		public string ShortString { get; set; }

		public string LongString { get; set; }
	}
}
