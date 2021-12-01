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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudentProjects.Database;
using StudentProjects.ModalWindows;

namespace StudentProjects.Pages
{
    /// <summary>
    /// Логика взаимодействия для StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {
        public StudentsPage()
        {
            InitializeComponent();

            UpdateTable();

            AddStudentButton.Click += AddStudentButton_Click;
            UpdateTableButton.Click += UpdateTableButton_Click;
        }

        private void UpdateTableButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable();
        }

        private void UpdateTable()
        {
            StudentsGrid.ItemsSource = Pools.Students
                .Select(s => new
                {
                    s.FullName, s.RecordBookNumber, Group = s.Group.Name, Speciality = s.Speciality.Name,
                    Cathedra = s.CathedraNumber
                })
                .Distinct().ToList();
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            ModalHandler.Open(new AddStudentModal());
        }
    }
}
