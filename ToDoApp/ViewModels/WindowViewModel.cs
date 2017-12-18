using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

		public String CurrentTitle {
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
