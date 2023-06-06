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
    /// Логика взаимодействия для AddedCabinetWindow.xaml
    /// </summary>
    public partial class AddedCabinetWindow : Window
    {

        string _name;
        Cabinet _сabinet;
        DatabaseEntities databaseEntities = new DatabaseEntities();
        public AddedCabinetWindow(string name)
        {
            InitializeComponent();
            _name = name;
        }

        public AddedCabinetWindow(string name, int id)
        {
            InitializeComponent();
            _name = name;
            ButtonSave.Content = "Изменить";
            _name = name;
            _сabinet = databaseEntities.Cabinet.FirstOrDefault(p => p.Id == id);
            Name.Text = _сabinet.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TableWindow tableWindow = new TableWindow(_name);
            tableWindow.Show();
            this.Close();
        }

        private bool isChecked()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(Name.Text))
            {
                error.AppendLine("Введите кабинет");
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (isChecked())
            {
                Cabinet cabinet = new Cabinet();
                cabinet.Name = Name.Text;

                try
                {
                    if (_сabinet == null)
                    {
                        databaseEntities.Cabinet.Add(cabinet);
                    }
                    else
                    {
                        _сabinet.Name = Name.Text;
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
    }
}
