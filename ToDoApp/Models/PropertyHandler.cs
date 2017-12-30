namespace ToDoApp
{
	class PropertyHandler
	{
		private static PropertyHandler instance;
		public PropertyHandler() => instance = this;

		public static PropertyHandler Instance => instance ?? new PropertyHandler();

		public TodoFile CurrentFile { get; set; }

		public string CurrentFilePath { get; set; }
	}
}
