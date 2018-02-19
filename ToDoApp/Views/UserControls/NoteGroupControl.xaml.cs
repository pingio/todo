using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoApp
{
	/// <summary>
	/// Interaction logic for NoteGroupControl.xaml
	/// </summary>
	public partial class NoteGroupControl : UserControl
	{
		public NoteGroupControl()
		{
			InitializeComponent();
		}

		protected override void OnDrop(DragEventArgs e)
		{
			base.OnDrop(e);
			if (e.Data.GetDataPresent(DataFormats.StringFormat))
			{
				var note = (object[])e.Data.GetData(DataFormats.StringFormat);
				var viewModel = (MainViewModel)WindowViewModel.Instance.CurrentViewModel;


				var text = viewModel.GetNoteGroup(note[0].ToString())
					.GetNoteItem(note[1].ToString());
				

				object[] param = new object[2];
				param[0] = this.Tag;
				param[1] = text.ShortString;
				viewModel.RemoveNoteItemCommand(note);
				viewModel.AddNoteItemCommand(param);

			}
			
			e.Handled = true;
		}
	}
}
