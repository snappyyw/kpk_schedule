using kpk_schedule.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace kpk_schedule.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddedGroupsWindow.xaml
    /// </summary>
    public partial class AddedGroupsWindow : Window
    {

        string _name;
        Groups _groups;

        DatabaseEntities databaseEntities = new DatabaseEntities();
        public AddedGroupsWindow(string name)
        {
            InitializeComponent();
            _name = name;
        }

        public AddedGroupsWindow(string name, int id)
        {
            InitializeComponent();
            ButtonSave.Content = "Изменить";
            _name = name;
            _groups = databaseEntities.Groups.FirstOrDefault(p => p.Id == id);
            Name.Text = _groups.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TableWindow tableWindow = new TableWindow(_name);
            tableWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (isChecked())
            {
                Groups groups = new Groups();
                groups.Name = Name.Text;

                try
                {
                    if (_groups == null)
                    {
                        databaseEntities.Groups.Add(groups);
                    }
                    else
                    {
                        _groups.Name = Name.Text;
                    }

                    databaseEntities.SaveChanges();
                    TableWindow tableWindow = new TableWindow(_name);
                    tableWindow.Show();
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошбика!");
                }
            }
        }

        private bool isChecked()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(Name.Text))
            {
                error.AppendLine("Введите группу");
            }


            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Ошбика!");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
