using System;

namespace ToDoApp
{
				class NoteItem
				{

								public NoteItem(string shortString, string longString = null, string id = null)
								{
												ShortString = shortString;
												LongString = longString;
												if (string.IsNullOrEmpty(id))
																ID = Guid.NewGuid().ToString();
												else
																ID = id;
								}

								public string ShortString { get; set; }

								public string LongString { get; set; }
								
								public string ID { get; set; }
				}
}
