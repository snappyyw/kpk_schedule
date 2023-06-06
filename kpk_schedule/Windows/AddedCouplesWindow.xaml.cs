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
    /// Логика взаимодействия для AddedCouplesWindow.xaml
    /// </summary>
    public partial class AddedCouplesWindow : Window
    {
        string _name;
        Couples _couples;
        DatabaseEntities databaseEntities = new DatabaseEntities();
        public AddedCouplesWindow(string name)
        {
            InitializeComponent();
            _name = name;
        }

        public AddedCouplesWindow(string name, int id)
        {
            InitializeComponent();
            _name = name;
            ButtonSave.Content = "Изменить";
            _name = name;
            _couples = databaseEntities.Couples.FirstOrDefault(p => p.Id == id);
            Name.Text = _couples.Name;
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
                Couples сouples = new Couples();
                сouples.Name = Name.Text;

                try
                {
                    if (_couples == null)
                    {
                        databaseEntities.Couples.Add(сouples);
                    }
                    else
                    {
                        _couples.Name = Name.Text;
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
                error.AppendLine("Введите пару");
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
