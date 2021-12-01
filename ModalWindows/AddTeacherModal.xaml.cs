using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using StudentProjects.Database;

namespace StudentProjects.ModalWindows
{
    /// <summary>
    /// Логика взаимодействия для AddTeacherModal.xaml
    /// </summary>
    public partial class AddTeacherModal : Window
    {
        public AddTeacherModal()
        {
            InitializeComponent();

            AcademicDegreeComboBox.ItemsSource = Context.GetAllAcademicDegrees();
            AcademicTitleComboBox.ItemsSource = Context.GetAllAcademicTitles();
            AcademicDegreeComboBox.DisplayMemberPath = "Name";
            AcademicTitleComboBox.DisplayMemberPath = "Name";
            AcademicDegreeComboBox.SelectedItem = AcademicDegreeComboBox.Items[0];
            AcademicTitleComboBox.SelectedItem = AcademicTitleComboBox.Items[0];

            AddButton.Click += AddButton_Click;
            FullNameTextBox.GotFocus += FullNameTextBox_GotFocus;
            FullNameTextBox.LostFocus += FullNameTextBox_LostFocus;
        }

        private void ResetText()
        {
            if (FullNameTextBox.Text != "Фамилия Имя Отчество")
                return;

            FullNameTextBox.Text = "";
            FullNameTextBox.FontSize = 16;
            FullNameTextBox.Foreground = Brushes.Black;
        }

        private void AddText()
        {
            if (!string.IsNullOrWhiteSpace(FullNameTextBox.Text))
                return;

            FullNameTextBox.Text = "Фамилия Имя Отчество";
            FullNameTextBox.FontSize = 16;
            FullNameTextBox.Foreground = Brushes.LightGray;
        }

        private void FullNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText();
        }

        private void FullNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ResetText();
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

            var academicDegree = AcademicDegreeComboBox.SelectedItem as AcademicDegree;
            var academicTitle = AcademicTitleComboBox.SelectedItem as AcademicTitle;

            if (Context.IsAnyTeacherCoincidences(fullName) || academicDegree == null || academicTitle == null)
            {
                MessageBox.Show("Не удалось добавить нового преподавателя.\rПроверьте:\rЗаполнили ли вы все поля\rВозможно преподаватель с таким ФИО уже существует", "", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            var teacher = new Teacher
            {
                FullName = fullName,
                AcademicDegreeId = academicDegree.Id,
                AcademicTitleId = academicTitle.Id
            };

            Context.AddTeacher(teacher);
            teacher.AcademicDegree = academicDegree;
            teacher.AcademicTitle = academicTitle;

            var result = MessageBox.Show("Добавить ещё?", "Сложный выбор", MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    FullNameTextBox.Text = "Фамилия Имя Отчество";
                    FullNameTextBox.Foreground = Brushes.LightGray;
                    FullNameTextBox.FontSize = 16;
                    AcademicDegreeComboBox.SelectedItem = AcademicDegreeComboBox.Items[0];
                    AcademicTitleComboBox.SelectedItem = AcademicTitleComboBox.Items[0];

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
                   || FullNameTextBox.Text == "Фамилия Имя Отчество";
        }

        private void AddTeacherModal_OnClosing(object sender, CancelEventArgs e)
        {
            Owner.Activate();
        }
    }
}
