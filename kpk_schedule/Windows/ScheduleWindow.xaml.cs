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
    /// Логика взаимодействия для ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window
    {

        DateTime? _data = null;
        int? _teachers = null;
        int? _lesson = null;
        int? _groups = null;
        DatabaseEntities databaseEntities = new DatabaseEntities();


        private void OnDataChanged()
        {
            var lessons = databaseEntities?.Lesson.ToList();
            var teachers = databaseEntities?.Teachers.ToList();
            var schedule = databaseEntities?.Schedule.ToList();
            var cabinet = databaseEntities?.Cabinet.ToList();
            var couples = databaseEntities?.Couples.ToList();
            var groups = databaseEntities?.Groups.ToList();
            var subject = databaseEntities.Subject.ToList();

            var ScheduleDto = schedule
                        ?.Select(x => new ScheduleDto
                        {
                            Id = x.Id,
                            GroupName = groups.FirstOrDefault(b => b.Id == x.IdGroups).Name,
                            GroupId = groups.FirstOrDefault(b => b.Id == x.IdGroups).Id,
                            CouplesName = couples.FirstOrDefault(b => b.Id == x.IdCouples).Name,
                            CabinetName = cabinet.FirstOrDefault(b => b.Id == x.IdCabinet).Name,
                            LessonName = lessons.FirstOrDefault(j => j.Id == subject.FirstOrDefault(b => b.Id == x.IdSubject).IdLesson.Value).Name,
                            LessonId = lessons.FirstOrDefault(j => j.Id == subject.FirstOrDefault(b => b.Id == x.IdSubject).IdLesson.Value).Id,
                            TeachersName = teachers.FirstOrDefault(j => j.Id == subject.FirstOrDefault(b => b.Id == x.IdSubject).IdTeachers.Value).Name,
                            TeachersId = teachers.FirstOrDefault(j => j.Id == subject.FirstOrDefault(b => b.Id == x.IdSubject).IdTeachers.Value).Id,
                            ScheduleDate = x.Data.Value,
                        })
                        .Where(x => !_data.HasValue || x.ScheduleDate == _data)
                        .Where(x => !_teachers.HasValue || x.TeachersId == _teachers)
                        .Where(x => !_lesson.HasValue || x.LessonId == _lesson)
                        .Where(x => !_groups.HasValue || x.GroupId == _groups)
                        .ToList();

            GridTable.ItemsSource = ScheduleDto;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DataTable.Text))
            {
                _data = DateTime.Parse(DataTable.Text);
                OnDataChanged();
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Teachers.SelectedValue != null && !string.IsNullOrEmpty(Teachers.SelectedValue.ToString()))
            {
                _teachers = int.Parse(Teachers.SelectedValue.ToString());
                OnDataChanged();
            }
        }

        private void ComboBox_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            if (Lesson.SelectedValue != null && !string.IsNullOrEmpty(Lesson.SelectedValue.ToString()))
            {
                _lesson = int.Parse(Lesson.SelectedValue.ToString());
                OnDataChanged();
            }
        }
        private void ComboBox_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {
            if (Groups.SelectedValue != null && !string.IsNullOrEmpty(Groups.SelectedValue.ToString()))
            {
                _groups = int.Parse(Groups.SelectedValue.ToString());
                OnDataChanged();
            }
        }
        public ScheduleWindow()
        {
            InitializeComponent();

            Teachers.ItemsSource = databaseEntities.Teachers.ToList();
            Lesson.ItemsSource = databaseEntities.Lesson.ToList();
            Groups.ItemsSource = databaseEntities.Groups.ToList();
            GridTable.AutoGenerateColumns = false;
            GridTable.Columns.Add(new DataGridTextColumn() { Width = 100, Header = "Группа", Binding = new Binding("GroupName") });
            GridTable.Columns.Add(new DataGridTextColumn() { Width = 100, Header = "Кабинет", Binding = new Binding("CabinetName") });
            GridTable.Columns.Add(new DataGridTextColumn() { Width = 100, Header = "Пара", Binding = new Binding("CouplesName") });
            GridTable.Columns.Add(new DataGridTextColumn() { Width = 100, Header = "Предмет", Binding = new Binding("LessonName") });
            GridTable.Columns.Add(new DataGridTextColumn() { Width = 100, Header = "Преподователь", Binding = new Binding("TeachersName") });
            GridTable.Columns.Add(new DataGridTextColumn() { Width = 100, Header = "Дата", Binding = new Binding("ScheduleDate") {StringFormat= "d" , ConverterCulture=new System.Globalization.CultureInfo("ru-RU") } });

            OnDataChanged();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddedScheduleWindow addedScheduleWindow = new AddedScheduleWindow();
            addedScheduleWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (GridTable.SelectedItems.Count > 0)
            {
                ScheduleDto scheduleDto = (ScheduleDto)GridTable.SelectedItems[0];
                AddedScheduleWindow addedScheduleWindow = new AddedScheduleWindow(scheduleDto.Id);
                addedScheduleWindow.Show();
                this.Close();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (GridTable.SelectedItems.Count > 0)
            {
                ScheduleDto scheduleDto = (ScheduleDto)GridTable.SelectedItems[0];
                var currentItem = databaseEntities.Schedule.FirstOrDefault(item => item.Id == scheduleDto.Id);
                if (MessageBox.Show("Удалить?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Subject subjectItem = databaseEntities.Subject.FirstOrDefault(item => item.Id == currentItem.IdSubject);
                    subjectItem.WorkTime = subjectItem.WorkTime + 1;
                    databaseEntities.Schedule.Remove(currentItem);
                    databaseEntities.SaveChanges();

                    OnDataChanged();
                }
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            _data = null;
            _teachers = null;
            _lesson = null;
            _groups = null;
            Lesson.SelectedValue = null;
            Teachers.SelectedValue = null;
            Groups.SelectedValue = null;
            DataTable.Text = null;
            OnDataChanged();
        }
    }
}
