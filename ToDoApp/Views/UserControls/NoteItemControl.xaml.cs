using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ToDoApp
{
	/// <summary>
	/// Interaction logic for NoteItemControl.xaml
	/// </summary>
	public partial class NoteItemControl : UserControl
	{
		public NoteItemControl()
		{
			InitializeComponent();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
	
			base.OnMouseMove(e);
			if (e.LeftButton == MouseButtonState.Pressed && e.OriginalSource.Equals(this.DraggableArea))
			{
				var noteItem = new string[2];

				var tag = (object[])this.NoteItem.Tag;


				// Package the data.
				DataObject data = new DataObject();
				data.SetData(DataFormats.StringFormat, this.NoteItem.Tag);

				// Inititate the drag-and-drop operation.
				DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
			}
		}
	}
}
