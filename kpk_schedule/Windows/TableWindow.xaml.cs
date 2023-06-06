
using kpk_schedule.Dto;
using kpk_schedule.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        string _name;

        DatabaseEntities databaseEntities = new DatabaseEntities();

        public TableWindow(string name)
        {
            InitializeComponent();

            GridTable.AutoGenerateColumns = false;

            DatabaseEntities databaseEntities = new DatabaseEntities();

            _name = name;
            switch (_name)
            {
                case "Groups":
                    GridTable.Columns.Add(new DataGridTextColumn() { Width=200, Header = "Название групп", Binding = new Binding("Name")});
                    GridTable.ItemsSource = databaseEntities.Groups.ToList();
                    break;
                case "Couples":
                    GridTable.Columns.Add(new DataGridTextColumn() { Width = 200, Header = "Пары", Binding = new Binding("Name") });
                    GridTable.ItemsSource = databaseEntities.Couples.ToList();
                    break;
                case "Cabinet":
                    GridTable.Columns.Add(new DataGridTextColumn() { Width = 200, Header = "Кабинет", Binding = new Binding("Name") });
                    GridTable.ItemsSource = databaseEntities.Cabinet.ToList();
                    break;
                case "Teachers":
                    GridTable.Columns.Add(new DataGridTextColumn() { Width = 200, Header = "ФИО", Binding = new Binding("Name") });
                    GridTable.ItemsSource = databaseEntities.Teachers.ToList();
                    break;
                case "Plan":
                    GridTable.Columns.Add(new DataGridTextColumn() { Width = 200, Header = "Кол-во пар", Binding = new Binding("WorkTime") });
                    GridTable.Columns.Add(new DataGridTextColumn() { Width = 200, Header = "ФИО", Binding = new Binding("TeachersName") });
                    GridTable.Columns.Add(new DataGridTextColumn() { Width = 200, Header = "Предмет", Binding = new Binding("LessonName") });
                    var subjects = databaseEntities?.Subject.ToList();
                    var lessons = databaseEntities?.Lesson.ToList();
                    var teachers = databaseEntities?.Teachers.ToList();
                    var planDto = subjects
                        ?.Select(x => new PlanDto
                        {
                            Id = x.Id,
                            WorkTime = x.WorkTime,
                            LessonName = lessons.FirstOrDefault(p => p.Id == x.IdLesson).Name,
                            TeachersName = teachers.FirstOrDefault(p => p.Id == x.IdTeachers).Name
                        }).ToList();

                    GridTable.ItemsSource = planDto;
                    break;
                case "Subjects":
                    GridTable.Columns.Add(new DataGridTextColumn() { Width = 200, Header = "Предмет", Binding = new Binding("Name") });
                    GridTable.ItemsSource = databaseEntities.Lesson.ToList();
                    break;
            }

            
        }

        private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (_name)
            {
                case "Teachers":
                    AddedTeachersWindow addedTeachersWindow = new AddedTeachersWindow(_name);
                    addedTeachersWindow.Show();
                    this.Close();
                    break;
                case "Couples":
                    AddedCouplesWindow addedCouplesWindow = new AddedCouplesWindow(_name);
                    addedCouplesWindow.Show();
                    this.Close();
                    break;
                case "Cabinet":
                    AddedCabinetWindow addedCabinetWindow = new AddedCabinetWindow(_name);
                    addedCabinetWindow.Show();
                    this.Close();
                    break;
                case "Groups":
                    AddedGroupsWindow addedGroupsWindow = new AddedGroupsWindow(_name);
                    addedGroupsWindow.Show();
                    this.Close();
                    break;
                case "Plan":
                    AddedPlanWindow addedPlanWindow = new AddedPlanWindow(_name);
                    addedPlanWindow.Show();
                    this.Close();
                    break;
                case "Subjects":
                    AddedSubjectsWindow addedSubjectsWindow = new AddedSubjectsWindow(_name);
                    addedSubjectsWindow.Show();
                    this.Close();
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            switch (_name)
            {
                case "Teachers":
                    if (GridTable.SelectedItems.Count > 0)
                    {
                        Teachers teachers = (Teachers)GridTable.SelectedItems[0];
                        AddedTeachersWindow addedTeachersWindow = new AddedTeachersWindow(_name, teachers.Id);
                        addedTeachersWindow.Show();
                        this.Close();
                    }
                    break;
                case "Couples":
                    if (GridTable.SelectedItems.Count > 0)
                    {
                        Couples couples = (Couples)GridTable.SelectedItems[0];
                        AddedCouplesWindow addedCouplesWindow = new AddedCouplesWindow(_name, couples.Id);
                        addedCouplesWindow.Show();
                        this.Close();
                    }
                    break;
                case "Cabinet":
                    if (GridTable.SelectedItems.Count > 0)
                    {
                        Cabinet cabinet = (Cabinet)GridTable.SelectedItems[0];
                        AddedCabinetWindow addedCabinetWindow = new AddedCabinetWindow(_name, cabinet.Id);
                        addedCabinetWindow.Show();
                        this.Close();
                    }
                    break;
                case "Groups":
                    if (GridTable.SelectedItems.Count > 0)
                    {
                        Groups groups = (Groups)GridTable.SelectedItems[0];
                        AddedGroupsWindow addedGroupsWindow = new AddedGroupsWindow(_name, groups.Id);
                        addedGroupsWindow.Show();
                        this.Close();
                    }
                    break;
                case "Plan":
                    if (GridTable.SelectedItems.Count > 0)
                    {
                        PlanDto paln = (PlanDto)GridTable.SelectedItems[0];
                        AddedPlanWindow addedPlanWindow = new AddedPlanWindow(_name, paln.Id);
                        addedPlanWindow.Show();
                        this.Close();
                    }
                    break;
                case "Subjects":
                    if (GridTable.SelectedItems.Count > 0)
                    {
                        Lesson lesson = (Lesson)GridTable.SelectedItems[0];
                        AddedSubjectsWindow addedSubjectsWindow = new AddedSubjectsWindow(_name, lesson.Id);
                        addedSubjectsWindow.Show();
                        this.Close();

                    }
                    break;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            switch (_name)
            {
                case "Teachers":
                    if (GridTable.SelectedItems.Count > 0)
                    {
                        Teachers teachers = (Teachers)GridTable.SelectedItems[0];
                        var currentItem = databaseEntities.Teachers.FirstOrDefault(item => item.Id == teachers.Id);
                        if (MessageBox.Show("Удалить?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {

                            if (databaseEntities.Subject.Any(p => p.IdTeachers == currentItem.Id))
                            {
                                MessageBox.Show("Нельзя удалить запись", "Ошибка");
                                return;
                            }
                            databaseEntities.Teachers.Remove(currentItem);
                            databaseEntities.SaveChanges();
                            GridTable.ItemsSource = databaseEntities.Groups.ToList();
                        }
                    }
                    break;
                case "Couples":
                    if (GridTable.SelectedItems.Count > 0)
                    {
                        Couples сouples = (Couples)GridTable.SelectedItems[0];
                        var currentItem = databaseEntities.Couples.FirstOrDefault(item => item.Id == сouples.Id);
                        if (MessageBox.Show("Удалить?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            if (databaseEntities.Schedule.Any(p => p.IdCouples == currentItem.Id))
                            {
                                MessageBox.Show("Нельзя удалить запись", "Ошибка");
                                return;
                            }
                            databaseEntities.Couples.Remove(currentItem);
                            databaseEntities.SaveChanges();
                            GridTable.ItemsSource = databaseEntities.Couples.ToList();
                        }
                    }
                    break;
                case "Cabinet":
                    if (GridTable.SelectedItems.Count > 0)
                    {
                        Cabinet сabinet = (Cabinet)GridTable.SelectedItems[0];
                        var currentItem = databaseEntities.Cabinet.FirstOrDefault(item => item.Id == сabinet.Id);
                        if (MessageBox.Show("Удалить?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            if (databaseEntities.Schedule.Any(p => p.IdCabinet == currentItem.Id))
                            {
                                MessageBox.Show("Нельзя удалить запись", "Ошибка");
                                return;
                            }
                            databaseEntities.Cabinet.Remove(currentItem);
                            databaseEntities.SaveChanges();
                            GridTable.ItemsSource = databaseEntities.Cabinet.ToList();
                        }
                    }
                    break;
                case "Groups":
                    if (GridTable.SelectedItems.Count > 0)
                    {
                        Groups groups = (Groups)GridTable.SelectedItems[0];
                        var currentItem = databaseEntities.Groups.FirstOrDefault(item => item.Id == groups.Id);
                        if (MessageBox.Show("Удалить?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            if (databaseEntities.Schedule.Any(p => p.IdGroups == currentItem.Id))
                            {
                                MessageBox.Show("Нельзя удалить запись", "Ошибка");
                                return;
                            }
                            databaseEntities.Groups.Remove(currentItem);
                            databaseEntities.SaveChanges();
                            GridTable.ItemsSource = databaseEntities.Groups.ToList();
                        }
                    }
                    break;
                case "Plan":
                    if (GridTable.SelectedItems.Count > 0)
                    {
                        PlanDto paln = (PlanDto)GridTable.SelectedItems[0];
                        var currentItem = databaseEntities.Subject.FirstOrDefault(item => item.Id == paln.Id);
                        if (MessageBox.Show("Удалить?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            if (databaseEntities.Schedule.Any(p => p.IdSubject == currentItem.Id))
                            {
                                MessageBox.Show("Нельзя удалить запись", "Ошибка");
                                return;
                            }
                            databaseEntities.Subject.Remove(currentItem);
                            databaseEntities.SaveChanges();

                            var subjects = databaseEntities?.Subject.ToList();
                            var lessons = databaseEntities?.Lesson.ToList();
                            var teachers = databaseEntities?.Teachers.ToList();
                            var planDto = subjects.ToList()
                                ?.Select(x => new PlanDto
                                {
                                    Id = x.Id,
                                    WorkTime = x.WorkTime,
                                    LessonName = lessons.FirstOrDefault(p => p.Id == x.IdLesson).Name,
                                    TeachersName = teachers.FirstOrDefault(p => p.Id == x.IdTeachers).Name
                                }).ToList();

                            GridTable.ItemsSource = planDto;
                        }
                    }
                    break;
                case "Subjects":
                    if (GridTable.SelectedItems.Count > 0)
                    {
                        Lesson lesson = (Lesson)GridTable.SelectedItems[0];
                        var currentItem = databaseEntities.Lesson.FirstOrDefault(item => item.Id == lesson.Id);
                        if (MessageBox.Show("Удалить?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            if (databaseEntities.Subject.Any(p => p.IdLesson == currentItem.Id))
                            {
                                MessageBox.Show("Нельзя удалить запись", "Ошибка");
                                return;
                            }
                            databaseEntities.Lesson.Remove(currentItem);
                            databaseEntities.SaveChanges();
                            GridTable.ItemsSource = databaseEntities.Lesson.ToList();
                        }
                    }
                    break;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            this.Close();
        }
    }
}
