using System;
using System.Collections.Generic;
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
		/// <summary>
		/// <see cref="MainPageButtonCommand"/>
		/// </summary>
		public ICommand MainPageButton => mainPageButton ?? (mainPageButton = new RelayCommand(param => MainPageButtonCommand(), canExecute));

		/// <summary>
		/// Changes view to main view
		/// </summary>
		private void MainPageButtonCommand => WindowViewModel.Instance.CurrentPage = AppPage.Main;
		#endregion

		#region SaveSettingsButton

		public String SaveSettingsButtonText { get; set; } = "Save Settings";
		private ICommand saveSettingsButton;

		/// <summary>
		/// <see cref="SaveSettingsButtonCommand"/>
		/// </summary>
		public ICommand SaveSettingsButton =>
			saveSettingsButton ?? (saveSettingsButton = new RelayCommand(param => SaveSettingsButtonCommand(), canExecute));
		
		/// <summary>
		/// Saves the settings as set in the view.
		/// </summary>
		private void SaveSettingsButtonCommand()
		{
			Properties.Settings.Default.FontSize = FontSizeSelected;
			Properties.Settings.Default.Save();

		}

		#endregion

		#endregion

		#region Settings options
		public int FontSizeSelected { get; set; } = Properties.Settings.Default.FontSize;

		/// <summary>
		/// Shows the list of available font sizes.
		/// </summary>
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
