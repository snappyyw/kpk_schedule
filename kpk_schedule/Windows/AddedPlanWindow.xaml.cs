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
    /// Логика взаимодействия для AddedPlanWindow.xaml
    /// </summary>
    public partial class AddedPlanWindow : Window
    {
        string _name;
        DatabaseEntities databaseEntities = new DatabaseEntities();
        Subject _subject;
        public AddedPlanWindow(string name)
        {
            InitializeComponent();
            Name1.ItemsSource = databaseEntities.Teachers.ToList();
            Name2.ItemsSource = databaseEntities.Lesson.ToList();
            _name = name;
        }

        public AddedPlanWindow(string name, int id)
        {
            InitializeComponent();
            ButtonSave.Content = "Изменить";
            Name1.ItemsSource = databaseEntities.Teachers.ToList();
            Name2.ItemsSource = databaseEntities.Lesson.ToList();
            _subject = databaseEntities.Subject.FirstOrDefault(x => x.Id == id);
            Name.Text = _subject.WorkTime.ToString();
            Name1.SelectedValue = _subject.IdTeachers;
            Name2.SelectedValue = _subject.IdLesson;
            _name = name;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (isChecked())
            {
                Subject subject = new Subject();
                subject.WorkTime = int.Parse(Name.Text);
                Console.WriteLine(Name1.SelectedItem.ToString());
                subject.IdTeachers = int.Parse(Name1.SelectedValue.ToString());
                subject.IdLesson = int.Parse(Name2.SelectedValue.ToString());

                try
                {
                    if (_subject == null)
                    {
                        databaseEntities.Subject.Add(subject);
                    }
                    else
                    {
                        _subject.WorkTime = int.Parse(Name.Text);
                        _subject.IdTeachers = int.Parse(Name1.SelectedValue.ToString());
                        _subject.IdLesson = int.Parse(Name2.SelectedValue.ToString());
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
                error.AppendLine("Введите часы");
            }
            if (Name1.SelectedValue == null)
            {
                error.AppendLine("Выберите преподователя");
            }
            if (Name2.SelectedValue == null)
            {
                error.AppendLine("Выберите предмет");
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
