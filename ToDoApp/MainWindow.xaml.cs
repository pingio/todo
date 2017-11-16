using System.Windows;
using System;
namespace ToDoApp
{
				/// <summary>
				/// Interaction logic for MainWindow.xaml
				/// </summary>
				public partial class MainWindow : Window
				{
								public MainWindow()
								{
												this.DataContext = new MainViewModel();
												InitializeComponent();
								}
				}
}
