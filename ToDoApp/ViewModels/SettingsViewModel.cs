using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

namespace ToDoApp
{
    class SettingsViewModel : BaseViewModel
    {
		#region private fields
		private bool canExecute = true;
		#endregion

		#region Buttons
		#region MainPageButton

		public String MainPageButtonText { get; set; } = "Back to Main Page.";

		private ICommand mainPageButton;
		public ICommand MainPageButton
		{
			get
			{
				return mainPageButton ?? (mainPageButton = new RelayCommand(() => MainPageButtonCommand(), canExecute));
			}
		}

		private void MainPageButtonCommand()
		{

			WindowViewModel.Instance.CurrentPage = AppPage.Main;
		}
		#endregion

		#region SaveSettingsButton

		public String SaveSettingsButtonText { get; set; } = "Save Settings";

		private ICommand saveSettingsButton;
		public ICommand SaveSettingsButton
		{
			get
			{
				return saveSettingsButton ?? (saveSettingsButton = new RelayCommand(() => SaveSettingsButtonCommand(), canExecute));
			}
		}

		private void SaveSettingsButtonCommand()
		{
			Properties.Settings.Default.FontSize = FontSizeSelected;
			Properties.Settings.Default.Save();

		}

		#endregion

		#endregion

		#region Settings options
		public int FontSizeSelected { get; set; } = Properties.Settings.Default.FontSize;

		public IEnumerable<Int32> FontSizeList
		{
			get
			{
				var sizes = new List<Int32>
				{
					14,
					16,
					18,
					20,
					22,
					24
				};

				return sizes;
			}
		}

		#endregion
	}
}
