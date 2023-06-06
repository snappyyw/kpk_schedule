using kpk_schedule.Dto;
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
    /// Логика взаимодействия для AddedScheduleWindow.xaml
    /// </summary>
    public partial class AddedScheduleWindow : Window
    {

        DatabaseEntities databaseEntities = new DatabaseEntities();
        Schedule _schedule;
        public AddedScheduleWindow()
        {
            InitializeComponent();
            Group.ItemsSource = databaseEntities.Groups.ToList();
            Cabinet.ItemsSource = databaseEntities.Cabinet.ToList();
            Couples.ItemsSource = databaseEntities.Couples.ToList();

            var subject = databaseEntities.Subject.ToList();
            var lessons = databaseEntities?.Lesson.ToList();
            var teachers = databaseEntities?.Teachers.ToList();

            var subjectDtoCurrent = subject
                        ?.Select(x => new SubjectDto
                        {
                            Id = x.Id,
                            Name = $"{teachers.FirstOrDefault(y=> y.Id == x.IdTeachers).Name}/{lessons.FirstOrDefault(y => y.Id == x.IdLesson).Name}"
                        }).ToList();

            Subject.ItemsSource = subjectDtoCurrent.ToList();
        }

        public AddedScheduleWindow(int id)
        {
            InitializeComponent();
            ButtonSave.Content = "Изменить";
            Group.ItemsSource = databaseEntities.Groups.ToList();
            Cabinet.ItemsSource = databaseEntities.Cabinet.ToList();
            Couples.ItemsSource = databaseEntities.Couples.ToList();

            var subject = databaseEntities.Subject.ToList();
            var lessons = databaseEntities?.Lesson.ToList();
            var teachers = databaseEntities?.Teachers.ToList();

            var subjectDtoCurrent = subject
                        ?.Select(x => new SubjectDto
                        {
                            Id = x.Id,
                            Name = $"{teachers.FirstOrDefault(y => y.Id == x.IdTeachers).Name}/{lessons.FirstOrDefault(y => y.Id == x.IdLesson).Name}"
                        }).ToList();

            Subject.ItemsSource = subjectDtoCurrent.ToList();

            _schedule = databaseEntities.Schedule.FirstOrDefault(x => x.Id == id);
            Subject.SelectedValue = _schedule.IdSubject;
            Group.SelectedValue = _schedule.IdGroups;
            Cabinet.SelectedValue = _schedule.IdCabinet;
            Couples.SelectedValue = _schedule.IdCouples;
            DataSchedule.DisplayDate = _schedule.Data.Value;
            DataSchedule.Text = _schedule.Data.Value.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ScheduleWindow scheduleWindow = new ScheduleWindow();
            scheduleWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (isChecked())
            {
                Schedule schedule = new Schedule();
                schedule.IdGroups = int.Parse(Group.SelectedValue.ToString());
                schedule.IdSubject = int.Parse(Subject.SelectedValue.ToString());
                schedule.IdCabinet = int.Parse(Cabinet.SelectedValue.ToString());
                schedule.IdCouples = int.Parse(Couples.SelectedValue.ToString());
                schedule.Data = DateTime.Parse(DataSchedule.Text);

                try
                {
                    if (_schedule == null)
                    {
                        Subject _currentSubject = databaseEntities.Subject.FirstOrDefault(i => i.Id == schedule.IdSubject);
                        if (_currentSubject.WorkTime <= 0)
                        {
                            MessageBox.Show("У преподователя кончились пары!", "Ошбика!");
                            return;
                        }
                        _currentSubject.WorkTime = _currentSubject.WorkTime - 1;
                        databaseEntities.Schedule.Add(schedule);
                    }
                    else
                    {
                        _schedule.IdGroups = int.Parse(Group.SelectedValue.ToString());
                        _schedule.IdSubject = int.Parse(Subject.SelectedValue.ToString());
                        _schedule.IdCabinet = int.Parse(Cabinet.SelectedValue.ToString());
                        _schedule.IdCouples = int.Parse(Couples.SelectedValue.ToString());
                        _schedule.Data = DateTime.Parse(DataSchedule.Text);
                    }

                    databaseEntities.SaveChanges();
                    ScheduleWindow scheduleWindow = new ScheduleWindow();
                    scheduleWindow.Show();
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
            if (Group.SelectedValue == null)
            {
                error.AppendLine("Выберите группу");
            }
            if (Cabinet.SelectedValue == null)
            {
                error.AppendLine("Выберите кабинет");
            }
            if (Subject.SelectedValue == null)
            {
                error.AppendLine("Выберите предмет и преподователя");
            }
            if (string.IsNullOrEmpty(DataSchedule.Text))
            {
                error.AppendLine("Выберите дату");
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
