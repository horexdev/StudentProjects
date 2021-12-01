using System.Windows;
using System.Windows.Controls;
using StudentProjects.Database;
using StudentProjects.Pages;

namespace StudentProjects
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            if (Pools.IsPoolsNotLoaded())
            {
                LoadingText.Visibility = Visibility.Visible;

                OpenStudentsPageButton.IsEnabled = false;
                OpenTeachersPageButton.IsEnabled = false;
                OpenProjectsPageButton.IsEnabled = false;
                OpenStatsPageButton.IsEnabled = false;

                LoadData();
            }

            OpenTeachersPageButton.Click += OpenTeachersPageButton_Click;
            OpenStudentsPageButton.Click += OpenStudentsPageButton_Click;
        }

        private async void LoadData()
        {
            await Context.LoadTeachers();
            await Context.LoadStudents();

            OpenStudentsPageButton.IsEnabled = true;
            OpenTeachersPageButton.IsEnabled = true;
            OpenProjectsPageButton.IsEnabled = true;
            OpenStatsPageButton.IsEnabled = true;

            LoadingText.Visibility = Visibility.Hidden;
        }

        private void OpenStudentsPageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Navigation.Navigate(new StudentsPage());
        }

        private void OpenTeachersPageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Navigation.Navigate(new TeachersPage());
        }
    }
}
