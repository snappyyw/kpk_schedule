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
    /// Логика взаимодействия для AddedTeachersWindow.xaml
    /// </summary>
    public partial class AddedTeachersWindow : Window
    {
        DatabaseEntities databaseEntities = new DatabaseEntities();
        string _name;
        Teachers _teachers;
        public AddedTeachersWindow(string name)
        {
            InitializeComponent();
            _name = name;
        }

        public AddedTeachersWindow(string name, int id)
        {
            InitializeComponent();
            ButtonSave.Content = "Изменить";
            _name = name;
            _teachers = databaseEntities.Teachers.FirstOrDefault(p => p.Id == id);
            Name.Text = _teachers.Name;
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
                Teachers  teachers = new Teachers();
                teachers.Name = Name.Text;

                try
                {
                    if (_teachers == null)
                    {
                        databaseEntities.Teachers.Add(teachers);
                    }
                    else
                    {
                        _teachers.Name = Name.Text;
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
                error.AppendLine("Введите ФИО");
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
