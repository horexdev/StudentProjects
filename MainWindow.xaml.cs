using System.Windows;
using StudentProjects.Database;

namespace StudentProjects
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Navigation.SetFrame(MainFrame);
            Navigation.Navigate(new MainPage());
            ModalHandler.SetMainWindow(this);
        }
    }
}
