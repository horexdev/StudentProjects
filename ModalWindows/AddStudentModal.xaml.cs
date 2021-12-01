using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using StudentProjects.Database;

namespace StudentProjects.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для AddStudentModal.xaml
    /// </summary>
    public partial class AddStudentModal : Window
    {
        public AddStudentModal()
        {
            InitializeComponent();

            GroupComboBox.ItemsSource = Context.GetAllGroups();
            SpecialityComboBox.ItemsSource = Context.GetAllSpeciality();
            GroupComboBox.DisplayMemberPath = "Name";
            SpecialityComboBox.DisplayMemberPath = "Name";
            GroupComboBox.SelectedItem = GroupComboBox.Items[0];
            SpecialityComboBox.SelectedItem = SpecialityComboBox.Items[0];

            AddButton.Click += AddButton_Click;
            FullNameTextBox.GotFocus += ResetText;
            FullNameTextBox.LostFocus += AddText;
            RecordBookNumberTextBox.GotFocus += ResetText;
            RecordBookNumberTextBox.LostFocus += AddText;
            CathedraNumberTextBox.GotFocus += ResetText;
            CathedraNumberTextBox.LostFocus += AddText;
        }

        private void ResetText(object sender, RoutedEventArgs e)
        {
            if (FullNameTextBox == null || RecordBookNumberTextBox == null || CathedraNumberTextBox == null)
                return;

            var textBox = sender as TextBox;

            if (textBox == FullNameTextBox && FullNameTextBox.Text == "Фамилия Имя Отчество")
            {
                FullNameTextBox.Text = string.Empty;
                FullNameTextBox.FontSize = 16;
                FullNameTextBox.Foreground = Brushes.Black;
            }
            else if (textBox == RecordBookNumberTextBox && RecordBookNumberTextBox.Text == "Номер зачётки")
            {
                RecordBookNumberTextBox.Text = string.Empty;
                RecordBookNumberTextBox.FontSize = 16;
                RecordBookNumberTextBox.Foreground = Brushes.Black;
            }
            else if (textBox == CathedraNumberTextBox && CathedraNumberTextBox.Text == "Номер кафедры")
            {
                CathedraNumberTextBox.Text = string.Empty;
                CathedraNumberTextBox.FontSize = 16;
                CathedraNumberTextBox.Foreground = Brushes.Black;
            }
        }

        private void AddText(object sender, RoutedEventArgs e)
        {
            if (FullNameTextBox == null || RecordBookNumberTextBox == null || CathedraNumberTextBox == null)
                return;

            var textBox = sender as TextBox;
            if (textBox == FullNameTextBox)
            {
                if (!string.IsNullOrWhiteSpace(FullNameTextBox.Text))
                    return;

                FullNameTextBox.Text = "Фамилия Имя Отчество";
                FullNameTextBox.FontSize = 16;
                FullNameTextBox.Foreground = Brushes.LightGray;
            }
            else if (textBox == RecordBookNumberTextBox)
            {
                if (!string.IsNullOrWhiteSpace(RecordBookNumberTextBox.Text))
                    return;

                RecordBookNumberTextBox.Text = "Номер зачётки";
                RecordBookNumberTextBox.FontSize = 16;
                RecordBookNumberTextBox.Foreground = Brushes.LightGray;
            }
            else if (textBox == CathedraNumberTextBox)
            {
                if (!string.IsNullOrWhiteSpace(CathedraNumberTextBox.Text))
                    return;

                CathedraNumberTextBox.Text = "Номер кафедры";
                CathedraNumberTextBox.FontSize = 16;
                CathedraNumberTextBox.Foreground = Brushes.LightGray;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsInputsFilledInvalid())
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                return;
            }

            var fullName = FullNameTextBox.Text;
            if (fullName == "Фамилия Имя Отчество")
                fullName = string.Empty;

            var group = GroupComboBox.SelectedItem as Group;
            var speciality = SpecialityComboBox.SelectedItem as Speciality;
            var recordNumberBook = Convert.ToInt32(RecordBookNumberTextBox.Text);
            var cathedraNumberBook = Convert.ToInt32(CathedraNumberTextBox.Text);

            if (Context.IsAnyStudentCoincidences(fullName) || group == null || speciality == null || recordNumberBook == 0 || cathedraNumberBook == 0)
            {
                MessageBox.Show("Не удалось добавить нового студента.\rПроверьте:\rЗаполнили ли вы все поля\rВозможно студент с таким ФИО уже существует", "", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            var student = new Student
            {
                FullName = fullName,
                RecordBookNumber = recordNumberBook,
                GroupId = group.Id,
                SpecialityId = speciality.Id,
                CathedraNumber = cathedraNumberBook
            };

            Context.AddStudent(student);
            student.Group = group;
            student.Speciality = speciality;

            var result = MessageBox.Show("Добавить ещё?", "Сложный выбор", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    FullNameTextBox.Text = "Фамилия Имя Отчество";
                    FullNameTextBox.Foreground = Brushes.LightGray;
                    FullNameTextBox.FontSize = 16;
                    RecordBookNumberTextBox.Text = "Номер зачётки";
                    RecordBookNumberTextBox.Foreground = Brushes.LightGray;
                    RecordBookNumberTextBox.FontSize = 16;
                    GroupComboBox.SelectedItem = GroupComboBox.Items[0];
                    SpecialityComboBox.SelectedItem = GroupComboBox.Items[0];
                    CathedraNumberTextBox.Text = "Номер кафедры";
                    CathedraNumberTextBox.Foreground = Brushes.LightGray;
                    CathedraNumberTextBox.FontSize = 16;

                    break;
                case MessageBoxResult.No:
                    Owner.Activate();
                    Close();

                    break;
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    break;
                case MessageBoxResult.Cancel:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool IsInputsFilledInvalid()
        {
            return string.IsNullOrEmpty(FullNameTextBox.Text)
                   || string.IsNullOrEmpty(RecordBookNumberTextBox.Text)
                   || string.IsNullOrEmpty(CathedraNumberTextBox.Text)
                   || FullNameTextBox.Text == "Фамилия Имя Отчество"
                   || RecordBookNumberTextBox.Text == "Номер зачётки"
                   || CathedraNumberTextBox.Text == "Номер кафедры";
        }

        private void AddStudentModal_OnClosing(object sender, CancelEventArgs e)
        {
            Owner.Activate();
        }
    }
}
