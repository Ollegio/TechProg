using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ListBoxUnits.ItemsSource = Init.Units;
            ListBoxAbils.ItemsSource = Init.AllAbilities;

            foreach (var listItem in ListBoxUnits.Items)
            {
                var listAnimation = new DoubleAnimation
                {
                    From = 0,
                    //To = listItem.,
                    Duration = TimeSpan.FromSeconds(1)
                };
                ListBoxUnits.BeginAnimation(ListBox.HeightProperty, listAnimation);
            }
            

        }


        private void ListBoxUnits_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxUnits.SelectedItems.Count > 0)
            {
                Edit.IsEnabled = true;
                Delete.IsEnabled = true;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var addwnd = new AddWindow();
            ListBoxUnits.BeginInit();
            addwnd.ShowDialog();
            ListBoxUnits.EndInit();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxUnits.SelectedItem == null)
                return;
            var unit = (KeyValuePair<string, Unit>) ListBoxUnits.SelectedItem;
            ListBoxUnits.BeginInit();
            Init.Units.Remove(unit.Key);
            ListBoxUnits.EndInit();
        }
    }
}
