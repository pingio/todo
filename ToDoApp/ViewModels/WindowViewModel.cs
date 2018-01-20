using System;

namespace ToDoApp
{
	class WindowViewModel : BaseViewModel
	{
		private static WindowViewModel instance;
		public WindowViewModel()
		{
			instance = this;
		}

		#region public properties
		public static WindowViewModel Instance => instance ?? new WindowViewModel();
		public AppPage CurrentPage { get; set; } = AppPage.Main;

		/// <summary>
		/// Selects the current window title
		/// </summary>
		public String CurrentTitle
		{
			get
			{
				switch (CurrentPage)
				{
					case (AppPage.Main):
						return "Todo App";

					case (AppPage.Settings):
						return "Todo App - Settings";

					default:
						return "Todo App";
				}
			}
		}
		#endregion
	}
}
