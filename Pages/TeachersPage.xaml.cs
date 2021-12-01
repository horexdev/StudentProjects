using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using StudentProjects.Database;
using StudentProjects.ModalWindows;

namespace StudentProjects.Pages
{
    /// <summary>
    /// Логика взаимодействия для TeachersPage.xaml
    /// </summary>
    public partial class TeachersPage : Page
    {
        public TeachersPage()
        {
            InitializeComponent();

            UpdateTable();

            AddTeacherButton.Click += AddTeacherButton_Click;
            UpdateTableButton.Click += UpdateTableButton_Click;
        }

        private void UpdateTableButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateTable();
        }

        private void UpdateTable()
        {
            TeachersGrid.ItemsSource = Pools.Teachers
                .Select(t => new { t.FullName, AcademicDegreeName = t.AcademicDegree.Name, AcademicTitleName = t.AcademicTitle.Name })
                .Distinct();
        }

        private void AddTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            ModalHandler.Open(new AddTeacherModal());
        }
    }
}
