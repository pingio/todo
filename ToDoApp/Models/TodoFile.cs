
namespace ToDoApp
{
	class TodoFile
	{

		/// <summary>
		/// Contents of the InProgress part, separated by line.
		/// </summary>
		public string InProgress { get; set; }

		/// <summary>
		/// Contents of the Planned part, separated by line.
		/// </summary>
		public string Planned { get; set; }


		/// <summary>
		/// Contents of the Ideas part, separated by line.
		/// </summary>
		public string Ideas { get; set; }
	}
}
