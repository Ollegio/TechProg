using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private Unit newUnit;
        private List<string> unitAbils;
        public AddWindow()
        {
            InitializeComponent();
            unitAbils = new List<string>();
            ListBoxUnitAbils.ItemsSource = unitAbils;
            foreach (var ability in Init.AllAbilities.Keys)
            {
                ListBoxAbilsAdd.Items.Add(ability);
            }
            var addAnimation = new DoubleAnimation
            {
                From = 0,
                To = 5,
                Duration = TimeSpan.FromSeconds(5)
            };
            BeginAnimation(OpacityProperty, addAnimation);
        }


        private void AddWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void AddAbil_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxAbilsAdd.SelectedItem == null)
                return;
            var abil = ListBoxAbilsAdd.SelectedItem;
            ListBoxAbilsAdd.Items.Remove(abil);
           
            ListBoxUnitAbils.BeginInit();
            unitAbils.Add(abil.ToString());
            ListBoxUnitAbils.EndInit();
        }

        private void RemAbil_OnClick(object sender, RoutedEventArgs e)
        {
            if (ListBoxUnitAbils.SelectedItem == null)
                return;
            var abil = ListBoxUnitAbils.SelectedItem;
            ListBoxAbilsAdd.Items.Add(abil.ToString());

            ListBoxUnitAbils.BeginInit();
            unitAbils.Remove(abil.ToString());
            ListBoxUnitAbils.EndInit();
        }

        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            newUnit = new Unit(NameText.Text, Int32.Parse(HealthText.Text), Int32.Parse(ManaText.Text));
            newUnit.abilities.AddRange(unitAbils);
            Init.Units.Add(newUnit.Name, newUnit);
            Close();
        }
    }
}
