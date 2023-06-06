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
    /// Логика взаимодействия для AddedSubjectsWindow.xaml
    /// </summary>
    public partial class AddedSubjectsWindow : Window
    {
        string _name;
        Lesson _lesson;

        DatabaseEntities databaseEntities = new DatabaseEntities();
        public AddedSubjectsWindow(string name)
        {
            InitializeComponent();
            _name = name;
        }

        public AddedSubjectsWindow(string name, int id)
        {
            InitializeComponent();
            ButtonSave.Content = "Изменить";
            _name = name;
            _lesson = databaseEntities.Lesson.FirstOrDefault(p => p.Id == id);
            Name.Text = _lesson.Name;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (isChecked())
            {
                Lesson lesson = new Lesson();
                lesson.Name = Name.Text;

                try
                {
                    if (_lesson == null)
                    {
                        databaseEntities.Lesson.Add(lesson);
                    }
                    else
                    {
                        _lesson.Name = Name.Text;
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
                error.AppendLine("Введите предмет");
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
